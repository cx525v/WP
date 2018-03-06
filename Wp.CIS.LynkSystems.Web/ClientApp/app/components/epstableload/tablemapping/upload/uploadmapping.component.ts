import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { TableMappingServcie } from '../../../../services/petro/tablemapping.service';
import '../../../../app.module.client';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectItem, Dropdown, Checkbox, Message, Growl, ConfirmationService } from 'primeng/primeng';
import { TableMapping, Mapping } from '../../../../models/petro/petroTablemapping.model';
import { DatePipe } from '@angular/common';
import { ViewEncapsulation } from '@angular/core';
import { Pipe, PipeTransform } from '@angular/core';
@Component({
    selector: 'uploadmapping',
    templateUrl: './uploadmapping.component.html',
    styleUrls: ['./uploadmapping.component.css'],
    providers: [],
    encapsulation: ViewEncapsulation.None,
})

@Pipe({ name: 'pxsuffix'
})
export class UploadMappingComponent implements OnInit, PipeTransform{
    fileName: string;
    fileSize: string;
    displaySave: boolean = false;
    processingUpload: boolean = false;
    ngOnInit() {

    }

    transform(input: number): string{  
       return input + '';
   }

    onUploadAuto(event) {
        console.log('complete');
    }

    onUploadHandler(event) {

        console.log(event);
    }

    uploadfile(event) {
        this.processingUpload = true;
        this.fileName = 'file name: ' + event.target.files[0].name;
        this.fileSize = 'size: ' + event.target.files[0].size;
        this.displaySave = true;
        this.processingUpload = false;
        
    }

    uploadMapping() {
        this.processingUpload = true;      
    }
}