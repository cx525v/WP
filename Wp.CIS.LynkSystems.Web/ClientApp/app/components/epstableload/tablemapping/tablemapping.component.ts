import { Component, OnInit, Input, OnChanges, SimpleChanges} from '@angular/core';
import { TableMappingServcie } from '../../../services/petro/tablemapping.service';
import '../../../app.module.client';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectItem, Dropdown, Checkbox, Message, Growl, ConfirmationService} from 'primeng/primeng';
import { commanderversion, commanderbaseversion } from '../../../models/petro/commanderversion.model';
import { TableMapping, Mapping } from '../../../models/petro/petroTablemapping.model';
import { CBSParam} from '../../../models/petro/CBSParam.model';
import { DatePipe } from '@angular/common';
import { ViewEncapsulation } from '@angular/core';
import { UploadMappingComponent } from './upload/uploadmapping.component';
import { OnlyNumber } from '../../../directives/onlyNumber.directive';
@Component({
    selector: 'tablemapping',   
    templateUrl: './tablemapping.component.html',
    styleUrls: ['./tablemapping.component.css'],
    providers: [TableMappingServcie, ConfirmationService],
    encapsulation: ViewEncapsulation.None,  
})
export class TableMappingComponent implements OnInit {
    public dataSource: TableMapping[] = [ ];
    public loading: boolean = false;
    displayDialog: boolean = false;;
    displayUploadDialog: boolean = false;
    //selectedTableMapping: TableMapping;
    mapping: TableMapping = new Mapping();
    newMapping: boolean; 
    msgs: Message[] = [];
    @Input() version: commanderbaseversion;
    @Input() commanderBasisVersions: SelectItem[] = [];
    Versions: SelectItem[] = [];
    selectedBasisVersion: commanderbaseversion;
    datePipe: DatePipe = new DatePipe('en-us');
    params: CBSParam[];
    paramDropdownItems: SelectItem[] = [];
    selectedParam: CBSParam;
    effectiveBeginDate: Date;
    effectiveEndDate: Date;

    constructor(private tableMappingServcie: TableMappingServcie, private conformationService: ConfirmationService) {
    }
    ngOnInit() {      
    }

    ngOnChanges(changes: SimpleChanges) {
        
        if (changes['version']) {
            this.getMapping();
            this.filterVersion();
            this.getCBSParameters();         
        }
    }

    getCBSParameters() {
        this.loading = true;
        if (this.version != undefined) {
            this.paramDropdownItems = [];
            this.tableMappingServcie.getCBSParameters()
                .subscribe(res => {
                    this.params = res as CBSParam[];  
                    this.params.forEach(param => {                        
                        this.paramDropdownItems.push({ label: param.paramName, value: param });                          
                    });
                   
                    this.loading = false;
                },
                error => {
                    this.loading = false;
                    console.log(error);
                }
            );
        }
    }

    formatDate(date: any) {

        return this.datePipe.transform(date, 'yyyy-MM-dd');
    }

    filterVersion() {
        this.Versions = [];
        for (let i = 0; i < this.commanderBasisVersions.length; i++) {
        
                if (this.commanderBasisVersions[i].value.versionID != this.version.versionID) {
                    this.Versions.push(this.commanderBasisVersions[i]);           
                }
            }      
    }

    getMapping() {       
        if (this.version) {
            this.loading = true;
            this.dataSource = [];
            this.tableMappingServcie.getTableMappings(this.version.versionID)
                .subscribe(res => {
                    this.dataSource = res as TableMapping[];   
                    for (let i = 0; i < this.dataSource.length;i++) {
                        this.dataSource[i].effectiveBeginDate = this.formatDate(this.dataSource[i].effectiveBeginDate);
                        this.dataSource[i].effectiveEndDate = this.formatDate(this.dataSource[i].effectiveEndDate);
                    }           
                    this.loading = false;
                },
                error => {
                    this.loading = false;
                    console.log(error);
                }
                );
        }
    }


    showDialogToAdd() {
        this.newMapping = true;
        this.mapping = new Mapping();
        this.mapping.versionID = this.version.versionID;
        this.effectiveBeginDate = undefined ;
        this.effectiveEndDate = undefined ;
        this.displayDialog = true;
    }

