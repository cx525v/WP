namespace Wp.CIS.LynkSystems.Model.Tree
{
    /// <summary>
    /// model for the data passed from UI
    /// </summary>
    public class UpdateXmlModel
    {
        /// <summary>
        /// property xml string that will be updated
        /// </summary>
        public string xml { get; set; }

        /// <summary>
        /// property the update contents of Xml
        /// </summary>
        public LeafData[] Updates { get; set; }
       
    }
}
