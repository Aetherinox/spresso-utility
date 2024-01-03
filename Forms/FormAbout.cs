using System;
using System.Drawing;
using System.Windows.Forms;
using Lng = ScreenpressoKG.Properties.Resources;
using Cfg = ScreenpressoKG.Properties.Settings;
using ScreenpressoKG.Forms;

namespace ScreenpressoKG
{

    public partial class FormAbout : Form
    {

        /*
            Define > Mouse
        */

        private bool mouseDown;
        private Point lastLocation;

        #region "Generate Readme"

            /*
                Readme
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
   - Type in a name you wish to use (call yourself whatever).
   - Press ""Generate"" button
   - Copy the generated license key.
   - Paste license key into Screenpresso.

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

                return string.Format(str_about, product, version, developer);
            }

        #endregion

        #region "Main Window: Initialize"

        public FormAbout()
            {
                InitializeComponent( );

                string ver                      = AppInfo.ProductVersionCore.ToString( );
                string product                  = AppInfo.Title;
                string tm                       = AppInfo.Trademark;

                lbl_HeaderName.Parent           = imgHeader;
                lbl_HeaderName.BackColor        = Color.Transparent;

                lbl_Version.Parent              = imgHeader;
                lbl_Version.BackColor           = Color.Transparent;

                btn_Close.Parent                = imgHeader;
                btn_Close.BackColor             = Color.Transparent;

                lbl_HeaderSub.Parent            = imgHeader;
                lbl_HeaderSub.BackColor         = Color.Transparent;

                lnk_TPBLink.Parent              = imgHeader;
                lnk_TPBLink.BackColor           = Color.Transparent;

                lnk_Github.Parent               = imgHeader;
                lnk_Github.BackColor            = Color.Transparent;

                lbl_Version.Text                = "v" + ver + " by " + tm;
                lbl_HeaderName.Text             = product;

                txt_Terms.Value                 = GetReadme(product, ver, tm);
                txt_Terms.Text                  = GetReadme(product, ver, tm);

                lbl_HeaderSub.Text              = Lng.about_hdr_desc;
                lnk_TPBLink.Text                = Lng.about_lnk_tpb;
                lnk_Github.Text                 = Lng.about_lnk_github;

                lbl_Dev_PIV_Thumbprint.Text     = Lng.about_lbl_thumbprint;
                lbl_Dev_GPG_KeyID.Text          = Lng.about_lbl_gpg;

                txt_Dev_PIV_Thumbprint.Value    = Cfg.Default.app_dev_piv_thumbprint;
                txt_Dev_GPG_KeyID.Value         = Cfg.Default.app_dev_gpg_keyid;

            }

            /*
                Tweak to fix frame flickering
            */

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x02000000;  // enable WS_EX_COMPOSITED
                    return cp;
                }
            } 

            private void FormAbout_Load(object sender, EventArgs e)
            {

            }

        #endregion

        #region "Main Window: Dragging"

            private void FormAbout_MouseDown(object sender, MouseEventArgs e)
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void FormAbout_MouseUp(object sender, MouseEventArgs e)
            {
                mouseDown = false;
            }

            private void FormAbout_MouseMove(object sender, MouseEventArgs e)
            {
                if (mouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                    this.Update();
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

            private void imgHeader_Paint( object sender, PaintEventArgs e )
            {
                Graphics g          = e.Graphics;
                Color backColor     = Color.FromArgb( 65, 255, 255, 255 );
                var imgSize         = imgHeader.ClientSize;

                e.Graphics.FillRectangle( new SolidBrush( backColor ), 1, imgSize.Height - 2, imgSize.Width - 2, 2 );
            }

            private void imgHeader_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void imgHeader_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown       = false;
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

        #endregion

        #region "Header: External Links"

            /*
                Window > Button > Close
            */

            private void llblLink_TPB_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
            {
                System.Diagnostics.Process.Start(Cfg.Default.app_url_tpb);
            }

            private void lnk_Github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                System.Diagnostics.Process.Start(Cfg.Default.app_url_github);
            }

        #endregion
    }
}
