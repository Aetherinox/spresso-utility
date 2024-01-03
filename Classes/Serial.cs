using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Windows.Forms;
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
            Serial > Block Host
        */

        public void BlockHost( )
        {
            string host_path = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.System ), "drivers\\etc\\hosts" );

            using ( StreamWriter w = File.AppendText( host_path ) )
            {
                w.WriteLine( Environment.NewLine );
                w.WriteLine( "# Screenpresso Host Block"        );
                w.WriteLine( "0.0.0.0 screenpresso.com"         );
                w.WriteLine( "0.0.0.0 www.screenpresso.com"     );
                w.WriteLine( "0.0.0.0 secure.screenpresso.com"  );
                w.WriteLine( "0.0.0.0 stats.screenpresso.com"   );
                w.WriteLine( "0.0.0.0 18.65.3.28"               );

                MessageBox.Show(
                    string.Format( Lng.msgbox_block_success_msg, host_path.ToString() ),
                    Lng.msgbox_block_success_title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
        }

        /*
            Serial > Generate
        */

        public string Generate( Editions edition, int ver, string data )
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider( 0x200 );

            rsa.FromXmlString( "<RSAKeyValue><Modulus>2FwAhdlB/Lw3csW+hov2cz33ZkaIP2rsl9GjJHgZgOrI/JvulnebHRvFrnMY4Z9TCvV7MT0rtTZ3aV1WFfGgpQ==</Modulus><Exponent>AQAB</Exponent><P>+MD97qTWvItm/OIwVrpE3PM6XNNznG4c0J8SnZnZ62E=</P><Q>3qlwFXADYCSsncBBVPrKDAfuzw6YgyItCfdlPm7238U=</Q><DP>Eujo5NlXEaIvRA4VyqICViGPUDsq0Lt2KU3OZnipnkE=</DP><DQ>uAdmofFAePgWyxMZbDkTQTpVQEEaAFgAzZnxzdY8qNk=</DQ><InverseQ>snkzqxdbYj2QSdGSzG9sEgvtgflbjFnLO68tRD+o0eo=</InverseQ><D>VwDYLPrqsCk32u1t6kkKN9lpTTV7wJTMw1hH1Hh/OPlzeyb/sFOW7V1lghRoxIzmOagdFGlSyt5jSeOBqxnTAQ==</D></RSAKeyValue>" );

            string dt_format        = "MM/dd/yyyy";
            string v_edition        = ( ( int ) edition).ToString( );
            string v_ver            = ver.ToString( );
            string v_datetime       = DateTime.Today.ToString( dt_format );

            string[] license_arr    = new string[] { "[", v_edition, "]-[screenpressopro]-[", v_ver, "]-[", data, "]-[", v_datetime, "]" };
            string s                = string.Concat( license_arr );
            byte[] inArray          = rsa.SignData( Encoding.ASCII.GetBytes( s ), new SHA1CryptoServiceProvider( ) );

            string key              = s + "-[" + Convert.ToBase64String( inArray ) + "]";

            if ( String.IsNullOrEmpty( key ) )
            {
                StatusBar.Update( Lng.status_keygen_fail );
                return Lng.resp_keygen_fail;
            }

            StatusBar.Update( Lng.status_keygen_success );

            return ( key );
        }

    }
}
