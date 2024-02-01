/*
    @app        : Screenpresso
    @repo       : https://github.com/Aetherinox/ScreenpressoKeygen
    @author     : Aetherinox
*/

#region "Using"
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Threading.Tasks;
using static ScreenpressoKG.FormParent;
using System.Net;
using System.Web.Script.Serialization;
using Lng = ScreenpressoKG.Properties.Resources;
using Cfg = ScreenpressoKG.Properties.Settings;
#endregion

namespace ScreenpressoKG
{

    public partial class FormAbout : Form
    {

        #region "Define: Fileinfo"

            /*
                Define > File Name
                    utilized with logging
            */

            readonly static string log_file = "FormAbout.cs";

        #endregion

        #region "Define: General"

            /*
                Define > Classes
            */

            private Helpers Helpers     = new Helpers( );

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
                Version info
            */

            private string ver                  = AppInfo.ProductVersionCore.ToString( );
            private string product              = AppInfo.Title;
            private string tm                   = AppInfo.Trademark;

        #endregion

        #region "Generate Readme"

            /*
                Frame > Parent
            */

            public string GetReadme(string product, string version, string developer)
            {

                string str_about =
@"{0}
Version {1}
{2}

This is for educational purposes only. I hold no responsibility for people doing bad things with it.

If you wish to view the source code, click the Github link above.

The keygen is free for anyone to use. I try to make stuff that isn't like the typical keygens out there. No loud annoying ass music, no ads, no weird color schemes that question if you're under the influence of shrooms.

INSTRUCTIONS
    - In the top box, press the 'BLOCK HOST' button
    - Type a name you wish to use (call yourself whatever).
    - Press Generate button
    - Copy the generated license key.
    - Launch Screenpresso and find the 'About' / 'Manage Activation' button.
    - Paste your generated license key into Screenpresso.



CERTIFICATE THUMBPRINT
These are specifically associated to the developer of this program.

To verify that this program is safe and unmodified by others,
right-click on the EXE file: 
    -> Click PROPERTIES menu item
    -> DIGITAL SIGNATURES tab
    -> DETAILS button
    -> VIEW CERTIFICATE button
    -> DETAILS tab

Scroll down and locate the THUMBPRINT field.
Match the thumbprint in the textbox with the thumbprint below.

If you do not see a Digital Signatures tab or if the thumbprints do not match, close and delete this program, it is not mine. My programs are free of malware and other harmful 'gifts'. This thumbprint ensures that you're using the correct program and it has not been tampered with.

GPG KEY ID
This key is used to sign the releases on Github.com, all commits are also signed with this key id.

";

                return string.Format( str_about, product, version, developer );
            }

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

            public FormAbout( )
            {
                InitializeComponent();
                SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true );

                typeof( Panel ).InvokeMember( "DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, 
                null, this, new object[] { true } );

                SuspendLayout( );

                /*
                    Form Control Buttons
                */

                btn_Close.Parent                = imgHeader;
                btn_Close.BackColor             = Color.Transparent;

                /*
                    Headers
                */

                lbl_HeaderName.Parent           = imgHeader;
                lbl_HeaderName.BackColor        = Color.Transparent;
                lbl_HeaderName.Text             = product;

                lbl_HeaderSub.Parent            = imgHeader;
                lbl_HeaderSub.BackColor         = Color.Transparent;
                lbl_HeaderSub.Text              = Lng.lbl_about_HeaderSub;

                /*
                    Button Links
                */

                lnk_TPB.Text                    = "⠀⠀⠀⠀⠀⠀  ⠀";
                lnk_Github.Text                 = "⠀⠀⠀⠀⠀⠀     ⠀";

                lnk_TPB.Parent                  = imgHeader;
                lnk_TPB.BackColor               = Color.Transparent;

                lnk_Github.Parent               = imgHeader;
                lnk_Github.BackColor            = Color.Transparent;

                lbl_Version.Parent              = imgHeader;
                lbl_Version.BackColor           = Color.Transparent;
                lbl_Version.ForeColor           = Color.Transparent;
                lbl_Version.Text                = "⠀⠀⠀⠀⠀⠀ ⠀⠀ ⠀⠀⠀⠀";


                pnl_HeaderBtm.Parent            = imgHeader;
                pnl_HeaderBtm.BackColor         = Color.Transparent;

                /*
                    About Readme
                */

                txt_Terms.Text                  = GetReadme(product, ver, tm);
                txt_Terms.Value                 = GetReadme(product, ver, tm);

