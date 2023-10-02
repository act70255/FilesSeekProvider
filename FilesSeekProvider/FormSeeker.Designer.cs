using System.Windows.Forms;

namespace FilesSeeker
{
    partial class FormSeeker
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
            tabAdd = new TabPage();
            tabBasePage = new TabPage();
            tabBase = new TabControl();
            tabBase.SuspendLayout();
            SuspendLayout();
            // 
            // tabAdd
            // 
            tabAdd.Location = new Point(4, 24);
            tabAdd.Name = "tabAdd";
            tabAdd.Padding = new Padding(3);
            tabAdd.Size = new Size(792, 422);
            tabAdd.TabIndex = 0;
            tabAdd.Text = "+";
            tabAdd.UseVisualStyleBackColor = true;
            // 
            // tabBasePage
            // 
            tabBasePage.Location = new Point(4, 24);
            tabBasePage.Name = "tabBasePage";
            tabBasePage.Padding = new Padding(3);
            tabBasePage.Size = new Size(1347, 561);
            tabBasePage.TabIndex = 0;
            tabBasePage.Text = "Tab[1]";
            tabBasePage.UseVisualStyleBackColor = true;
            // 
            // tabBase
            // 
            tabBase.Controls.Add(tabBasePage);
            tabBase.Controls.Add(tabAdd);
            tabBase.Dock = DockStyle.Fill;
            tabBase.Location = new Point(0, 0);
            tabBase.Name = "tabBase";
            tabBase.SelectedIndex = 0;
            tabBase.Size = new Size(1355, 589);
            tabBase.TabIndex = 0;
            // 
            // FormSeeker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1355, 589);
            Controls.Add(tabBase);
            Name = "FormSeeker";
            Text = "FormSeeker";
            Load += FormSeeker_Load;
            tabBase.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage tabAdd;
        private TabPage tabBasePage;
        private TabControl tabBase;
    }
}