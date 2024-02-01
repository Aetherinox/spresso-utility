/*
    @app        : Screenpresso
    @repo       : https://github.com/Aetherinox/ScreenpressoKeygen
    @author     : Aetherinox
*/

#region "Using"
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScreenpressoKG.Forms;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Reflection;
using System.Net;
using Res = ScreenpressoKG.Properties.Resources;
using Cfg = ScreenpressoKG.Properties.Settings;
#endregion

namespace ScreenpressoKG
{

    public partial class FormParent : Form, IReceiver
    {

        #region "Define: Fileinfo"

            /*
                Define > File Name
                    utilized with logging
            */

            readonly static string log_file = "FormParent.cs";

        #endregion

        #region "Define: General"

           /*
                Define > Classes
            */

            private AppInfo AppInfo             = new AppInfo( );
            private Helpers Helpers             = new Helpers( );
            readonly private Serial Serial      = new Serial( );

            /*
                Define > Debug Activation
            */

            static int i_DebugClicks                        = 0;
            private System.Windows.Forms.Timer DebugTimer   = new System.Windows.Forms.Timer( );
            Stopwatch SW_DebugRemains                       = new Stopwatch( );

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
                Form > Register Object

                    used to show / hide form without creating a new instance

                @usage      : FormParent.Object = this;
            */

            private static FormParent Obj;

            public static FormParent Object
            {
                get
                {
                    if ( Obj == null )
                    {
                        Obj = new FormParent( );
                    }
                    return Obj;
                }
                set { Obj = value; }
            }

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
                public string piv { get; set; }
                public string gpg { get; set; }
                public IList<string> products { get; set; }
            }

        #endregion

        #region "Define: Custom Menu Items"

            ToolStripItem item_sep      = new ToolStripSeparator( );
            ToolStripItem item_debug    = new ToolStripMenuItem( );

        #endregion

        #region "Method: Element Dragging"

