
import { Component, OnInit, TemplateRef, EventEmitter, QueryList, AfterContentInit, Input, Output } from '@angular/core';

import { DomSanitizer } from '@angular/platform-browser';

import { Message, ProgressBar, Button, PrimeTemplate, TemplateLoader } from 'primeng/primeng';

import { MessagesModule } from 'primeng/primeng';
//import { Messag } from 'primeng/primeng';

@Component({
    selector: 'file-uploader-cis',
    templateUrl: 'file-upload-cis.component.html',
    styleUrls: ['file-upload-cis.component.css']
})
export class FileUploadComponent implements OnInit, AfterContentInit {

    //private sanitizer;
    @Input() name: string;
    @Input() url: string;
    @Input() method: string;
    @Input() multiple: boolean;
    @Input() accept: string;
    @Input() disabled: boolean;
    @Input() auto: boolean;
    @Input() withCredentials: boolean;
    @Input() maxFileSize: number;
    @Input() invalidFileSizeMessageSummary: string;
    @Input() invalidFileSizeMessageDetail: string;
    @Input() invalidFileTypeMessageSummary: string;
    @Input() invalidFileTypeMessageDetail: string;
    @Input() style: string;
    @Input() styleClass: string;
    previewWidth: number;
    @Input() chooseLabel: string;
    @Input() uploadLabel: string;
    @Input() cancelLabel: string;
    @Input() showUploadButton: boolean;
    @Input() showCancelButton: boolean;
    @Input() mode: string;
    @Input() customUpload: boolean;
    @Output() onBeforeUpload: EventEmitter<any>;
    @Output() onBeforeSend: EventEmitter<any>;
    @Output() onUpload: EventEmitter<any>;
    @Output() onError: EventEmitter<any>;
    @Output() onClear: EventEmitter<any>;
    @Output() onRemove: EventEmitter<any>;
    @Output() onSelect: EventEmitter<any>;
    @Output() uploadHandler: EventEmitter<any>;
    templates: QueryList<any>;
    files: File[];
    progress: number;
    dragHighlight: boolean;
    msgs: Message[];
    fileTemplate: TemplateRef<any>;
    contentTemplate: TemplateRef<any>;
    toolbarTemplate: TemplateRef<any>;
    focus: boolean;

    constructor(private sanitizer: DomSanitizer) {

        this.method = 'POST';
        this.invalidFileSizeMessageSummary = '{0}: Invalid file size, ';
        this.invalidFileSizeMessageDetail = 'maximum upload size is {0}.';
        this.invalidFileTypeMessageSummary = '{0}: Invalid file type, ';
        this.invalidFileTypeMessageDetail = 'allowed file types: {0}.';
        this.previewWidth = 50;
        this.chooseLabel = 'Choose';
        this.uploadLabel = 'Upload';
        this.cancelLabel = 'Cancel';
        this.showUploadButton = true;
        this.showCancelButton = true;
        this.mode = 'advanced';
        this.onBeforeUpload = new EventEmitter();
        this.onBeforeSend = new EventEmitter();
        this.onUpload = new EventEmitter();
        this.onError = new EventEmitter();
        this.onClear = new EventEmitter();
        this.onRemove = new EventEmitter();
        this.onSelect = new EventEmitter();
        this.uploadHandler = new EventEmitter();
        this.progress = 0;
        this.templates = new QueryList<any>();

    }

    ngOnInit(): void {

        this.files = [];
    }

    ngAfterContentInit(): void {

        this.templates.forEach(function (item) {
            switch (item.getType()) {
                case 'file':
                    this.fileTemplate = item.template;
                    break;
                case 'content':
                    this.contentTemplate = item.template;
                    break;
                case 'toolbar':
                    this.toolbarTemplate = item.template;
                    break;
                default:
                    this.fileTemplate = item.template;
                    break;
            }
        });
    }

    onChooseClick(event: any, fileInput: any): void {

        fileInput.value = null;
        fileInput.click();
    }

    onFileSelect(event: any): void {

        this.msgs = [];
        if (!this.multiple) {
            this.files = [];
        }

        let files = event.dataTransfer ? event.dataTransfer.files : event.target.files;
        for (let i = 0; i < files.length; i++) {
            let file = files[i];
            if (this.validate(file)) {
                if (this.isImage(file)) {
                    file.objectURL = this.sanitizer.bypassSecurityTrustUrl((window.URL.createObjectURL(files[i])));
                }
                this.files.push(files[i]);
            }
        }

        this.onSelect.emit({ originalEvent: event, files: files });
        if (this.hasFiles() && this.auto) {
            this.upload();
        }

    }

