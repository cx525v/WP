namespace Wp.CIS.LynkSystems.Model
{
    public class Parameters
    {
        public int ParameterID { get; set; }
        public string ParamName { get; set; }
        public string ParameterDesc { get; set; }
        public string DataType { get; set; }
        public decimal NumVal { get; set; }
        public bool BitVal { get; set; }
        public string StringVal { get; set; }
        public bool IsCardSpecific { get; set; }
        public bool PDL { get; set; }
        public bool UseSpace { get; set; }
        public bool IsStratus { get; set; }
        public bool IsVericentre { get; set; }
        public bool StratusMultiplier { get; set; }
        public bool IsCustomerDefault { get; set; }
        public bool IsOLADefault { get; set; }
    }
}
