using System;
using System.Security.Cryptography;
using System.Text;
using Lng = ScreenpressoKG.Properties.Resources;
using Cfg = ScreenpressoKG.Properties.Settings;

namespace ScreenpressoKG
{
    class Serial
    {

        /*
            Serial > Edition Types
        */

        public enum Editions
        {
            ActivationKey,
            LicenseBoundToSoftwareName,
            LicenseBoundToHardDrive,
            LicenseCorporate,
            LicenseBoundToHardDrive2
        }

        /*
            Serial > Generate
        */

        public string Generate(Editions edition, int ver, string data)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(0x200);

            rsa.FromXmlString("<RSAKeyValue><Modulus>2FwAhdlB/Lw3csW+hov2cz33ZkaIP2rsl9GjJHgZgOrI/JvulnebHRvFrnMY4Z9TCvV7MT0rtTZ3aV1WFfGgpQ==</Modulus><Exponent>AQAB</Exponent><P>+MD97qTWvItm/OIwVrpE3PM6XNNznG4c0J8SnZnZ62E=</P><Q>3qlwFXADYCSsncBBVPrKDAfuzw6YgyItCfdlPm7238U=</Q><DP>Eujo5NlXEaIvRA4VyqICViGPUDsq0Lt2KU3OZnipnkE=</DP><DQ>uAdmofFAePgWyxMZbDkTQTpVQEEaAFgAzZnxzdY8qNk=</DQ><InverseQ>snkzqxdbYj2QSdGSzG9sEgvtgflbjFnLO68tRD+o0eo=</InverseQ><D>VwDYLPrqsCk32u1t6kkKN9lpTTV7wJTMw1hH1Hh/OPlzeyb/sFOW7V1lghRoxIzmOagdFGlSyt5jSeOBqxnTAQ==</D></RSAKeyValue>");

            string dt_format        = "MM/dd/yyyy";
            string v_edition        = ( ( int ) edition).ToString();
            string v_ver            = ver.ToString();
            string v_datetime       = DateTime.Today.ToString(dt_format);

            string[] license_arr    = new string[] { "[", v_edition, "]-[screenpressopro]-[", v_ver, "]-[", data, "]-[", v_datetime, "]" };
            string s                = string.Concat( license_arr );
            byte[] inArray          = rsa.SignData( Encoding.ASCII.GetBytes(s), new SHA1CryptoServiceProvider( ) );

            return (s + "-[" + Convert.ToBase64String(inArray) + "]");
        }

    }
}
