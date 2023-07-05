using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;

namespace ScreenpressoKG
{

    public partial class FormParent : Form
    {
        private Serial Serial       = new Serial( );

        /*
            Frame > Parent
        */

        public FormParent( )
        {
            InitializeComponent( );
            this.statusStrip.Renderer       = new StatusBar_Renderer( );
            string product                  = AppInfo.Title;
            lblTitle.Text                   = product;
        }


        /*
            Frame > Parent > Load
        */

        private void FormParent_Load( object sender, EventArgs e )
        {
            mnuTop.Renderer                 = new ToolStripProfessionalRenderer( new mnuTop_ColorTable( ) );
            toolStripStatusLabel1.Text      = string.Format( "Press Generate to create license key ..." );
            statusStrip.Refresh( );
        }

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

        private void btn_Window_Minimize_MouseEnter(object sender, EventArgs e)
        {
            minimizeBtn.ForeColor = Color.FromArgb(222, 31, 100);
        }

        /*
            Window > Button > Minimize > Mouse Leave
        */

        private void btn_Window_Minimize_MouseLeave(object sender, EventArgs e)
        {
            minimizeBtn.ForeColor = Color.FromArgb(255, 255, 255);
        }

        /*
            Window > Button > Close > Click
        */

        private void btn_Window_Close_Click( object sender, EventArgs e )
        {
            Application.Exit( );
        }

        /*
            Window > Button > Close > Mouse Enter
        */

        private void btn_Window_Close_MouseEnter(object sender, EventArgs e)
        {
            closeBtn.ForeColor = Color.FromArgb(222, 31, 100);
        }

        /*
            Window > Button > Close > Mouse Leave
        */

        private void btn_Window_Close_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.ForeColor = Color.FromArgb(255, 255, 255);
        }

        /*
            button > Generate > OnClick
        */

        private void btn_Generate_Click( object sender, EventArgs e )
        {
            if (string.IsNullOrEmpty(txt_User.Value))
            {
                MessageBox.Show("Type a name for yourself first", "No name specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // force placeholder off
                txt_LicenseKey.isPlaceholder = false;
                txt_LicenseKey.Value = Serial.Generate(Serial.Type.LicenseCorporate, new Random().Next(0x270f), this.txt_User.Value);

                toolStripStatusLabel1.Text = string.Format("Serial number generated. Paste into Screenpresso app.");
                statusStrip.Refresh();
            }
        }

        /*
             Button > Copy generated serial number
         */

        private void btn_Copy_Click(object sender, EventArgs e)
        {

            Debug.WriteLine(txt_LicenseKey.Value);

            if (string.IsNullOrEmpty(txt_LicenseKey.Value))
            {
                toolStripStatusLabel1.Text = string.Format("Generate license key first");
                statusStrip.Refresh();
            }
            else
            {
                Clipboard.SetText(txt_LicenseKey.Value);

                toolStripStatusLabel1.Text = string.Format("License key copied. Paste in Screenpresso app.");
                statusStrip.Refresh();
            }
        }

        /*
            Main Form > Mouse Down
            deals with moving form around on screen
        */

        private bool mouseDown;
        private Point lastLocation;

        private void MainForm_MouseDown( object sender, MouseEventArgs e )
        {
            mouseDown       = true;
            lastLocation    = e.Location;
        }

        /*
            Main Form > Mouse Up
            deals with moving form around on screen
        */

        private void MainForm_MouseUp( object sender, MouseEventArgs e )
        {
            mouseDown = false;
        }

        /*
            Main Form > Mouse Move
            deals with moving form around on screen
        */

        private void MainForm_MouseMove( object sender, MouseEventArgs e )
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    ( this.Location.X - lastLocation.X ) + e.X, ( this.Location.Y - lastLocation.Y ) + e.Y );

