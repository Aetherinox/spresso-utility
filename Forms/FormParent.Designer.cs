using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using ScreenpressoKG;

namespace ScreenpressoKG
{
    partial class FormParent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /*
            Main Form > Rectangle
        */

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen clr_border = new Pen(Color.FromArgb(75, 75, 75));
            e.Graphics.DrawRectangle(clr_border, 0, 0, Width - 1, Height - 1);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParent));
            this.minimizeBtn = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Label();
            this.btnGenerate = new ScreenpressoKG.AetherxButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lbl_User = new System.Windows.Forms.Label();
            this.mnuTop = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCopy = new ScreenpressoKG.AetherxButton();
            this.lbl_LicenseKey = new System.Windows.Forms.Label();
            this.grpBox = new System.Windows.Forms.Panel();
            this.btn_HostView = new ScreenpressoKG.AetherxButton();
            this.btn_DoBlock = new ScreenpressoKG.AetherxButton();
            this.lbl_HostBlocker_Desc = new System.Windows.Forms.Label();
            this.lbl_HostBlocker_Title = new System.Windows.Forms.Label();
            this.txt_LicenseKey = new ScreenpressoKG.AetherxTextBox();
            this.txt_User = new ScreenpressoKG.AetherxTextBox();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTop.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.grpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimizeBtn.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeBtn.Location = new System.Drawing.Point(469, 12);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(13, 32);
            this.minimizeBtn.TabIndex = 0;
            this.minimizeBtn.Text = "―";
            this.minimizeBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.minimizeBtn.Click += new System.EventHandler(this.btn_Window_Minimize_Click);
            this.minimizeBtn.MouseEnter += new System.EventHandler(this.btn_Window_Minimize_MouseEnter);
            this.minimizeBtn.MouseLeave += new System.EventHandler(this.btn_Window_Minimize_MouseLeave);
            // 
            // closeBtn
            // 
            this.closeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBtn.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.closeBtn.Location = new System.Drawing.Point(494, 7);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(24, 32);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "x";
            this.closeBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.closeBtn.Click += new System.EventHandler(this.btn_Window_Close_Click);
            this.closeBtn.MouseEnter += new System.EventHandler(this.btn_Window_Close_MouseEnter);
            this.closeBtn.MouseLeave += new System.EventHandler(this.btn_Window_Close_MouseLeave);
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(6)))), ((int)(((byte)(85)))));
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGenerate.FlatAppearance.BorderSize = 0;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(19, 442);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(111, 29);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btn_Generate_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(6)))), ((int)(((byte)(85)))));
            this.lblTitle.Location = new System.Drawing.Point(19, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(316, 30);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Screenpresso License Generator";
            this.lblTitle.Click += new System.EventHandler(this.lbl_Title_Click);
            // 
            // lbl_User
            // 
            this.lbl_User.AutoSize = true;
            this.lbl_User.Location = new System.Drawing.Point(12, 243);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(52, 21);
            this.lbl_User.TabIndex = 6;
            this.lbl_User.Text = "Name";
            this.lbl_User.Click += new System.EventHandler(this.lbl_Serial_Sub_Click);
            // 
            // mnuTop
            // 
            this.mnuTop.AutoSize = false;
            this.mnuTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.mnuTop.Dock = System.Windows.Forms.DockStyle.None;
            this.mnuTop.GripMargin = new System.Windows.Forms.Padding(12, 2, 0, 2);
            this.mnuTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mnuTop.Location = new System.Drawing.Point(1, 46);
            this.mnuTop.Name = "mnuTop";
            this.mnuTop.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mnuTop.Size = new System.Drawing.Size(528, 32);
            this.mnuTop.TabIndex = 16;
            this.mnuTop.Text = "menuStrip1";
            this.mnuTop.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuTop_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 28);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.aboutToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.ForeColor = System.Drawing.Color.Red;
            this.statusStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip.Location = new System.Drawing.Point(1, 494);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.Size = new System.Drawing.Size(528, 28);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 17;
            this.statusStrip.Text = "statusStrip1";
            this.statusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip_ItemClicked);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ActiveLinkColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.LinkVisited = true;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(139, 19);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(6)))), ((int)(((byte)(85)))));
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopy.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Location = new System.Drawing.Point(136, 442);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(69, 29);
            this.btnCopy.TabIndex = 18;
            this.btnCopy.Text = "&Copy";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btn_Copy_Click);
            // 
            // lbl_LicenseKey
            // 
            this.lbl_LicenseKey.AutoSize = true;
            this.lbl_LicenseKey.Location = new System.Drawing.Point(12, 304);
            this.lbl_LicenseKey.Name = "lbl_LicenseKey";
            this.lbl_LicenseKey.Size = new System.Drawing.Size(166, 21);
            this.lbl_LicenseKey.TabIndex = 20;
            this.lbl_LicenseKey.Text = "Generated License Key";
            this.lbl_LicenseKey.Click += new System.EventHandler(this.lbl_LicenseKey_Click);
            // 
            // grpBox
            // 
            this.grpBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.grpBox.Controls.Add(this.btn_HostView);
            this.grpBox.Controls.Add(this.btn_DoBlock);
            this.grpBox.Controls.Add(this.lbl_HostBlocker_Desc);
            this.grpBox.Controls.Add(this.lbl_HostBlocker_Title);
            this.grpBox.Location = new System.Drawing.Point(19, 97);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(494, 126);
            this.grpBox.TabIndex = 24;
            this.grpBox.Paint += new System.Windows.Forms.PaintEventHandler(this.grpBox_Paint);
            // 
            // btn_HostView
            // 
            this.btn_HostView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(6)))), ((int)(((byte)(85)))));
            this.btn_HostView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_HostView.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_HostView.FlatAppearance.BorderSize = 0;
            this.btn_HostView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_HostView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_HostView.Location = new System.Drawing.Point(246, 78);
            this.btn_HostView.Name = "btn_HostView";
            this.btn_HostView.Size = new System.Drawing.Size(106, 29);
            this.btn_HostView.TabIndex = 20;
            this.btn_HostView.Text = "&View Host";
            this.btn_HostView.UseVisualStyleBackColor = false;
            this.btn_HostView.Click += new System.EventHandler(this.btn_HostView_Click);
            // 
            // btn_DoBlock
            // 
            this.btn_DoBlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(6)))), ((int)(((byte)(85)))));
            this.btn_DoBlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DoBlock.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_DoBlock.FlatAppearance.BorderSize = 0;
            this.btn_DoBlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DoBlock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DoBlock.Location = new System.Drawing.Point(134, 78);
            this.btn_DoBlock.Name = "btn_DoBlock";
            this.btn_DoBlock.Size = new System.Drawing.Size(106, 29);
            this.btn_DoBlock.TabIndex = 19;
            this.btn_DoBlock.Text = "&Block Host";
            this.btn_DoBlock.UseVisualStyleBackColor = false;
            this.btn_DoBlock.Click += new System.EventHandler(this.btn_DoBlock_Click);
            // 
            // lbl_HostBlocker_Desc
            // 
            this.lbl_HostBlocker_Desc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbl_HostBlocker_Desc.Location = new System.Drawing.Point(7, 33);
            this.lbl_HostBlocker_Desc.Name = "lbl_HostBlocker_Desc";
            this.lbl_HostBlocker_Desc.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.lbl_HostBlocker_Desc.Size = new System.Drawing.Size(484, 32);
            this.lbl_HostBlocker_Desc.TabIndex = 12;
            this.lbl_HostBlocker_Desc.Text = "Perform this step first to block Screenpresso from communicating with your comput" +
    "er. Required for license activation to work.";
            this.lbl_HostBlocker_Desc.Click += new System.EventHandler(this.lbl_HostBlocker_Desc_Click);
            // 
            // lbl_HostBlocker_Title
            // 
            this.lbl_HostBlocker_Title.AutoSize = true;
            this.lbl_HostBlocker_Title.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbl_HostBlocker_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(6)))), ((int)(((byte)(85)))));
            this.lbl_HostBlocker_Title.Location = new System.Drawing.Point(6, 7);
            this.lbl_HostBlocker_Title.Name = "lbl_HostBlocker_Title";
            this.lbl_HostBlocker_Title.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.lbl_HostBlocker_Title.Size = new System.Drawing.Size(108, 26);
            this.lbl_HostBlocker_Title.TabIndex = 11;
            this.lbl_HostBlocker_Title.Text = "Host Blocker";
            // 
            // txt_LicenseKey
            // 
            this.txt_LicenseKey.AutoScroll = true;
            this.txt_LicenseKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.txt_LicenseKey.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(6)))), ((int)(((byte)(85)))));
            this.txt_LicenseKey.BorderFocusColor = System.Drawing.Color.White;
            this.txt_LicenseKey.BorderSize = 1;
            this.txt_LicenseKey.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txt_LicenseKey.ForeColor = System.Drawing.Color.White;
            this.txt_LicenseKey.Location = new System.Drawing.Point(19, 330);
            this.txt_LicenseKey.Margin = new System.Windows.Forms.Padding(5);
            this.txt_LicenseKey.Multiline = true;
            this.txt_LicenseKey.Name = "txt_LicenseKey";
            this.txt_LicenseKey.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_LicenseKey.PasswordChar = false;
            this.txt_LicenseKey.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txt_LicenseKey.PlaceholderText = "";
            this.txt_LicenseKey.ReadOnly = true;
            this.txt_LicenseKey.Size = new System.Drawing.Size(494, 98);
            this.txt_LicenseKey.TabIndex = 22;
            this.txt_LicenseKey.UnderlineStyle = false;
            this.txt_LicenseKey.Value = "";
            // 
            // txt_User
            // 
            this.txt_User.AutoScroll = true;
            this.txt_User.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.txt_User.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(6)))), ((int)(((byte)(85)))));
            this.txt_User.BorderFocusColor = System.Drawing.Color.White;
            this.txt_User.BorderSize = 1;
            this.txt_User.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txt_User.ForeColor = System.Drawing.Color.White;
            this.txt_User.Location = new System.Drawing.Point(19, 269);
            this.txt_User.Margin = new System.Windows.Forms.Padding(5);
            this.txt_User.Multiline = false;
            this.txt_User.Name = "txt_User";
            this.txt_User.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_User.PasswordChar = false;
            this.txt_User.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txt_User.PlaceholderText = "";
            this.txt_User.ReadOnly = false;
            this.txt_User.Size = new System.Drawing.Size(494, 28);
            this.txt_User.TabIndex = 21;
            this.txt_User.UnderlineStyle = false;
            this.txt_User.Value = "";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 28);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            // 
            // aboutToolStripMenuItem2
            // 
            this.aboutToolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.aboutToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem3});
            this.aboutToolStripMenuItem2.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem2.Name = "aboutToolStripMenuItem2";
            this.aboutToolStripMenuItem2.Size = new System.Drawing.Size(52, 28);
            this.aboutToolStripMenuItem2.Text = "About";
            // 
            // aboutToolStripMenuItem3
            // 
            this.aboutToolStripMenuItem3.Name = "aboutToolStripMenuItem3";
            this.aboutToolStripMenuItem3.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem3.Text = "About";
            // 
            // FormParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(530, 525);
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.txt_LicenseKey);
            this.Controls.Add(this.txt_User);
            this.Controls.Add(this.lbl_LicenseKey);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lbl_User);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.minimizeBtn);
            this.Controls.Add(this.mnuTop);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuTop;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormParent";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   ";
            this.Load += new System.EventHandler(this.FormParent_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.mnuTop.ResumeLayout(false);
            this.mnuTop.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label minimizeBtn;
        private System.Windows.Forms.Label closeBtn;
        private AetherxButton btnGenerate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.MenuStrip mnuTop;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private AetherxButton btnCopy;
        private Label lbl_LicenseKey;
        private AetherxTextBox txt_User;
        private AetherxTextBox txt_LicenseKey;
        private Panel grpBox;
        private AetherxButton btn_DoBlock;
        private Label lbl_HostBlocker_Desc;
        private Label lbl_HostBlocker_Title;
        private AetherxButton btn_HostView;
        private ToolStripMenuItem fileToolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem2;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem3;
    }
}

