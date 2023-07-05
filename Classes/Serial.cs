using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScreenpressoKG
{
    class Serial
    {

        public string Generate(Type type, int version, string data )
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(0x200);
            provider.FromXmlString("<RSAKeyValue><Modulus>2FwAhdlB/Lw3csW+hov2cz33ZkaIP2rsl9GjJHgZgOrI/JvulnebHRvFrnMY4Z9TCvV7MT0rtTZ3aV1WFfGgpQ==</Modulus><Exponent>AQAB</Exponent><P>+MD97qTWvItm/OIwVrpE3PM6XNNznG4c0J8SnZnZ62E=</P><Q>3qlwFXADYCSsncBBVPrKDAfuzw6YgyItCfdlPm7238U=</Q><DP>Eujo5NlXEaIvRA4VyqICViGPUDsq0Lt2KU3OZnipnkE=</DP><DQ>uAdmofFAePgWyxMZbDkTQTpVQEEaAFgAzZnxzdY8qNk=</DQ><InverseQ>snkzqxdbYj2QSdGSzG9sEgvtgflbjFnLO68tRD+o0eo=</InverseQ><D>VwDYLPrqsCk32u1t6kkKN9lpTTV7wJTMw1hH1Hh/OPlzeyb/sFOW7V1lghRoxIzmOagdFGlSyt5jSeOBqxnTAQ==</D></RSAKeyValue>");
            string[] strArray = new string[] { "[", ((int)type).ToString(), "]-[screenpressopro]-[", version.ToString(), "]-[", data, "]-[", DateTime.Today.ToString("MM/dd/yyyy"), "]" };
            string s = string.Concat(strArray);
            byte[] inArray = provider.SignData(Encoding.ASCII.GetBytes(s), new SHA1CryptoServiceProvider());

            return (s + "-[" + Convert.ToBase64String(inArray) + "]");
        }

        public enum Type
        {
            ActivationKey,
            LicenseBoundToSoftwareName,
            LicenseBoundToHardDrive,
            LicenseCorporate,
            LicenseBoundToHardDrive2
        }

    }
}
