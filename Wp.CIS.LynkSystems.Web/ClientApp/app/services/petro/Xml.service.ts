import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import { PetroTable, UpdateXmlModel, Tree, Updates} from '../../models/petro/petroTable.model';

declare var gAppConfigSettings: IAppConfigSettings;

@Injectable()
export class XmlService {
    errorMessage: string;
    mode = 'Observable';
    constructor(private http: Http) {

    }

   public getTableSchema(schemaDef: string): Observable<any>{
       const url = gAppConfigSettings.WebApiUrl + 'xml/getTableSchema';

        let data = JSON.stringify(schemaDef);
        return this.http.post(url, data).map(r => {

            return r.json();
                    
        });
    }

   public updatePetroTableDefaultXml(xml: string, updates: Updates[]): Observable<any> {
        const url = gAppConfigSettings.WebApiUrl + 'xml/updateDefaultXml';
   
       let data = {     
           xml: xml,
           Updates: updates
        }
       
       return this.http.post(url, data)
           .map((res) => {               
               return res.json();
           },
            (error) => {               
                console.log(error);
            });
   }


   public validateDefaultXml(dictionaries:string[], xsd: string, xml:string): Observable<any> {
       const url = gAppConfigSettings.WebApiUrl + 'xml/validateXml';

       let data: any = {
           dictionaries: dictionaries,
           xsd: xsd,
           xml: xml
       }

       return this.http.post(url, data).map(r => {
          return r.json();
       },
           (error) => {
               console.log(error);
           });
   }

  
   ConvertTreeToXml(tree: Tree): string {
       let xml: string ='';
       if (tree.children != null) {
           if (tree.data && tree.data.attributes) {
               xml = xml.concat('<').concat(tree.label).concat(tree.data.attributes).concat('>');
           } else {
               xml = xml.concat('<').concat(tree.label).concat('>');
           }          
           tree.children.forEach(t => {
               xml = xml.concat(this.ConvertTreeToXml(t));
           });
           xml = xml.concat('</').concat(tree.label).concat('>');
       } else {
           xml = xml.concat(tree.label);
       }

       return xml;
   }

   public getTreeFromDefaultXml(defaultXml: string) {
       const url = gAppConfigSettings.WebApiUrl + 'xml/gettreenode';

       let data: any = {          
           defaultXml: defaultXml
       }

       return this.http.post(url, data).toPromise().then(r => {
           return r.json();
       },
           (error) => {
               console.log(error);
           });
   }

}