                this.Update( );
            }
        }

        /*
            Label > Window Title
        */

        private void lbl_Title_Click( object sender, EventArgs e )
        {

        }

        /*
            Top Menu > File > Exit
        */

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit( );
        }

        /*
            Top Menu > Help > About
        */

        private void aboutToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            FormAbout to = new FormAbout( );
            to.TopMost = true;
            to.Show( );
        }

        /*
            Top Menu > Click Item
        */

        private void mnuTop_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
 
        }

        /*
            Status Bar > Color Table
        */

        public class ClrTable : ProfessionalColorTable
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
            public StatusBar_Renderer( ) : base( new ClrTable() ) { }

            protected override void OnRenderToolStripBorder( ToolStripRenderEventArgs e )
            {
                if (!( e.ToolStrip is StatusStrip ) ) base.OnRenderToolStripBorder( e );
            }
        }

        /*
            Top Menu > Override Render Colors
        */

        public class mnuTop_ColorTable : ProfessionalColorTable
        {

            /*
                Gets the starting color of the gradient used when 
                a top-level System.Windows.Forms.ToolStripMenuItem is pressed.
            */

            public override Color MenuItemPressedGradientBegin => Color.FromArgb( 55, 55, 55 );

            /*
                Gets the end color of the gradient used when a top-level
                System.Windows.Forms.ToolStripMenuItem is pressed.
            */

            public override Color MenuItemPressedGradientEnd => Color.FromArgb( 55, 55, 55 );

            /*
                Gets the border color to use with a 
                System.Windows.Forms.ToolStripMenuItem.
            */

            public override Color MenuItemBorder => Color.FromArgb( 0, 45, 45, 45 );

            /*
                Gets the starting color of the gradient used when the 
                System.Windows.Forms.ToolStripMenuItem is selected.
            */

            public override Color MenuItemSelectedGradientBegin => Color.FromArgb( 222, 31, 103 );

            /*
                Gets the end color of the gradient used when the 
                System.Windows.Forms.ToolStripMenuItem is selected.
            */

            public override Color MenuItemSelectedGradientEnd => Color.FromArgb( 222, 31, 103 );

            /*
                Gets the solid background color of the 
                System.Windows.Forms.ToolStripDropDown.
            */

            public override Color ToolStripDropDownBackground => Color.FromArgb( 40, 40, 40 );

            /*
                Top Menu > Image > Start Gradient Color
            */

            public override Color ImageMarginGradientBegin => Color.FromArgb(222, 31, 103);

            /*
                Top Menu > Image > Middle Gradient Color
            */

            public override Color ImageMarginGradientMiddle => Color.FromArgb(222, 31, 103);

            /*
                Top Menu > Image > End Gradient Color
            */

            public override Color ImageMarginGradientEnd => Color.FromArgb(222, 31, 103);

            /*
                Top Menu > Shadow Effect
            */

            public override Color SeparatorDark => Color.FromArgb(0, 45, 45, 45);

            /*
                Top Menu > Border Color
            */

            public override Color MenuBorder => Color.FromArgb(0, 45, 45, 45);

            /*
                 Top Menu > Item Hover BG Color
             */

            public override Color MenuItemSelected => Color.FromArgb(222, 31, 103);

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        /*
            Label > Body
                "Click Generate button and copy the serial number below, and paste into Screen"
        */

        private void lbl_ClickToGenerate_Click( object sender, EventArgs e )
        {

        }

        /*
            Label > Sub Header
                "Generated serial key"
        */

        private void lbl_Serial_Sub_Click( object sender, EventArgs e )
        {

        }

        /*
            Label > Body
                "Click the "Generate" button below to create a number number."
        */

        private void lbl_Serial_Body_Click( object sender, EventArgs e )
        {

        }

        /*
            Text Box > Serial > OnTextChanged
        */

        private void serialTextbox__TextChanged(object sender, EventArgs e)
        {

        }

        private void tbUser__TextChanged(object sender, EventArgs e)
        {

        }
    }
}