    save() {      
        this.conformationService.confirm({
            message: this.newMapping? 'Are you sure that you want to add the mapping?' : 'Are you sure that you want to make the change?',
            accept: () => {
                this.loading = true;
                let tableMappings = [...this.dataSource];
                if (this.mapping && this.mapping.pdlFlag && this.selectedParam && this.selectedParam.parameterID) {
                    this.mapping.paramID = this.selectedParam.parameterID;
                    this.mapping.paramName = this.selectedParam.paramName;
                }

                this.mapping.effectiveBeginDate = this.formatDate(this.effectiveBeginDate);
                this.mapping.effectiveEndDate = this.formatDate(this.effectiveEndDate);

                if (this.newMapping) {
                    this.mapping.versionID = this.version.versionID;
                    this.tableMappingServcie.insertTableMappings(this.mapping).subscribe(res => {
                        let added: boolean = res as boolean;
                        if (added) {
                            this.msgs = [];
                            this.msgs.push({ severity: 'sucess', summary: 'Added', detail: 'The mapping is added' });
                            this.displayDialog = false;
                            this.getMapping();
                            this.loading = false;
                        } else {

                            this.msgs = [];
                            this.msgs.push({ severity: 'error', summary: 'Error', detail: 'Failed to add new mapping' });
                            this.displayDialog = false;
                            this.loading = false;
                        }
                    },
                        error => {
                            console.log(error);
                            this.loading = false;
                        }
                    );

                }
                else {
                      this.tableMappingServcie.updateableMappings(this.mapping).subscribe(res => {
                        let saved: boolean = res as boolean;
                        if (saved) {
                            this.msgs = [];
                            this.msgs.push({ severity: 'sucess', summary: 'Saved', detail: 'The mapping is saved' });
                            this.getMapping();
                            this.displayDialog = false;
                        } else {
                            this.msgs = [];
                            this.msgs.push({ severity: 'error', summary: 'Error', detail: 'Failed to update mapping' });
                            this.displayDialog = false;
                        }
                        this.loading = false;
                    },
                        error => {
                            this.displayDialog = false;
                            this.loading = false;
                        });
                }
                this.dataSource = tableMappings;
                this.mapping = null;
                this.displayDialog = false;
            },
            header: this.newMapping ? 'Adding Mapping':'Saving Mapping'
        });     
    }

    SelectMapping(mapping) {
        this.mapping =this.clone(mapping);      
        this.showMapping();
    }
 
    showMapping() {
        this.newMapping = false;        
        this.effectiveBeginDate = new Date(this.mapping.effectiveBeginDate + 'T00:00:00');
        this.effectiveEndDate = new Date(this.mapping.effectiveEndDate + 'T00:00:00');
        if (this.mapping.paramID != undefined) {
            this.paramDropdownItems.forEach(
                param => {
                    if (this.mapping.paramID == param.value.parameterID) {
                        this.selectedParam = param.value;
                    }
                }
            )
        };

        this.displayDialog = true;
    }

    clone(c: Mapping): Mapping {
        let mapping = new Mapping();
        for (let prop in c) {
          mapping[prop] = c[prop];
        }
        mapping.effectiveBeginDate = this.formatDate(mapping.effectiveBeginDate);
        mapping.effectiveEndDate = this.formatDate(mapping.effectiveEndDate);
        return mapping;
    }
    copy() {
        if (this.selectedBasisVersion == undefined) {
            this.msgs = [];
            this.msgs.push({ severity: 'warn', summary: 'Warn Message', detail: 'Please select a commander version!' });
        }
        else {
            if (this.version.versionID === this.selectedBasisVersion.versionID) {
                this.msgs = [];
                this.msgs.push({ severity: 'error', summary: 'save version', detail: 'Please select a commander version!' });

            }
            else {
                this.conformationService.confirm({
                    message: 'Are you sure that you want to copy the mapping from ' + this.selectedBasisVersion.versionDescription + '?',
                    accept: () => {
                        this.tableMappingServcie.copyMapping(this.selectedBasisVersion.versionID, this.version.versionID)
                            .subscribe((data) => {
                               if (data) {
                                    this.msgs = [];
                                    this.msgs.push({ severity: 'sucess', summary: 'mapping copied', detail: 'mapping copied sucessfully!' });
                                    this.getMapping();
                                } else {
                                    this.msgs = [];
                                    this.msgs.push({ severity: 'error', summary: 'coping error', detail: 'failed to copy mapping!' });
                                }
                            })
                    },
                    header: 'Copying Mapping'
                });
            }
        }
    }


    showUploadDialog() {
        this.displayUploadDialog = true;
    }

    convertDate(dateString: string): Date {
        return new Date(dateString);
    }
    isEffectiveDateValid(): boolean {
        if (!this.effectiveEndDate && !this.effectiveBeginDate) {
            return true;
        }
        else if (this.effectiveBeginDate > this.effectiveEndDate) {
            return false;
        } else {
            return true;
        }
    }

    isCharStartIndexNumber(): boolean {
        if (!this.mapping.charStartIndex)
            return true;
        else {
            var input: string = this.mapping.charStartIndex.toString()
            return new RegExp('^[0-9]{0,}$').test(input) ? true : false;
        }        
    }

    isCharLengthNumber(): boolean {
        if (!this.mapping.charLength) {
            return true;
        }
        else {
            var input: string = this.mapping.charLength.toString()
            return new RegExp('^[0-9]{0,}$').test(input) ? true : false;
        }
    }

    isFormValid(): boolean {      
        if (this.mapping) {
            if (!this.effectiveBeginDate
                || !this.effectiveEndDate
                || !this.mapping.viperTableName
                || !this.mapping.viperFieldName
                || !this.isEffectiveDateValid()   
                || !this.isCharStartIndexNumber()
                || !this.isCharLengthNumber()) {
                return false;
            }
            else if (this.mapping.pdlFlag) {
                if (!this.selectedParam) {
                    return false;
                } else {
                    return true;
                }
            }
            else {
                if (!this.mapping.worldPayTableName) {
                    return false;
                } else if (!this.mapping.worldPayFieldName) {
                    return false;
                } else {
                    return true;
                }
            }
        } else {

            return false;
        }
    }
    change(event) {
        this.mapping.paramID = this.selectedParam.parameterID;
        this.mapping.paramName = this.selectedParam.paramName;
    }
}
