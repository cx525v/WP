import { Injectable } from '@angular/core';
@Injectable()
export class DownloadService {
    constructor() {      
    }

    download(xml: string, fileName: string) {       
        var blob = new Blob([xml], { type: 'text/xml' });  
        if (window.navigator && window.navigator.msSaveOrOpenBlob) {
            window.navigator.msSaveOrOpenBlob(blob, fileName);
        } else {          
            var a = document.createElement("a");
            a.setAttribute('style', 'display:none;');
            document.body.appendChild(a);
            var url = window.URL.createObjectURL(blob);       
            a.href = url;
            a.download = fileName;       
            a.click();      
        }
    }
}