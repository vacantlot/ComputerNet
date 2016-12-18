namespace ComputerNet
{
    partial class FrmMDI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.通讯一ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通讯二ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.服务器端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客户端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.通讯一ToolStripMenuItem,
            this.通讯二ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(307, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 通讯一ToolStripMenuItem
            // 
            this.通讯一ToolStripMenuItem.Name = "通讯一ToolStripMenuItem";
            this.通讯一ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.通讯一ToolStripMenuItem.Text = "通讯一";
            this.通讯一ToolStripMenuItem.Click += new System.EventHandler(this.通讯一ToolStripMenuItem_Click);
            // 
            // 通讯二ToolStripMenuItem
            // 
            this.通讯二ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务器端ToolStripMenuItem,
            this.客户端ToolStripMenuItem,
            this.使用说明ToolStripMenuItem});
            this.通讯二ToolStripMenuItem.Name = "通讯二ToolStripMenuItem";
            this.通讯二ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.通讯二ToolStripMenuItem.Text = "通讯二";
            // 
            // 服务器端ToolStripMenuItem
            // 
            this.服务器端ToolStripMenuItem.Name = "服务器端ToolStripMenuItem";
            this.服务器端ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.服务器端ToolStripMenuItem.Text = "服务器端";
            this.服务器端ToolStripMenuItem.Click += new System.EventHandler(this.服务器端ToolStripMenuItem_Click);
            // 
            // 客户端ToolStripMenuItem
            // 
            this.客户端ToolStripMenuItem.Name = "客户端ToolStripMenuItem";
            this.客户端ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.客户端ToolStripMenuItem.Text = "客户端";
            this.客户端ToolStripMenuItem.Click += new System.EventHandler(this.客户端ToolStripMenuItem_Click);
            // 
            // 使用说明ToolStripMenuItem
            // 
            this.使用说明ToolStripMenuItem.Name = "使用说明ToolStripMenuItem";
            this.使用说明ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.使用说明ToolStripMenuItem.Text = "使用说明";
            this.使用说明ToolStripMenuItem.Click += new System.EventHandler(this.使用说明ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.使用说明ToolStripMenuItem_Click);
            // 
            // FrmMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 289);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMDI";
            this.ShowIcon = false;
            this.Text = "简单聊天室";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMDI_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 通讯一ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通讯二ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 服务器端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客户端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用说明ToolStripMenuItem;
    }
}