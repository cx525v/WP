using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Tree;

namespace Wp.CIS.LynkSystems.Model
{
    public class PetroTable
    {
        public int TableID { get; set; }
        public string TableName { get; set; }
        public int VersionID { get; set; }
        public bool Active { get; set; }
        public bool DefinitionOnly { get; set; }
        public string SchemaDef { get; set; }
        public string DefaultXML { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

    }
}
