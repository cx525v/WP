using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class Demographics
    {

        public string Level { get; set; }
        public int AddressTypeID { get; set; }
        public int NameAddressID { get; set; }
        public string AddressType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipCode4 { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string DOB { get; set; }
        public string SSN { get; set; }
        //public int LID { get; set; }
        //public int LIDType { get; set; }
        public string LastFour { get; set; }
    }
}