                /*
                    GPG / PIV Fields
                */

                lbl_Dev_PIV_Thumbprint.Text     = Lng.lbl_about_thumbprint;
                lbl_Dev_GPG_KeyID.Text          = Lng.lbl_about_gpg;

                txt_Dev_PIV_Thumbprint.Value    = Cfg.Default.app_dev_piv_thumbprint;
                txt_Dev_GPG_KeyID.Value         = Cfg.Default.app_dev_gpg_keyid;

            }

            private async void FormAbout_Load(object sender, EventArgs e)
            {
                await Task.Run( ( ) => FetchJson( Cfg.Default.app_url_manifest ) );
                Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Form", String.Format( "FormAbout_Load : {0}", System.Reflection.MethodBase.GetCurrentMethod( ).Name ) );
            }

            /*
                This optimizes the form for better loading of elements.
                You must set the Form Properties AUTO-VALIDATE to "disabled".
            */

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                    return cp;
                }
            }

            /*
                Task > Fetch Json

                    views the data stored at https://github.com/Aetherinox/ScreenpressoKeygen/blob/master/Manifest/manifest.json
            */

            private async Task FetchJson( string uri )
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
                        Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Uplink", String.Format( "{0} : {1}", "FormAbout.FetchJson", "Successful connection - populated manifest data" ) );
                    else
                       Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Uplink", String.Format( "{0} : {1}", "FormAbout.FetchJson", "Successful connection - missing manifest data" ) );

                    /*
                        Check if update is available for end-user
                    */

                    if ( !string.IsNullOrEmpty( manifest.piv ) )
                        txt_Dev_PIV_Thumbprint.Value    = manifest.piv;

                    if ( !string.IsNullOrEmpty( manifest.gpg ) )
                        txt_Dev_GPG_KeyID.Value         = manifest.gpg;

                }
                catch ( WebException e )
                {
                    Log.Send( log_file, new System.Diagnostics.StackTrace( true ).GetFrame( 0 ).GetFileLineNumber( ), "[ App.Interface ] Uplink", String.Format( "{0} : {1}", "FormAbout.FetchJson", "Failed connection - exception" ) );
                    Log.Send( log_file, 0, "", String.Format( "{0}", e.Message ) );
                }
            }

        #endregion

        #region "Main Window: Controls"

        /*
            Window > Button > Close
        */

        private void btn_Window_Close_Click(object sender, EventArgs e)
            {

                FormParent to       = new FormParent( );
                to.Show( );

                this.Close();
            }

            /*
                Window > Button > Close > Mouse Enter
            */

            private void btn_Window_Close_MouseEnter(object sender, EventArgs e)
            {
                btn_Close.ForeColor = Color.FromArgb(222, 31, 100);
            }

            /*
                Window > Button > Close > Mouse Leave
            */

            private void btn_Window_Close_MouseLeave(object sender, EventArgs e)
            {
                btn_Close.ForeColor = Color.FromArgb(255, 255, 255);
            }

        #endregion

        #region "Header"

        /*
            Header Image
        */

            private void imgHeader_Paint( object sender, PaintEventArgs e )
            {
                Graphics g          = e.Graphics;
                Color backColor     = Color.FromArgb( 65, 255, 255, 255 );
                var imgSize         = imgHeader.ClientSize;

                e.Graphics.FillRectangle( new SolidBrush( backColor ), 1, imgSize.Height - 2, imgSize.Width - 2, 2 );
            }

        #endregion

        #region "Header Links"

        /*
            The header contains three levels. Two are links, and one is the version number.
            The header also includes a semi-transparent panel to dim the background. In order to do this
            and have the labels appear properly, we need to do some hacky stuff.

                - Set the original link labels to have blank text
                - Add a Paint hook to each link label and draw the semi-transparent box
                - Manually draw the text on top of the transparent background
                - Track when the mouse enters / leaves the button so that text will be highlighted
        */

        /*
            Links & label settings
        */

        private bool _bTPB_Hover            = false;
            private bool _bGithub_Hover         = false;
            private string lnk_TPB_label        = " " + Lng.lnk_about_tpb;
            private string lnk_Github_Label     = " " + Lng.lnk_about_github;
            private Color clr_Filler            = Color.FromArgb( 125, 0, 0, 0 );
            private Color clr_Text              = Color.FromArgb( 255, 255, 128 );

            /*
                Header Bottom Panel
            */

            private void pnl_HeaderBtm_Paint( object sender, PaintEventArgs e )
            {
                Graphics g                  = e.Graphics;
                Color clr_Border            = Color.FromArgb( 35, 255, 255, 255 );
                Color clr_Filler            = Color.FromArgb( 125, 0, 0, 0 );
                var imgSize                 = pnl_HeaderBtm.ClientSize;
                e.Graphics.FillRectangle    ( new SolidBrush( clr_Border ), 1, 1, imgSize.Width - 1, 1 );
                e.Graphics.FillRectangle    ( new SolidBrush( clr_Filler ), 2, 2, imgSize.Width - 4, imgSize.Height - 4 );
            }

            /*
                Label > Version
            */

            private void lbl_Version_Paint( object sender, PaintEventArgs e )
            {
                Graphics g                  = e.Graphics;
                Color clr_Filler            = Color.FromArgb( 125, 0, 0, 0 );
                var imgSize                 = lbl_Version.ClientSize;
                e.Graphics.FillRectangle    ( new SolidBrush( clr_Filler ), 0, 0, imgSize.Width - 0, imgSize.Height - 0 );

                var format                  = new StringFormat() { Alignment = StringAlignment.Far };
                var rect                    = new RectangleF( 0, 0, imgSize.Width, imgSize.Height );

                string teaxt = "v" + ver + " by " + tm;
                using ( Font font1 = new Font( "Segoe UI", 10, FontStyle.Regular, GraphicsUnit.Point ) )
                {
                    e.Graphics.DrawString( teaxt, font1, Brushes.White, rect, format );
                }
            }

            /*
                Link > The Pirate Bay
            */


            private void lnk_TPB_Paint( object sender, PaintEventArgs e )
            {
                Graphics g                  = e.Graphics;
                SolidBrush bru_Text         = new SolidBrush( clr_Text );
                var imgSize                 = lnk_Github.ClientSize;
                e.Graphics.FillRectangle    ( new SolidBrush( clr_Filler ), 0, 0, imgSize.Width - 0, imgSize.Height - 0 );

                var format                  = new StringFormat( ) { Alignment = StringAlignment.Near };
                var rect                    = new RectangleF( 0, 0, imgSize.Width - 6, imgSize.Height );

                FontStyle action            = FontStyle.Regular;

                if ( _bTPB_Hover )
                    action = FontStyle.Underline;

                using ( Font font1 = new Font( "Segoe UI", 10, action, GraphicsUnit.Point ) )
                {
                    e.Graphics.DrawString( lnk_TPB_label, font1, bru_Text, rect, format );
                }
            }

            private void lnk_TPB_MouseEnter( object sender, EventArgs e )
            {
                _bTPB_Hover = true;
                lnk_TPB.Refresh( );
            }

            private void lnk_TPB_MouseLeave( object sender, EventArgs e )
            {
                _bTPB_Hover = false;
                lnk_TPB.Refresh( );
            }

            private void lnk_TPB_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
            {
                System.Diagnostics.Process.Start( Cfg.Default.app_url_tpb );
            }

            /*
                Link > Github
            */

            private void lnk_Github_Paint( object sender, PaintEventArgs e )
            {
                Graphics g                  = e.Graphics;
                SolidBrush bru_Text         = new SolidBrush( clr_Text );
                var imgSize                 = lnk_Github.ClientSize;
                e.Graphics.FillRectangle    ( new SolidBrush( clr_Filler ), 0, 0, imgSize.Width - 0, imgSize.Height - 0 );

                var format                  = new StringFormat() { Alignment = StringAlignment.Near };
                var rect                    = new RectangleF( 0, 0, imgSize.Width - 3, imgSize.Height );

                FontStyle action            = FontStyle.Regular;

                if ( _bGithub_Hover )
                    action = FontStyle.Underline;

                using ( Font font1 = new Font( "Segoe UI", 10, action, GraphicsUnit.Point ) )
                {
                    e.Graphics.DrawString( lnk_Github_Label, font1, bru_Text, rect, format );
                }
            }

            private void lnk_Github_MouseEnter( object sender, EventArgs e )
            {
                _bGithub_Hover = true;
                lnk_Github.Refresh( );
            }

            private void lnk_Github_MouseLeave( object sender, EventArgs e )
            {
                _bGithub_Hover = false;
                lnk_Github.Refresh( );
            }

            private void lnk_Github_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
            {
                System.Diagnostics.Process.Start( Cfg.Default.app_url_github );
            }

        #endregion
    }
}
