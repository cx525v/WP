using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Wp.CIS.LynkSystems.Model.Tree;
using Wp.CIS.LynkSystems.Model;
using System.Text;
using System.Threading;

public interface IXmlApi
{
    string Update(UpdateXmlModel model);
    string RemoveEncoding(string xml);
    XmlValidationMessage IsValid(string[] dictionaries, string xsd, string xml);
    Tree ConvertToTree(string defaultXml);
    TableProperty GetTableProperty(string schemaDef);
    List<PetroTable> GetTablesFromXml(string xml);
    PetroTable GetTableFromXml(string xml);
    List<EPSMapping> GetMappingsFromXml(string xml);
    EPSMapping GetMappingFromXml(string xml);

    VersionAudit GetVersionFromXMl(string xml);
}