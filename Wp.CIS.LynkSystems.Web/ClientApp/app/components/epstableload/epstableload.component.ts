import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Dropdown, Button, SelectItem } from 'primeng/primeng';
import { CommanderVersionService } from '../../services/petro/commanderversion.service';
import { commanderbaseversion, commanderversion } from '../../models/petro/commanderversion.model';
import '../../app.module.client';
import { ConfirmationService, Message, Growl, Messages } from 'primeng/primeng';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'epstableload',
    templateUrl: './epstableload.component.html',
    styleUrls: ['./epstableload.component.css'],
    providers: [CommanderVersionService, ConfirmationService],
})
export class EPSTableLoadComponent implements OnInit {

    public commanderBasisVersions: SelectItem[] = [];
    public basisVersions: SelectItem[] = [];
    public selectedBasisVersion: commanderbaseversion;
    public basisVersion: commanderbaseversion;
    public displayVersion: commanderbaseversion;
    public newVersion: string ='';    
    public displayVersionDialog: boolean = false;
    public msgs: Message[] = [];
    public loading = false;
    constructor(private commanderVersionService: CommanderVersionService, private confirmationService: ConfirmationService) {
    }

    ngOnInit() {
        this.getVersions();     
    }
   
    versionExists() {
        var exists = false;
        if (this.newVersion) {
            if (this.commanderBasisVersions) {
                this.commanderBasisVersions.forEach(v => {
                    if (v.label.toUpperCase() == this.newVersion) {
                        exists = true;
                    }
                });
            }
        }
        return exists;
    }


    isValidatedVersion(): boolean {
        if (!this.newVersion || this.newVersion.length > 20) {
            return false;
        } else {
            return new RegExp('^[A-Z .0-9#$*()?!+_-]{1,20}$').test(this.newVersion) ? true : false;          
        }
    }

    getVersions() {
        this.loading = true;
        this.commanderVersionService.getCommanderBaseVersion().subscribe(res => {
            let versions: commanderbaseversion[] = res;
            this.commanderBasisVersions = [];
            versions.forEach(ver => {
                this.commanderBasisVersions.push({ label: ver.versionDescription, value: ver });
            });
            if (versions.length > 0) {
                this.selectedBasisVersion = versions[0];
                this.displayVersion = this.selectedBasisVersion;
            }
            this.loading = false;
        },
            error => {
                this.loading = false;
               // console.log(error);
            }

        ); 
    }
    
    public versionClick() {
        this.basisVersions = [];
        let v: commanderversion = {
            versionDescription: '',
            basisVersion: '',
            createdByUser: '',
            createdDate:''
        };
        let item: SelectItem = {
            label: "Select Basis Version", value: v
        };
        this.basisVersions.push(item);
        this.commanderBasisVersions.forEach(ver => {
            this.basisVersions.push(ver);
        });

        this.msgs = [];
        if (this.selectedBasisVersion) {
            this.basisVersion = {
                versionID: this.selectedBasisVersion.versionID,
                versionDescription: this.selectedBasisVersion.versionDescription,
                createdByUser: this.selectedBasisVersion.createdByUser,
                createdDate: this.selectedBasisVersion.createdDate
            };           
        } else {
            this.basisVersion = undefined;
        }
        this.displayVersionDialog = true;
    }
   
    validatedNewversion(): boolean {
     
        let invalidlength: boolean = !this.newVersion || this.newVersion.length > 60;
        if (invalidlength) {
            this.msgs = [];
            this.msgs.push({ severity: 'error', summary: 'Version Invalid', detail: this.newVersion + ' is invalid' });
            return false;
        }
        else {

            let loop: boolean = true;
            let exists: boolean = false;
            this.commanderBasisVersions.forEach(ver => {
                if (loop) {
                    if (ver.label.trim() === this.newVersion.trim()) {
                        exists = true;
                        loop = false;
                    }
                }
            });
            if (exists) {
                this.msgs = [];
                this.msgs.push({ severity: 'error', summary: 'version exists', detail: this.newVersion + ' already exists' });
                return false;
            } else {

                return true;
            }
        }
     
    }

    CreateVersion() {  
        this.msgs = [];       
        if (this.validatedNewversion()) {
           let version: commanderversion = {
               baseVersionID: this.basisVersion?this.basisVersion.versionID:undefined,
               versionDescription: this.newVersion.trim().toUpperCase(),             
               createdDate: '',
               createdByUser: '',
               basisVersion: this.basisVersion.versionDescription,
               versionID: 0
           };

           this.commanderVersionService.addComanderVersion(version).subscribe(
               (data) => {
                   let added: boolean = data as boolean;
                   if (added) {
                       this.getVersions();
                       this.msgs = [];
                       this.msgs.push({ severity: 'sucess', summary: 'Commander Version', detail: version.versionDescription + ' is added successfully!' });
                       this.displayVersionDialog = false;
                   } else {
                       this.msgs = [];
                       this.msgs.push({ severity: 'error', summary: 'Failed to add new commander version', detail: version.versionDescription + ' is NOT added successfully!' });
                   }
               }
           );
       }
    }

    mouseover(event) {
       this.displayVersion = event.value;
    }  

    delete(event) {
        let deletingVersion: commanderbaseversion = event.value as commanderbaseversion;
        this.confirmationService.confirm({
            message: 'Are you sure that you want to delete ' + deletingVersion.versionDescription + '?',
            accept: () => {
                this.commanderVersionService.deleteCommanderVersion(deletingVersion.versionID).subscribe(
                 (data) =>
                    {
                     let deleted: boolean = data as boolean;    
                     if (deleted) {
                         let index: number = this.commanderBasisVersions.indexOf(event);
                         if (index > -1) {
                             this.commanderBasisVersions.splice(index, 1);
                             this.msgs = [];
                             this.msgs.push({ severity: 'sucess', summary: 'Commander Version', detail: deletingVersion.versionDescription + ' is removed successfully!' });
                         }

                         if (this.commanderBasisVersions.length > 0) {
                             this.selectedBasisVersion = this.commanderBasisVersions[0].value;
                         }
                                      
                     } else {
                         this.msgs = [];
                         this.msgs.push({ severity: 'Error', summary: 'Failed to remove commander version', detail: deletingVersion.versionDescription + ' is not removed!' });
                     }
                    },
                 (error) =>
                     {
                     this.msgs = [];
                     this.msgs.push({ severity: 'error', summary: 'Deleting commander versin error!', detail: 'Error occurred while deleteing ' + deletingVersion.versionDescription + '!' });
                      }
              );
            },
            header: 'deleting commander version'
        });
    }

    versionChange(event) {
       this.newVersion = this.newVersion.toUpperCase();
    }
}