    validate(file: File): boolean {

        if (this.accept && !this.isFileTypeValid(file)) {
            this.msgs.push({
                severity: 'error',
                summary: this.invalidFileTypeMessageSummary.replace('{0}', file.name),
                detail: this.invalidFileTypeMessageDetail.replace('{0}', this.accept)
            });
            return false;
        }
        if (this.maxFileSize && file.size > this.maxFileSize) {
            this.msgs.push({
                severity: 'error',
                summary: this.invalidFileSizeMessageSummary.replace('{0}', file.name),
                detail: this.invalidFileSizeMessageDetail.replace('{0}', this.formatSize(this.maxFileSize))
            });
            return false;
        }

        return true;
    }

    private isFileTypeValid(file): boolean {

        let acceptableTypes = this.accept.split(',');
        for (let _i = 0, acceptableTypes_1 = acceptableTypes; _i < acceptableTypes_1.length; _i++) {
            let type = acceptableTypes_1[_i];
            let acceptable = this.isWildcard(type) ? this.getTypeClass(file.type) === this.getTypeClass(type)
                : file.type == type || this.getFileExtension(file) === type;
            if (acceptable) {
                return true;
            }
        }

        return false;
    }

    getTypeClass(fileType: string): string {

        return fileType.substring(0, fileType.indexOf('/'));
    }

    isWildcard(fileType: string): boolean {

        return fileType.indexOf('*') !== -1;
    }

    getFileExtension(file: File): string {

        return '.' + file.name.split('.').pop();
    }

    isImage(file: File): boolean {

        return /^image\//.test(file.type);
    }

    onImageLoad(img: any): void {

        window.URL.revokeObjectURL(img.src);
    }

    upload(): void {

        let _this = this;
        if (this.customUpload) {
            this.uploadHandler.emit({
                files: this.files
            });
        }
        else {
            this.msgs = [];
            let xhr_1 = new XMLHttpRequest(), formData = new FormData();
            this.onBeforeUpload.emit({
                'xhr': xhr_1,
                'formData': formData
            });
            for (let i = 0; i < this.files.length; i++) {
                formData.append(this.name, this.files[i], this.files[i].name);
            }
            xhr_1.upload.addEventListener('progress', function (e) {
                if (e.lengthComputable) {
                    _this.progress = Math.round((e.loaded * 100) / e.total);
                }
            }, false);
            xhr_1.onreadystatechange = function () {
                if (xhr_1.readyState == 4) {
                    _this.progress = 0;
                    if (xhr_1.status >= 200 && xhr_1.status < 300)
                        _this.onUpload.emit({ xhr: xhr_1, files: _this.files });
                    else
                        _this.onError.emit({ xhr: xhr_1, files: _this.files });
                    _this.clear();
                }
            };
            xhr_1.open(this.method, this.url, true);
            this.onBeforeSend.emit({
                'xhr': xhr_1,
                'formData': formData
            });
            xhr_1.withCredentials = this.withCredentials;
            xhr_1.send(formData);
        }
    }

    clear(): void {

        this.files = [];
        this.onClear.emit();
    }

    remove(event: Event, index: number): void {

        this.onRemove.emit({ originalEvent: event, file: this.files[index] });
        this.files.splice(index, 1);
    }

    hasFiles(): boolean {

        return this.files && this.files.length > 0;
    }

    onDragEnter(e: any): void {

        if (!this.disabled) {
            e.stopPropagation();
            e.preventDefault();
        }

    }

    onDragOver(e: any): void {

        if (!this.disabled) {
            this.dragHighlight = true;
            e.stopPropagation();
            e.preventDefault();
        }
    }

    onDragLeave(event: any): void {

        if (!this.disabled) {
            this.dragHighlight = false;
        }
    }

    onDrop(event: any): void {

        if (!this.disabled) {
            this.dragHighlight = false;
            event.stopPropagation();
            event.preventDefault();
            var files = event.dataTransfer ? event.dataTransfer.files : event.target.files;
            var allowDrop = this.multiple || (files && files.length === 1);
            if (allowDrop) {
                this.onFileSelect(event);
            }
        }
    }

    onFocus(): void {

        this.focus = true;
    }

    onBlur(): void {

        this.focus = false;
    }

    formatSize(bytes: any): string {

        if (bytes == 0) {
            return '0 B';
        }
        let k = 1000, dm = 3, sizes = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'], i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];

    }

    onSimpleUploaderClick(event: Event): void {

        if (this.hasFiles()) {
            this.upload();
        }
    }


}