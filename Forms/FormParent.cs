using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScreenpressoKG.Forms;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Lng = ScreenpressoKG.Properties.Resources;
using Cfg = ScreenpressoKG.Properties.Settings;

namespace ScreenpressoKG
{

    public partial class FormParent : Form, IReceiver
    {

        #region "Declarations"

            /*
                Define > Classes
            */

            private AppInfo AppInfo             = new AppInfo( );
            private Helpers Helpers             = new Helpers( );
            readonly private Serial Serial      = new Serial( );

            /*
                Define > Internal > Helper
            */

            internal Helpers Helper
            {
                set     { Helpers = value;  }
                get     { return Helpers;   }
            }

            /*
                Define > Mouse
            */

            private bool mouseDown;
            private Point lastLocation;

            /*
                Could not find MobaXterm.exe

                patch_launch_fullpath       : Full path to exe
                patch_launch_dir            : Directory only
                patch_launch_exe            : Patcher exe filename only
            */

            static private string patch_launch_fullpath = Process.GetCurrentProcess( ).MainModule.FileName;
            static private string patch_launch_dir      = Path.GetDirectoryName( patch_launch_fullpath );
            static private string patch_launch_exe      = Path.GetFileName( patch_launch_fullpath );

            /*
                Define > updates
            */

            private bool bUpdateAvailable               = false;

            /*
                Manifest > Json
            */

            public class Manifest
            {
                public string version { get; set; }
                public string name { get; set; }
                public string author { get; set; }
                public string description { get; set; }
                public string url { get; set; }
                public IList<string> scripts { get; set; }
            }

        #endregion

        #region "Main Window: Initialize"

            /*
                Frame > Parent
            */

            public FormParent( )
            {
                InitializeComponent( );
                StatusBar.InitializeReceiver( this );

                this.status_Strip.Renderer = new StatusBar_Renderer( );

                /*
                    Product, trademark, etc.
                */

                string ver                      = AppInfo.ProductVersionCore.ToString( );
                string product                  = AppInfo.Title;
                string tm                       = AppInfo.Trademark;

                /*
                    Form Control Buttons
                */

                btn_Close.Parent                = imgHeader;
                btn_Close.BackColor             = Color.Transparent;

                btn_Minimize.Parent             = imgHeader;
                btn_Minimize.BackColor          = Color.Transparent;

                /*
                    Headers
                */

                lbl_HeaderName.Parent           = imgHeader;
                lbl_HeaderName.BackColor        = Color.Transparent;
                lbl_HeaderName.Text             = product;

                lbl_HeaderSub.Parent            = imgHeader;
                lbl_HeaderSub.BackColor         = Color.Transparent;
                lbl_HeaderSub.Text              = "v" + ver + " by " + tm;

                /*
                    Host Block Segment
                */

                lbl_HostBlocker_Title.Text      = Lng.lbl_bhost_pnl_title;
                lbl_HostBlocker_Desc.Text       = string.Format( Lng.lbl_bhost_pnl_intro, product );
                btn_DoBlock.Text                = Lng.btn_bhost_block;
                btn_HostView.Text               = Lng.btn_bhost_viewfile;

                /*
                    User / License Boxes
                */

                lbl_User.Text                   = Lng.lbl_generate_name;
                txt_User.PlaceholderText        = Cfg.Default.app_def_name;
                lbl_LicenseKey.Text             = Lng.lbl_generate_license;

                /*
                    Buttons
                */

                btnGenerate.Text                = Lng.btn_license_generate;
                btnCopy.Text                    = Lng.btn_license_copy;

            }

            /*
                Frame > Parent > Load
            */

