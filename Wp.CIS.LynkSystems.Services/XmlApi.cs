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
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
using System.Threading;
using Worldpay.Logging.Providers.Log4Net.Facade;
namespace Wp.CIS.LynkSystems.Services
{
    /// <summary>
    /// The service to update default Xml values, convert Xml to Tree data, validate xml schema and etc
    /// </summary>
    public class XmlApi : IXmlApi
    {
        private readonly ILoggingFacade _loggingFacade;
        
        public XmlApi(ILoggingFacade loggingFacade)
        {
            this._loggingFacade = loggingFacade;

        }
        /// <summary>
        /// modify default xml values
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Update(UpdateXmlModel model)
        {
            try
            {
                string xml = model.xml;
                foreach (LeafData update in model.Updates)
                {
                    xml = UpdateXml(xml, update);
                }
                return xml;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="update"></param>
        private void UpdateElement(XElement element, LeafData update)
        {
            try
            {
                if (element.HasElements)
                {
                    foreach (var ele in element.Elements())
                    {
                        UpdateElement(ele, update);
                    }
                }
                else
                {
                    if (element.Name.LocalName.Equals(update.colName))
                    {
                        element.Value = update.newValue;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        private string UpdateXml(string xml, LeafData update)
        {
            try
            {
                TreeNode node = new TreeNode();
                var xDoc = XDocument.Parse(xml);
                XElement table = xDoc.Elements().First();
                int i = 0;
                foreach (var ele in table.Elements())
                {
                    if (ele.HasElements)
                    {
                        if (i == update.rowNum)
                        {
                            UpdateElement(ele, update);
                        }
                        i++;
                    }
                    else
                    {
                        if (ele.Name.LocalName.Equals(update.colName))
                        {
                            ele.Value = update.newValue;
                            break;
                        }
                    }
                }

                xml = xDoc.ToString();
                return xml;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xsd"></param>
        /// <returns></returns>
        private string GetTargetNameSpaceAttribute(XDocument xsd)
        {
            try
            {
                if (xsd.Root.Attribute("targetNamespace") == null) {
                    return string.Empty;
                }
                else
                   return xsd.Root.Attribute("targetNamespace").Value;
            }
            catch 
            {
                throw;
            }
          
        }

        /// <summary>
        /// remove xml encoding for db compatible 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string RemoveEncoding(string xml)
        {
            try
            {
                XDocument xDoc = XDocument.Parse(xml);
                if (xDoc.Declaration != null)
                    xDoc.Declaration.Encoding = null;
                return xDoc.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// validate XML file in schema and dictionary
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="xsd"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
            
        public XmlValidationMessage IsValid(string[] dictionaries , string xsd, string xml)
        {
            XmlValidationMessage message = new XmlValidationMessage();

            if (string.IsNullOrEmpty(xsd) || string.IsNullOrEmpty(xml))
            {
                message.IsValid = false;
                message.ErrorMessage = "Invalid xml or schema";
                return message;
            }

            try
            {
                XDocument xdocXsd = XDocument.Parse(xsd);
                string xsdtargetNamespace = GetTargetNameSpaceAttribute(xdocXsd);
                bool errors = false;
                string errorMessage = string.Empty;
                XmlSchemaSet schemas = new XmlSchemaSet();

                if (dictionaries != null)
                {
                    foreach (string dict in dictionaries)
                    {
                        XDocument xdocDist = XDocument.Parse(dict);
                        string dictTargetNamespace = GetTargetNameSpaceAttribute(xdocDist);
                        schemas.Add(dictTargetNamespace, XmlReader.Create(new StringReader(dict)));
                    }
                }

                schemas.Add(xsdtargetNamespace, XmlReader.Create(new StringReader(xsd)));

                XDocument xdoc = XDocument.Parse(xml);
                xdoc.Validate(schemas, (o, e) =>
                {
                    errorMessage = e.Message;
                    errors = true;
                });

                message.ErrorMessage = errorMessage;
                message.IsValid = !errors;
            }
            catch (Exception e)
            {
                message.ErrorMessage = e.Message;
                message.IsValid = false;
                _loggingFacade.LogExceptionAsync(e, string.Empty, LogLevels.Error, "Error in XmlApi IsValid()", CancellationToken.None);

            }

            return message;           
        }
              

       private int rowNum = 0;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="xml"></param>
       /// <returns></returns>
        private  TreeNode Parse(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            try
            {
                XDocument xDoc = XDocument.Parse(xml);
                XElement element = xDoc.Elements().First();
                rowNum = 0;
                return ParseElement(element,true);
            }
            catch 
            {
                throw;
            }

            //return null;
        }

        /// <summary>
        /// get attribute string of xml element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private string GetAttributeString(XElement element)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string attr = element.Attributes().ToString();
                foreach (XAttribute attribute in element.Attributes())
                {
                    sb.Append(" ");
                    sb.Append(attribute.ToString());
                }
                return sb.ToString();
            }
            catch
            {
                throw;
            }
        }

        private TreeNode ParseCommentNode(XNode xnode)
        {
            try
            {
                var comment = xnode as XComment;
                if (comment != null)
                {
                    TreeNode commentNode = new TreeNode();
                    commentNode.label = comment.ToString();
                    commentNode.data = new LeafData
                    {
                        isComment = true
                    };
                    return commentNode;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        private List<TreeNode> ParseCommentNodes(XElement element)
        {
            try
            {
                List<TreeNode> nodes = new List<TreeNode>();
                foreach (var node in element.Nodes())
                {
                    TreeNode td = ParseCommentNode(node);
                    if (td != null)
                        nodes.Add(td);
                }

                return nodes;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Parse xml element to Tree nodes
        /// </summary>
        /// <param name="element"></param>
        /// <param name="increaseRow"></param>
        /// <returns></returns>
        private TreeNode ParseElement(XNode xnode, bool increaseRow)
        {
            try
            {
                TreeNode node = new TreeNode();
                var element = xnode as XElement;
                if (element != null)
                {
                    node.label = element.Name.LocalName;

                    if (element.HasAttributes)
                    {
                        node.data = new LeafData
                        {
                            attributes = GetAttributeString(element)
                        };
                    }

                    if (element.HasElements)
                    {
                        List<TreeNode> children = new List<TreeNode>();
                        foreach (XNode child in element.Nodes())
                        {
                            children.Add(ParseElement(child, false));
                            var theNode = child as XElement;
                            if (increaseRow && theNode != null)
                            {
                                rowNum++;
                            }
                        }
                        node.children = children;
                    }
                    else
                    {
                        TreeNode cellNode = new TreeNode();
                        cellNode.label = element.Value;
                        cellNode.data = new LeafData
                        {
                            colName = node.label,
                            rowNum = rowNum,
                            oldValue = element.Value
                        };
                        cellNode.icon = "fa-pencil-square-o";
                        List<TreeNode> cellChildren = new List<TreeNode>();
                        cellChildren.Add(cellNode);
                        node.children = cellChildren;
                    }
                }
                else
                {
                    var comment = xnode as XComment;
                    if (comment != null)
                    {
                        TreeNode td = ParseCommentNode(xnode);
                        node.label = td.label;
                        node.data = td.data;
                    }
                }

                return node;
            }
            catch {
                throw;
            }

        }

        //convert xml string to Tree object
        public Tree ConvertToTree(string defaultXml)
        {
            try
            {
                TreeNode node = Parse(defaultXml);
                Tree tree = new Tree();
                List<TreeNode> list = new List<TreeNode>();
                list.Add(node);
                tree.data = list;
                return tree;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get table name from xml schema
        /// </summary>
        /// <param name="xsd"></param>
        /// <returns></returns>
        private string GetTableNameFromSchema(XDocument xsd)
        {
            try
            {
                var ns = xsd.Root.GetDefaultNamespace();
                var prefix = xsd.Root.GetNamespaceOfPrefix("xs");
                var table = xsd.Root.Element(prefix + "element");
                if (table.Attribute("name") == null)
                    return string.Empty;
                else
                    return table.Attribute("name").Value;
            }
            catch(Exception) 
            {
                throw;
            }          
        }

        /// <summary>
        /// retrieve the key of table
        /// </summary>
        /// <param name="xsd"></param>
        /// <returns></returns>
        private string GetTableKey(XDocument xsd)
        {
            try
            {
                var ns = xsd.Root.GetDefaultNamespace();
                var prefix = xsd.Root.GetNamespaceOfPrefix("xs");
                var table = xsd.Root.Element(prefix + "element");
                if (table.Element(prefix + "key") == null)
                {
                    return string.Empty;
                }
                else
                { 
                    var key = table.Element(prefix + "key")
                                        .Elements(prefix + "field").FirstOrDefault();
                    if (key.Attribute("xpath") == null)
                        return string.Empty;
                    else
                        return key.Attribute("xpath").Value.Replace("this:", "");
                }
            }
            catch(Exception e)
            {
                _loggingFacade.LogExceptionAsync(e, string.Empty, LogLevels.Error, "Error in XmlApi GetTableKey()", CancellationToken.None);
            }

            return string.Empty;
        }

        /// <summary>
        /// get xml file schemalocation
        /// </summary>
        /// <param name="xsd"></param>
        /// <returns></returns>
        private string GetSchemaLocation(XDocument xsd)
        {
            try
            {
                var ns = xsd.Root.GetDefaultNamespace();
                var prefix = xsd.Root.GetNamespaceOfPrefix("xs");
                var import = xsd.Root.Element(prefix + "import");
                if (import.Attribute("schemaLocation") == null)
                    return string.Empty;
                else
                    return import.Attribute("schemaLocation").Value;
            }
            catch (Exception e)
            {
                _loggingFacade.LogExceptionAsync(e, string.Empty, LogLevels.Error, "Error in XmlApi GetSchemaLocation()", CancellationToken.None);
            }

            return string.Empty;
        }

        /// <summary>
        /// retrieve xml schema properties
        /// </summary>
        /// <param name="schemaDef"></param>
        /// <returns></returns>
        public TableProperty GetTableProperty(string schemaDef)
        {
            try
            {
                XDocument xsd = XDocument.Parse(schemaDef);
                return new TableProperty
                {
                    tableName = GetTableNameFromSchema(xsd),
                    key = GetTableKey(xsd),
                    schemaLocation = GetSchemaLocation(xsd)
                };
            }
            catch (Exception)
            {
                throw;              
            }
        }

        public List<PetroTable> GetTablesFromXml(string xml)
        {
            try
            {
                List<PetroTable> list = new List<PetroTable>();
                if (string.IsNullOrEmpty(xml))
                {
                    return list;
                }
                xml = "<root>" + xml + "</root>";
                XDocument xDoc = XDocument.Parse(xml);
                foreach (XElement element in xDoc.Root.Elements())
                {
                    list.Add(GetTable(element));
                }

                return list;
            }
            catch
            {
                throw;
            }
        }

        private PetroTable GetTable(XElement element)
        {
            try
            {
                PetroTable table = new PetroTable();
                foreach (XAttribute attribute in element.Attributes())
                {
                    if (attribute.Name.LocalName.Equals("Active"))
                    {
                        table.Active = attribute.Value == "1";
                    }
                    else if (attribute.Name.LocalName.Equals("DefinitionOnly"))
                    {
                        table.DefinitionOnly = attribute.Value == "1";
                    }
                    else if (attribute.Name.LocalName.Equals("EffectiveDate"))
                    {
                        table.EffectiveDate = DateTime.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("TableID"))
                    {
                        table.TableID = Int32.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("TableName"))
                    {
                        table.TableName = attribute.Value;
                    }
                }

                foreach (var ele in element.Elements())
                {
                    if (ele.Name.LocalName.Equals("SchemaDef"))
                    {
                        table.SchemaDef = ele.ToString().Replace("<SchemaDef>", "").Replace("</SchemaDef>", "");
                    }
                    else if (ele.Name.LocalName.Equals("DefaultXML"))
                    {
                        table.DefaultXML = ele.ToString().Replace("<DefaultXML>", "").Replace("</DefaultXML>", "").Replace("<DefaultXML />", "");
                    }
                }
                return table;
            }
            catch
            {
                throw;
            }
        }

        public PetroTable GetTableFromXml(string xml)
        {
            try
            {
                PetroTable table = new PetroTable();
                if (string.IsNullOrEmpty(xml))
                {
                    return table;
                }
                XDocument xDoc = XDocument.Parse(xml);
                XElement root = xDoc.Root;
                foreach (XAttribute attribute in root.Attributes())
                {
                    if (attribute.Name.LocalName.Equals("Active"))
                    {
                        table.Active = attribute.Value == "1";
                    }
                    else if (attribute.Name.LocalName.Equals("DefinitionOnly"))
                    {
                        table.DefinitionOnly = attribute.Value == "1";
                    }
                    else if (attribute.Name.LocalName.Equals("EffectiveDate"))
                    {
                        table.EffectiveDate = DateTime.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("TableID"))
                    {
                        table.TableID = Int32.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("TableName"))
                    {
                        table.TableName = attribute.Value;
                    }
                }

                foreach (var element in root.Elements())
                {
                    if (element.Name.LocalName.Equals("SchemaDef"))
                    {
                        table.SchemaDef = element.Value;
                    }
                    else if (element.Name.LocalName.Equals("DefaultXML"))
                    {
                        table.DefaultXML = element.Value;
                    }
                }

                return table;
            }
            catch
            {
                throw;
            }

        }

        public List<EPSMapping> GetMappingsFromXml(string xml)
        {
            try
            {
                List<EPSMapping> mappingList = new List<EPSMapping>();
                if (string.IsNullOrEmpty(xml))
                {
                    return mappingList;
                }
                xml = "<root>" + xml + "</root>";
                XDocument xDoc = XDocument.Parse(xml);
                foreach (XElement element in xDoc.Root.Elements())
                {
                    mappingList.Add(GetMapping(element));
                }
                return mappingList;
            }
            catch
            {
                throw;
            }
        }

        private EPSMapping GetMapping(XElement element)
        {
            try
            {
                EPSMapping mapping = new EPSMapping();
                foreach (XAttribute attribute in element.Attributes())
                {
                    if (attribute.Name.LocalName.Equals("MappingID"))
                    {
                        mapping.mappingID = Int32.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("PDLFlag"))
                    {
                        mapping.pdlFlag = attribute.Value == "1";
                    }
                    else if (attribute.Name.LocalName.Equals("ParamID"))
                    {
                        mapping.paramID = Int32.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("WorldPayFieldDescription"))
                    {
                        mapping.worldPayFieldDescription = attribute.Value;
                    }
                    else if (attribute.Name.LocalName.Equals("EffectiveBeginDate"))
                    {
                        mapping.effectiveBeginDate = DateTime.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("EffectiveEndDate"))
                    {
                        mapping.effectiveEndDate = DateTime.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("VIPERTableName"))
                    {
                        mapping.viperTableName = attribute.Value;
                    }
                    else if (attribute.Name.LocalName.Equals("VIPERFieldName"))
                    {
                        mapping.viperFieldName = attribute.Value;
                    }
                    else if (attribute.Name.LocalName.Equals("ViperCondition"))
                    {
                        mapping.viperCondition = attribute.Value;
                    }
                    else if (attribute.Name.LocalName.Equals("CharStartIndex"))
                    {
                        mapping.charStartIndex = Int32.Parse(attribute.Value);
                    }
                    else if (attribute.Name.LocalName.Equals("CharLength"))
                    {
                        mapping.charLength = Int32.Parse(attribute.Value);
                    }
                }

                return mapping;
            }
            catch
            {
                throw;
            }
        }
        public EPSMapping GetMappingFromXml(string xml)
        {
            try
            {
                EPSMapping mapping = new EPSMapping();
                if (string.IsNullOrEmpty(xml))
                {
                    return mapping;
                }
                XDocument xDoc = XDocument.Parse(xml);
                return GetMapping(xDoc.Root);
            }
            catch
            {
                throw;
            }
        }

        public VersionAudit GetVersionFromXMl(string xml)
        {
            try
            {
                VersionAudit version = new VersionAudit();
                if (string.IsNullOrEmpty(xml))
                {
                    return version;
                }
                XDocument xDoc = XDocument.Parse(xml);
                XElement root = xDoc.Root;
                foreach (XAttribute attribute in root.Attributes())
                {
                    if (attribute.Name.LocalName.Equals("VersionID"))
                    {
                        version.VersionID = attribute.Value;
                    }
                    else if (attribute.Name.LocalName.Equals("VersionDescription"))
                    {
                        version.VersionDescription = attribute.Value;
                    }
                    else if (attribute.Name.LocalName.Equals("ObsoleteIndicator"))
                    {
                        version.ObsoleteIndicator = attribute.Value == "1";
                    }
                }

                return version;
            }
            catch
            {
                throw;
            }
        }
    }
    
}
