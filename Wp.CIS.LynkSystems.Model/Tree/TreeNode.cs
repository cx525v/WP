using System.Collections.Generic;

namespace Wp.CIS.LynkSystems.Model.Tree
{
    /// <summary>
    /// Tree Node model for PrimeNG Tree
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// label property
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// icon property
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// Tree leaf data
        /// </summary>
        public LeafData data { get; set; }

        /// <summary>
        /// children of node
        /// </summary>
        public List<TreeNode> children { get; set; }
    }

    /// <summary>
    /// Data model for Tree leaf
    /// </summary>
    public class LeafData
    {
        public int rowNum { get; set; }
        public string colName { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
        public string path { get; set; }
        public TableKey key { get; set; }
        public string attributes { get; set; }
        public bool isComment { get; set; }
    }

    /// <summary>
    /// The model to represent key of table
    /// </summary>
    public class TableKey
    {
        /// <summary>
        /// key name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// key value
        /// </summary>
        public string Value { get; set; }
    }


    public class Tree
    {
        /// <summary>
        /// array property
        /// </summary>
        public List<TreeNode> data { get; set; }
    }
}