            private void FormParent_Load( object sender, EventArgs e )
            {
                mnu_Main.Renderer   = new ToolStripProfessionalRenderer( new mnu_Main_ColorTable( ) );
                StatusBar.Update    ( string.Format( Lng.statusbar_generate ) );

                /*
                    update checker > json
                        views the data stored at https://github.com/Aetherinox/ScreenpressoKeygen/blob/master/Manifest/manifest.json
                */

                using ( var webClient = new System.Net.WebClient( ) )
                {
                    var json = webClient.DownloadString( Cfg.Default.app_url_manifest );

                    if( json == null )
                        return;

                    JavaScriptSerializer serializer     = new JavaScriptSerializer( ); 
                    Manifest manifest                   = serializer.Deserialize<Manifest>( json );

                    /*
                        Check if update is available for end-user
                    */

                    bool bUpdate        = AppInfo.UpdateAvailable( manifest.version );
                    string ver_curr     = AppInfo.PublishVersion;

                    /*
                        determines if the update notification appears
                    */

                    if ( bUpdate || AppInfo.bIsDebug( ) )
                        bUpdateAvailable = true;

                    /*
                        update checker
                    */

                    if ( ( bUpdateAvailable && !Cfg.Default.bShowedUpdates ) )
                    {
                        Cfg.Default.bShowedUpdates = true;

                        var result = MessageBox.Show( string.Format( Lng.msgbox_update_msg, manifest.version, Cfg.Default.app_softw_name ),
                            string.Format( Lng.msgbox_update_title, ver_curr, manifest.version ),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation
                        );

                        string answer = result.ToString( ).ToLower( );

                        if ( answer == "yes" )
                            System.Diagnostics.Process.Start( Cfg.Default.app_url_github + "/releases/" );
                    }

                }

            }

        #endregion

        #region "Main Window: Control Buttons"

            /*
                Control Buttons
                    ->  Minimize
                    ->  Maximize
                    ->  Close

                Icons:  http://modernicons.io/segoe-mdl2/cheatsheet/
                Font:   Segoe MDL2 Assets
            */

            /*
                Window > Button > Minimize > Click
            */

            private void btn_Window_Minimize_Click( object sender, EventArgs e )
            {
                this.WindowState = FormWindowState.Minimized;
            }

            /*
                Window > Button > Minimize > Mouse Enter
            */

            private void btn_Window_Minimize_MouseEnter( object sender, EventArgs e )
            {
                btn_Minimize.ForeColor = Color.FromArgb(222, 31, 100);
            }

            /*
                Window > Button > Minimize > Mouse Leave
            */

            private void btn_Window_Minimize_MouseLeave( object sender, EventArgs e )
            {
                btn_Minimize.ForeColor = Color.FromArgb(255, 255, 255);
            }

            /*
                Window > Button > Close > Click
            */

            private void btn_Window_Close_Click( object sender, EventArgs e )
            {
                Application.Exit();
            }

            /*
                Window > Button > Close > Mouse Enter
            */

            private void btn_Window_Close_MouseEnter( object sender, EventArgs e )
            {
                btn_Close.ForeColor = Color.FromArgb(222, 31, 100);
            }

            /*
                Window > Button > Close > Mouse Leave
            */

            private void btn_Window_Close_MouseLeave( object sender, EventArgs e )
            {
                btn_Close.ForeColor = Color.FromArgb(255, 255, 255);
            }

        #endregion

        #region "Main Window: Dragging"

            private void MainForm_MouseDown(object sender, MouseEventArgs e)
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void MainForm_MouseUp(object sender, MouseEventArgs e)
            {
                mouseDown = false;
            }

            private void MainForm_MouseMove(object sender, MouseEventArgs e)
            {
                if (mouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                    this.Update();
                }
            }

        #endregion

        #region "Header"

        /*
            Header Image
        */

            private void imgHeader_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown       = true;
                lastLocation    = e.Location;
            }

            private void imgHeader_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void imgHeader_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

        /*
            Header > Name Label
        */

            private void lbl_HeaderName_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void lbl_HeaderName_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void lbl_HeaderName_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

        /*
            Header > Sub Label
        */

            private void lbl_HeaderSub_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void lbl_HeaderSub_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void lbl_HeaderSub_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

        #endregion

        #region "Top Menu"

