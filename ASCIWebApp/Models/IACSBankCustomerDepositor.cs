using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Models
{
    public partial class IACSBankCustomerDepositor
    {
        public int IACSBankCustomerDepositorId { get; set; }
        public byte NNumber { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string SecondName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Sex { get; set; }

        public string Nationality { get; set; }

        public string ANTPType { get; set; }
        public string SocCardNum { get; set; }

        public IACSBankCustomerDepositorPassportTypeName PassportTypeName { get; set; }

        public string RegistrationCountry { get; set; }

        public uint RegistrationDistrictName { get; set; }

        public string RegistrationAddress { get; set; }

        public string LocationCountry { get; set; }

        public uint LocationDistrictName { get; set; }

        public string LocationAddress { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("e-mail")]
        public string Email { get; set; }
    }
}
