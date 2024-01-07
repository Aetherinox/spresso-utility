/*
    @app        : Screenpresso
    @repo       : https://github.com/Aetherinox/ScreenpressoKeygen
    @author     : Aetherinox
*/

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Diagnostics;
using Lng = ScreenpressoKG.Properties.Resources;
using Cfg = ScreenpressoKG.Properties.Settings;

namespace ScreenpressoKG
{
    class Serial
    {

        /*
            Define > Classes
        */

        private AppInfo AppInfo             = new AppInfo( );

        /*
             patch and target paths
        */

        private static string patch_launch_fullpath     = Process.GetCurrentProcess( ).MainModule.FileName;
        private static string patch_launch_dir          = Path.GetDirectoryName( patch_launch_fullpath );
        private static string patch_launch_exe          = Path.GetFileName( patch_launch_fullpath );
        private static string app_target_exe            = Cfg.Default.app_target_exe;

        /*
            Serial > Edition Types

            Do not change these, they are programmed into Screenpresso.
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
            Define > Classes
        */

        private Helpers Helpers = new Helpers( );

        /*
            Serial > Block Host

            @return : void
        */

        public void BlockHost( )
        {
            string host_path    = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.System ), "drivers\\etc\\hosts" );

            /*
                Windows host file
            */

            using ( StreamWriter w = File.AppendText( host_path ) )
            {
                w.WriteLine( Environment.NewLine                );
                w.WriteLine( "# Screenpresso Host Block"        );
                w.WriteLine( "0.0.0.0 screenpresso.com"         );
                w.WriteLine( "0.0.0.0 www.screenpresso.com"     );
                w.WriteLine( "0.0.0.0 secure.screenpresso.com"  );
                w.WriteLine( "0.0.0.0 stats.screenpresso.com"   );
                w.WriteLine( "0.0.0.0 18.65.3.28"               );
                w.WriteLine( "0.0.0.0 webapi.screenpresso.com"  );
                w.WriteLine( "0.0.0.0 18.155.192.82"            );
                w.WriteLine( "0.0.0.0 46.105.204.6"             );

                MessageBox.Show(
                    string.Format( Lng.msgbox_bhost_success_msg, host_path ),
                    Lng.msgbox_bhost_success_title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }

            /*
                full path to exe being searched for
                    X:\Path\To\Folder\Screenpresso.exe
            */

            string app_path_exe         = Helpers.FindApp( );

            /*
                Found no app file path, no need to continue blocks with the firewall.
                Do host entries only
            */

            if ( !File.Exists( app_path_exe ) )
            {

                MessageBox.Show(
                    string.Format( Lng.msgbox_bhost_fw_badpath_msg, app_path_exe ),
                    Lng.msgbox_bhost_fw_badpath_title,
                    MessageBoxButtons.OK, MessageBoxIcon.None
                );

                return;
            }

            /*
                splits full path into directory
                    X:\Path\To\Folder\Screenpresso.exe ->
                    X:\Path\To\Folder\
            */

            string app_path_dir         = Path.GetDirectoryName( app_path_exe );

            /*
                if for some reason, this utility can't edit the user's host file, we'll use Windows Firewall as a back up.
                Create two new firewall rules for inbound and outbound.
            */

            string fw_id_sha1           = Hash.GetSHA1Hash( app_path_exe );

            if ( string.IsNullOrEmpty( fw_id_sha1 ) )
                fw_id_sha1 = "0";

            string fw_id_name           = string.Format( "01-Screenpresso ({0})", fw_id_sha1 );
            string fw_id_desc           = string.Format( "Blocks Screenpresso from communicating with license server. Added by {0}", Cfg.Default.app_url_github );
            string fw_id_exe            = app_path_exe;

            /*
                firewall rules | inbound + outbound
            */

            string fwl_rule_block_in    = "New-NetFirewallRule -Name \"" + fw_id_name + "-Inbound (Auto-added)\" -DisplayName \"" + fw_id_name + "-Inbound (Auto-added)\" -Description \"" + fw_id_desc + "\" -Enabled True -Protocol Any -Profile Any -Direction Inbound -Program \"" + fw_id_exe + "\" -Action Block";
            string fwl_rule_block_out   = "New-NetFirewallRule -Name \"" + fw_id_name + "-Outbound (Auto-added)\" -DisplayName \"" + fw_id_name + "-Outbound (Auto-added)\" -Description \"" + fw_id_desc + "\" -Enabled True -Protocol Any -Profile Any -Direction Outbound -Program \"" + fw_id_exe + "\" -Action Block";

            /*
                run powershell query to add entries to firewall.
            */

            using ( PowerShell ps = PowerShell.Create( ) )
            {

                ps.AddScript( fwl_rule_block_in );
                ps.AddScript( fwl_rule_block_out );

                Collection<PSObject> PSOutput   = ps.Invoke( );
                StringBuilder sb                = new StringBuilder( );

                foreach ( PSObject PSItem in PSOutput )
                {
                    if ( PSItem != null )
                    {
                        Console.WriteLine( $"Output line: [{PSItem}]" );
                        sb.AppendLine( PSItem.ToString( ) );

                        if ( AppInfo.bIsDebug( ) )
                        {
                            MessageBox.Show(
                                string.Format( Lng.msgbox_debug_ps_bhost_qry_ok_msg, fwl_rule_block_in, fwl_rule_block_out ),
                                Lng.msgbox_debug_ps_bhost_qry_ok_title,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                            );
                        }

                    }
                }

                if ( ps.Streams.Error.Count > 0 )
                {
                    if ( AppInfo.bIsDebug( ) )
                    {
                        MessageBox.Show(
                            string.Format( Lng.msgbox_debug_ps_bhost_qry_alert_msg ),
                            Lng.msgbox_debug_ps_bhost_qry_alert_title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                    }
                }
            }

            /*
                Firewall success message
            */

            MessageBox.Show(
                string.Format( Lng.msgbox_bhost_fw_success_msg, app_path_exe ),
                Lng.msgbox_bhost_fw_success_title,
                MessageBoxButtons.OK, MessageBoxIcon.None
            );

        }

        /*
            Serial > Generate

            @arg    : enum edition
            @arg    : int version
            @arg    : str data

            @return : str
        */

        public string Generate( Editions edition, int version, string data )
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider( 0x200 );

            rsa.FromXmlString( "<RSAKeyValue><Modulus>2FwAhdlB/Lw3csW+hov2cz33ZkaIP2rsl9GjJHgZgOrI/JvulnebHRvFrnMY4Z9TCvV7MT0rtTZ3aV1WFfGgpQ==</Modulus><Exponent>AQAB</Exponent><P>+MD97qTWvItm/OIwVrpE3PM6XNNznG4c0J8SnZnZ62E=</P><Q>3qlwFXADYCSsncBBVPrKDAfuzw6YgyItCfdlPm7238U=</Q><DP>Eujo5NlXEaIvRA4VyqICViGPUDsq0Lt2KU3OZnipnkE=</DP><DQ>uAdmofFAePgWyxMZbDkTQTpVQEEaAFgAzZnxzdY8qNk=</DQ><InverseQ>snkzqxdbYj2QSdGSzG9sEgvtgflbjFnLO68tRD+o0eo=</InverseQ><D>VwDYLPrqsCk32u1t6kkKN9lpTTV7wJTMw1hH1Hh/OPlzeyb/sFOW7V1lghRoxIzmOagdFGlSyt5jSeOBqxnTAQ==</D></RSAKeyValue>" );

            string dt_format        = "MM/dd/yyyy";
            string v_edition        = ( ( int ) edition ).ToString( );
            string v_ver            = version.ToString( );
            string v_datetime       = DateTime.Today.ToString( dt_format );

            string[] license_arr    = new string[] { "[", v_edition, "]-[screenpressopro]-[", v_ver, "]-[", data, "]-[", v_datetime, "]" };
            string s                = string.Concat( license_arr );
            byte[] inArray          = rsa.SignData( Encoding.ASCII.GetBytes( s ), new SHA1CryptoServiceProvider( ) );

            string key              = s + "-[" + Convert.ToBase64String( inArray ) + "]";

            /*
                Returns a blank key for some reason
            */

            if ( String.IsNullOrEmpty( key ) )
            {
                StatusBar.Update( Lng.status_keygen_fail );
                return Lng.txt_License_resp_keygen_fail;
            }

            /*
                key generated successfully
            */

            StatusBar.Update( Lng.status_keygen_succ );

            return ( key );
        }

    }
}