            /*
                Top Menu > Color Overrides
            */

            public class mnu_Main_ColorTable : ProfessionalColorTable
            {
                /*
                    Gets the starting color of the gradient used when
                    a top-level System.Windows.Forms.ToolStripMenuItem is pressed.
                */

                public override Color MenuItemPressedGradientBegin => Color.FromArgb( 255, 55, 55, 55 );

                /*
                    Gets the end color of the gradient used when a top-level
                    System.Windows.Forms.ToolStripMenuItem is pressed.
                */

                public override Color MenuItemPressedGradientEnd => Color.FromArgb( 255, 55, 55, 55 );

                /*
                    Gets the border color to use with a
                    System.Windows.Forms.ToolStripMenuItem.
                */

                public override Color MenuItemBorder => Color.FromArgb( 0, 45, 45, 45 );

                /*
                    Gets the starting color of the gradient used when the
                    System.Windows.Forms.ToolStripMenuItem is selected.
                */

                public override Color MenuItemSelectedGradientBegin => Color.FromArgb( 255, 222, 31, 103 );

                /*
                    Gets the end color of the gradient used when the
                    System.Windows.Forms.ToolStripMenuItem is selected.
                */

                public override Color MenuItemSelectedGradientEnd => Color.FromArgb( 255, 222, 31, 103 );

                /*
                    Gets the solid background color of the
                    System.Windows.Forms.ToolStripDropDown.
                */

                public override Color ToolStripDropDownBackground => Color.FromArgb( 255, 40, 40, 40 );

                /*
                    Top Menu > Image > Start Gradient Color
                */

                public override Color ImageMarginGradientBegin => Color.FromArgb( 255, 222, 31, 103 );

                /*
                    Top Menu > Image > Middle Gradient Color
                */

                public override Color ImageMarginGradientMiddle => Color.FromArgb( 0, 222, 31, 103 );

                /*
                    Top Menu > Image > End Gradient Color
                */

                public override Color ImageMarginGradientEnd => Color.FromArgb( 0, 222, 31, 103 );

                /*
                    Top Menu > Shadow Effect
                */

                public override Color SeparatorDark => Color.FromArgb( 0, 255, 255, 255 );

                /*
                    Top Menu > Border Color
                */

                public override Color MenuBorder => Color.FromArgb( 0, 45, 45, 45 );

                /*
                    Top Menu > Item Hover BG Color
                */

                public override Color MenuItemSelected => Color.FromArgb( 255, 222, 31, 103 );
            }

            /*
                Top Menu > Paint
            */

            private void mnu_Main_Paint( object sender, PaintEventArgs e )
            {
                Graphics g      = e.Graphics;
                Color backColor = Color.FromArgb( 35, 255, 255, 255 );
                var imgSize     = mnu_Main.ClientSize;

                e.Graphics.FillRectangle( new SolidBrush( backColor ), 1, 1, imgSize.Width - 2, 1 );
                e.Graphics.FillRectangle( new SolidBrush( backColor ), 1, imgSize.Height - 2, imgSize.Width - 2, 1 );
            }

            /*
                Top Menu > File > Exit
            */

            private void mnu_Sub_Exit_Click( object sender, EventArgs e )
            {
                Application.Exit( );
            }

            /*
                Top Menu > Help > Contribute
            */

            private void mnu_Cat_Contribute_Click( object sender, EventArgs e )
            {
                this.Hide( );

                FormContribute to   = new FormContribute( );
                to.TopMost          = true;
                to.Show( );
            }

            /*
               Top Menu > Help > Check for Updates
            */

            private void mnu_Sub_Updates_Click( object sender, EventArgs e )
            {
                System.Diagnostics.Process.Start( Cfg.Default.app_url_github );
            }

            /*
                Top Menu > Updates > Update Indicator
            */

