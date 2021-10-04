using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Models
{
    public class IACSShort
    {
        public string SocCardNum { get; set; }

        public string PassportNum { get; set; }

        public string LAccountNumber { get; set; }

        public string ANTPType { get; set; }

        public override bool Equals(object other)
        {
            var that = other as IACSShort;

            if (!(that is IACSShort))
            {
                return false;
            }

            return this.SocCardNum.Equals(that.SocCardNum, StringComparison.CurrentCultureIgnoreCase)
                && this.PassportNum.Equals(that.PassportNum, StringComparison.CurrentCultureIgnoreCase)
                && this.LAccountNumber.Equals(that.LAccountNumber, StringComparison.CurrentCultureIgnoreCase)
                && this.ANTPType.Equals(that.ANTPType, StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return $"~{PassportNum}~{SocCardNum}~{ANTPType}~{LAccountNumber}~".GetHashCode();
        }

        public override string ToString()
        {
            return $"Passport number: {PassportNum}," +
                $"SocCard number: {SocCardNum}," +
                $"Antp type: {ANTPType}," +
                $"L Account number: {LAccountNumber} \n";
        }
    }
}
