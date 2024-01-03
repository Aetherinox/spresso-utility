using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.ConstrainedExecution;
using ScreenpressoKG;
using Lng = ScreenpressoKG.Properties.Resources;
using Cfg = ScreenpressoKG.Properties.Settings;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ScreenpressoKG.Forms
{
    public partial class FormContribute : Form
    {

        /*
            Define
        */

        private Helpers Helpers     = new Helpers( );

        /*
            Define > Mouse
        */

        private bool mouseDown;
        private Point lastLocation;

        #region "Main Window: Initialize"

            public FormContribute( )
            {
                InitializeComponent( );

                btn_Close.Parent            = imgHeader;
                btn_Close.BackColor         = Color.Transparent;

                lbl_HeaderName.Parent       = imgHeader;
                lbl_HeaderName.BackColor    = Color.Transparent;

                lbl_HeaderSub.Parent        = imgHeader;
                lbl_HeaderSub.BackColor     = Color.Transparent;

                string ver                  = AppInfo.ProductVersionCore.ToString( );
                string product              = AppInfo.Title;
                string tm                   = AppInfo.Trademark;

                lbl_HeaderSub.Text          = "v" + ver + " by " + tm;
                lbl_HeaderSub.Text          = product;

                lbl_contrib_intro.Text      = Lng.txt_contrib_intro;
                lbl_BTC.Text                = Lng.lbl_contrib_btc;
                lbl_ETH.Text                = Lng.lbl_contrib_eth;
                lbl_BCH.Text                = Lng.lbl_contrib_bch;


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
                Window > Button > Close > Click
            */

            private void btn_Window_Close_Click( object sender, EventArgs e )
            {
                FormParent to   = new FormParent( );
                to.Show         ( );
                this.Close      ( );
            }

        #endregion

        #region "Main Window: Paint"
        private void FormContribute_Paint( object sender, PaintEventArgs e )
            {
                Pen clr_border = new Pen(Color.FromArgb(75, 75, 75));
                e.Graphics.DrawRectangle(clr_border, 0, 0, Width - 1, Height - 1);
            }
        #endregion

        #region "Main Window: Dragging"

            /*
                Main Form > Mouse Down
                deals with moving form around on screen
            */

            private void FormContribute_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            /*
                Main Form > Mouse Up
                deals with moving form around on screen
            */

            private void FormContribute_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            /*
                Main Form > Mouse Move
                deals with moving form around on screen
            */

            private void FormContribute_MouseMove( object sender, MouseEventArgs e )
            {
                if (mouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                    this.Update( );
                }
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

        #region "Label: Title"

            private void lbl_Title_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }
            private void lbl_Title_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void lbl_Title_MouseMove( object sender, MouseEventArgs e )
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

            private void lbl_Subtitle_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void lbl_Subtitle_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void lbl_Subtitle_MouseMove( object sender, MouseEventArgs e )
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

        #region "Body: Intro"

            private void lbl_contrib_intro_MouseDown( object sender, MouseEventArgs e )
            {
                mouseDown = true;
                lastLocation = e.Location;
            }

            private void lbl_contrib_intro_MouseUp( object sender, MouseEventArgs e )
            {
                mouseDown = false;
            }

            private void lbl_contrib_intro_MouseMove( object sender, MouseEventArgs e )
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
    }
}