            private void mnu_Sub_Updates_Paint( object sender, PaintEventArgs e )
            {
                if ( bUpdateAvailable )
                {
                    var imgSize     = mnu_Sub_Updates.Size;
                    var bmp         = new Bitmap( Lng.notify_01 );

                    e.Graphics.DrawImage( bmp, 7,  ( imgSize.Height / 2 ) - ( 24 / 2 ), 24, 24 );
                }
            }

            /*
               Top Menu > Help > x509 Certificate Validation
            */

            private void mnu_Sub_Validate_Click( object sender, EventArgs e )
            {
                string exe_target = System.AppDomain.CurrentDomain.FriendlyName;
                if ( !File.Exists( exe_target ) )
                {

                    MessageBox.Show( string.Format( "Could not find executable's location. Aborting validation\n\nFilename: \n{0}", exe_target ),
                        "Integrity Check: Aborted",
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );

                    return;
                }

                string x509_cert = Helper.x509_Thumbprint( exe_target );

                /*
                    x509 certificate

                    Add integrity validation. Ensure the resource DLL has been signed by the developer,
                    otherwise cancel the patching step.
                */

                if ( x509_cert != "0" )
                {

                    /* certificate: resource file  signed */

                    if ( x509_cert.ToLower( ) == Cfg.Default.app_dev_piv_thumbprint.ToLower( ) )
                    {

                        /* certificate: resource file signed and authentic */

                        MessageBox.Show( string.Format( "Successfully validated that this patch is authentic, continuing...\n\nCertificate Thumbprint: \n{0}", x509_cert ),
                            "Integrity Check Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        /* certificate: resource file signed but not by developer */

                        MessageBox.Show( string.Format( "The fails associated to this patch have a signature, however, it is not by the developer who wrote the patch, aborting...\n\nCertificate Thumbprint: \n{0}", x509_cert ),
                            "Integrity Check Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                    }
                }
                else
                {
                    /* certificate: resource file not signed at all */

                    MessageBox.Show( string.Format( "The files for this activator are not signed and may be fake from another source. Files from this activator's developer will ALWAYS be signed.\n\nEnsure you downloaded this patch from the developer.\n\nFailed File(s):\n     {0}", exe_target ),
                        "Integrity Check Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
                }
            }

            /*
                Top Menu > Separator
                    Separates "Exit" from the other items in "About" dropdown.
            */

            private void mnu_Help_Sep_1_Paint( object sender, PaintEventArgs e )
            {
                ToolStripSeparator toolStripSeparator = (ToolStripSeparator)sender;

                int width           = toolStripSeparator.Width;
                int height          = toolStripSeparator.Height;
                Color backColor     = Color.FromArgb( 255, 222, 31, 103 );

                // Fill the background.
                e.Graphics.FillRectangle( new SolidBrush( backColor ), 0, 0, width, height );
            }

            /*
                Top Menu > Help > About
            */

            private void mnu_Sub_About_Click( object sender, EventArgs e )
            {
                this.Hide( );

                FormAbout to    = new FormAbout( );
                to.TopMost      = true;
                to.Show( );
            }

        #endregion

        #region "Body: Block Host"

            /*
                 Group box for activation + generated response boxes

                 paint custom borders because default borders looks like shit
             */

            private void box_BlockGroup_Paint(object sender, PaintEventArgs e)
            {
                // border color
                Pen p = new Pen( Color.FromArgb( 75, 75, 75 ) );

                e.Graphics.DrawLine( p, 0, 1, 0, e.ClipRectangle.Height - 1 );          // left
                e.Graphics.DrawLine( p, 1, 1, e.ClipRectangle.Width - 1, 1 );           // top
                e.Graphics.DrawLine( p, e.ClipRectangle.Width - 1, 1, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1 );        // right
                e.Graphics.DrawLine( p, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1, 0, e.ClipRectangle.Height - 1 );       // bottom
            }

            /*
                Button > Block > On Click
            */

            private void btn_DoBlock_Click( object sender, EventArgs e )
            {

                var result = MessageBox.Show
                (
                    Lng.msgbox_block_msg,
                    Lng.msgbox_bhost_title,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

                string answer = result.ToString( ).ToLower( );

                /*
                    Block Host > Cancel
                */

                if ( answer != "yes" )
                {
                    MessageBox.Show
                    (
                        Lng.msgbox_bhost_cancel_msg,
                        Lng.msgbox_bhost_cancel_title,
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );

                    return;
                }

                /*
                    Block Host > Continue
                */

                Serial.BlockHost( );
            }

            private void btn_HostView_Click( object sender, EventArgs e )
            {
                string etc_path = @"C:\Windows\System32\drivers\etc";
                Process.Start( "explorer.exe", etc_path );
            }

        #endregion

        #region "Body: Action Buttons"

            /*
                Button > Generate
            */

            private void btn_Generate_Click( object sender, EventArgs e )
            {
                if ( string.IsNullOrEmpty( txt_User.Value ) )
                {
                    MessageBox.Show( Lng.msgbox_generate_invalidname_msg, Lng.msgbox_generate_invalidname_title,
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
                }
                else
                {
                    // force placeholder off
                    txt_LicenseKey.isPlaceholder    = false;
                    txt_LicenseKey.Value            = Serial.Generate( Editions.LicenseCorporate, new Random( ).Next( 0x270f ), this.txt_User.Value );
                }
            }

            /*
                 Button > Copy generated serial number
             */

            private void btn_Copy_Click( object sender, EventArgs e )
            {

                Debug.WriteLine( txt_LicenseKey.Value );

                if (string.IsNullOrEmpty( txt_LicenseKey.Value ) )
                {
                    MessageBox.Show( Lng.msgbox_copy_invlicense_msg, Lng.msgbox_copy_invlicense_title,
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );

                    StatusBar.Update( string.Format( Lng.statusbar_copy_invalidlicense ) );
                }
                else
                {
                    Clipboard.SetText( txt_LicenseKey.Value );
                    StatusBar.Update( string.Format( Lng.statusbar_copy_succ ) );
                }
            }

        #endregion

        #region "Status Bar"

            /*
                status bar in lower part of the main interface.
                updated when certain actions are completed to inform the user.
            */

            /*
                Status Bar > Color Table (Override)
            */

            public class StatusBar_ClrTable : ProfessionalColorTable
            {
                public override Color StatusStripGradientBegin => Color.FromArgb( 35, 35, 35 );
                public override Color StatusStripGradientEnd => Color.FromArgb( 35, 35, 35 );
            }

            /*
                Status Bar > Renderer
                Override colors
            */

            public class StatusBar_Renderer : ToolStripProfessionalRenderer
            {
                public StatusBar_Renderer( )
                    : base( new StatusBar_ClrTable( ) ) { }

                protected override void OnRenderToolStripBorder( ToolStripRenderEventArgs e )
                {
                    if ( !( e.ToolStrip is StatusStrip ) )
                        base.OnRenderToolStripBorder( e );
                }
            }

            /*
                Statusbar > Paint
            */

            private void status_Strip_Paint( object sender, PaintEventArgs e )
            {
                Graphics g                  = e.Graphics;
                Color backColor             = Color.FromArgb( 35, 255, 255, 255 );
                var imgSize                 = status_Strip.ClientSize;
                e.Graphics.FillRectangle    ( new SolidBrush( backColor ), 1, 1, imgSize.Width - 2, 2 );
            }

            /*
                Statusbar > Mouse Actions
            */

            private void status_Strip_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void status_Strip_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void status_Strip_MouseMove( object sender, MouseEventArgs e )
            {
                if ( mouseDown )
                {
                    this.Location = new Point(
                        ( this.Location.X - lastLocation.X ) + e.X,
                        ( this.Location.Y - lastLocation.Y ) + e.Y
                    );

                    this.Update( );
                }
            }

            /*
                Receiver > Update Status
            */

            public void Status( string message )
            {
                lbl_StatusOutput.Text = message;
                status_Strip.Refresh( );
            }

        #endregion

    }
}