            private void obj_DragWindow_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown       = true;
                lastLocation    = e.Location;
            }

            /*
                Main Form > Mouse Up
                deals with moving form around on screen
            */

            private void obj_DragWindow_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown       = false;
            }

            /*
                Main Form > Mouse Move
                deals with moving form around on screen
            */

            private void obj_DragWindow_MouseMove( object sender, MouseEventArgs e )
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

        #region "Main Window: Initialize"

            /*
                Frame > Parent
            */

            public FormParent( )
            {
                Stopwatch sw_LoadTime = new Stopwatch();
                sw_LoadTime.Start( );
                InitializeComponent( );
                SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true );

                typeof( Panel ).InvokeMember( "DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, 
                null, this, new object[] { true } );

                SuspendLayout( );

                /*
                    Register Form Object
                */

                FormParent.Object = this;

                /*
                    Initialize Receiver
                */

                StatusBar.InitializeReceiver( this );

                /*
                    Renderers
                */

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

                lbl_HostBlocker_Title.Text      = Res.lbl_bhost_pnl_title;
                lbl_HostBlocker_Desc.Text       = string.Format( Res.lbl_bhost_pnl_intro, product );
                btn_DoBlock.Text                = Res.btn_bhost_block;
                btn_HostView.Text               = Res.btn_bhost_viewfile;

                /*
                    User / License Boxes
                */

                lbl_User.Text                   = Res.lbl_generate_name;
                txt_User.PlaceholderText        = Cfg.Default.app_def_name;
                lbl_LicenseKey.Text             = Res.lbl_generate_license;

                /*
                    Buttons
                */

                btnGenerate.Text                = Res.btn_license_generate;
                btnCopy.Text                    = Res.btn_license_copy;

                /*
                    Debug Timer
                */

                sw_LoadTime.Stop            ( );
                TimeSpan ts                 = sw_LoadTime.Elapsed;
                string sw_TimeElapsed       = String.Format( "{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10 );

                Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Form", String.Format( "FormParent : {0} - Elapsed Loadtime {1}", System.Reflection.MethodBase.GetCurrentMethod( ).Name, sw_TimeElapsed ) );

            }

            /*
                Frame > Parent > Load
            */

            private async void FormParent_Load( object sender, EventArgs e )
            {
                mnu_Main.Renderer   = new ToolStripProfessionalRenderer( new mnu_Main_ColorTable( ) );
                StatusBar.Update    ( string.Format( Res.statusbar_generate ) );

                /*
                    update checker > json
                        views the data stored at https://github.com/Aetherinox/ScreenpressoKeygen/blob/master/Manifest/manifest.json
                */

                using ( var webClient = new System.Net.WebClient( ) )
                {
                    StatusBar.Update( string.Format( Res.status_patch_default ) );

                    await Task.Run( ( ) => CheckUpdates( Cfg.Default.app_url_manifest ) );
                    Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Form", String.Format( "FormParent_Load : {0}", System.Reflection.MethodBase.GetCurrentMethod( ).Name ) );

                    /*
                        Debug Timer
                        forces the debug activation timer to expire every X seconds.
                        A user must click on the header image at least 7 times in a matter of 7 seconds in order for debug mode to activate.

                        see method DebugTimer_Tick for functionality
                    */

                    DebugTimer.Interval     = ( 10 * ( 100 * Cfg.Default.app_debug_clicks_activate ) );
                    DebugTimer.Tick         += new EventHandler( DebugTimer_Tick );
                    DebugTimer.Start        ( );
                    SW_DebugRemains.Start   ( );
                }

            }


            /*
                Debug Timer > Tick
                This termines how long a user has to click the header image in order to enable developer mode.
                This is easier than creating yet another menu item.
            */

            private void DebugTimer_Tick( object sender, EventArgs e )
            {
                i_DebugClicks = 0;
                SW_DebugRemains.Restart( );

                Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Debug ] Timer", String.Format( "Debug timer SW_DebugRemains expired after {0} seconds -- resetting", Cfg.Default.app_debug_clicks_activate ) );
            }

            /*
                Task > Check for Updates

                views the data stored at https://github.com/Aetherinox/filezilla-patcher/blob/master/Manifest/manifest.json
            */

            private async Task CheckUpdates( string uri )
            {
                try
                {
                    var webClient       = new WebClient( );
                    var json            = await webClient.DownloadStringTaskAsync( uri );

                    JavaScriptSerializer serializer     = new JavaScriptSerializer( ); 
                    Manifest manifest                   = serializer.Deserialize<Manifest>( json );

                    /*
                        validate json results from github
                    */

                    if ( manifest != null )
                        Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Uplink", String.Format( "{0} : {1}", "FormParent.CheckUpdates", "Successful connection - populated manifest data" ) );
                    else
                       Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Uplink", String.Format( "{0} : {1}", "FormParent.CheckUpdates", "Successful connection - missing manifest data" ) );

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

                    #pragma warning disable CS4014
                    Task.Factory.StartNew( () =>
                    {
                        if ( ( bUpdateAvailable && !Settings.bShowedUpdates ) )
                        {
                            Settings.bShowedUpdates = true;

                            var result = MessageBox.Show
                            ( 
                                new Form( ) { TopMost = true, TopLevel = true, StartPosition = FormStartPosition.CenterScreen },
                                string.Format( Res.msgbox_update_msg, manifest.version, Cfg.Default.app_name ),
                                string.Format( Res.msgbox_update_title, ver_curr, manifest.version ),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation
                            );

                            if ( result.ToString( ).ToLower( ) == "yes" )
                                System.Diagnostics.Process.Start( Cfg.Default.app_url_github + "/releases/" );
                        }
                     });
                }
                catch ( WebException e )
                {
                    Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Uplink", String.Format( "{0} : {1}", "FormParent.CheckUpdates", "Failed connection - exception" ) );
                    Log.Send( log_file, 0, "", String.Format( "{0}", e.Message ) );
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
                    var bmp         = new Bitmap( Res.notify_01 );

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
                    Res.msgbox_block_msg,
                    Res.msgbox_bhost_title,
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
                        Res.msgbox_bhost_cancel_msg,
                        Res.msgbox_bhost_cancel_title,
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
                    MessageBox.Show( Res.msgbox_generate_invalidname_msg, Res.msgbox_generate_invalidname_title,
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
                    MessageBox.Show( Res.msgbox_copy_invlicense_msg, Res.msgbox_copy_invlicense_title,
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );

                    StatusBar.Update( string.Format( Res.statusbar_copy_invalidlicense ) );
                }
                else
                {
                    Clipboard.SetText( txt_LicenseKey.Value );
                    StatusBar.Update( string.Format( Res.statusbar_copy_succ ) );
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
                Receiver > Update Status
            */

            public void Status( string message )
            {
                lbl_StatusOutput.Text = message;
                status_Strip.Refresh( );
            }

        #endregion

        #region "Debug Mode: Header"

            /*
                Debug Mode > Header Click
                    if the user clicks the header a certain number of times in a short duration, they will enable
                    debugging mode.
            */

            private void lbl_HeaderName_MouseClick( object sender, MouseEventArgs e )
            {

                /*
                    add +1 to clicks
                */

                i_DebugClicks++;

                /*
                    don't go higher than 7, otherwise each click after 7 will re-show confirmation dialog. Start back at 0
                */

                int i_DebugRemains = Cfg.Default.app_debug_clicks_activate - i_DebugClicks;
                if ( i_DebugClicks > Cfg.Default.app_debug_clicks_activate )
                {
                    i_DebugClicks = 0;
                    return;
                }

                /*
                    timer > remaining
                */

                int remains         = Cfg.Default.app_debug_clicks_activate - Convert.ToInt32( SW_DebugRemains.Elapsed.TotalSeconds );


                /*
                    prompt to enable / disable debug
                */

                if ( Settings.app_bDevmode )
                    Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Debug ] Trigger", String.Format( "Disable debug with {0} more clicks -- {1} seconds remain", i_DebugRemains, remains ) );
                else
                    Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Debug ] Trigger", String.Format( "Enable debug with {0} more clicks -- {1} seconds remain", i_DebugRemains, remains ) );

                /*
                    wait until 7 clicks are done in X seconds
                */

                if ( i_DebugClicks >= Cfg.Default.app_debug_clicks_activate )
                {

                    if ( Settings.app_bDevmode )
                    {

                        /*
                            Debug > disable
                        */

                        var resp_input = MessageBox.Show
                        (
                            new Form( ) { TopMost = true, TopLevel = true, StartPosition = FormStartPosition.CenterScreen },
                            Res.msgbox_debug_egg_disable_msg,
                            Res.msgbox_debug_egg_disable_title,
                            MessageBoxButtons.YesNo, MessageBoxIcon.None
                        );

                        if ( resp_input.ToString( ).ToLower( ) == "yes" )
                        {
                            Settings.app_bDevmode = false;
                            Program.DisableDebugConsole( );
                        }

                    }
                    else
                    {

                        /*
                            Debug > enable
                        */

                        string log_path = Log.GetStorageFile( );
                        var resp_input = MessageBox.Show
                        (
                            new Form( ) { TopMost = true, TopLevel = true, StartPosition = FormStartPosition.CenterScreen },
                            String.Format( Res.msgbox_debug_egg_enable_msg, log_path ),
                            Res.msgbox_debug_egg_enable_title,
                            MessageBoxButtons.YesNo, MessageBoxIcon.None
                        );

                        if ( resp_input.ToString( ).ToLower( ) == "yes" )
                        {
                            Settings.app_bDevmode = true;
                            Program.EnableDebugConsole( );

                            //mnu_Cat_Help.DropDownItems.Clear();

                            mnu_Cat_Help.DropDownItems.Add( item_sep );
                            mnu_Cat_Help.DropDownItems.Add( item_debug );
                        }
                    }
                }
            }

        #endregion
    }
}
