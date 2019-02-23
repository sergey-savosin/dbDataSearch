namespace dbDataSearch.Setup
{
    partial class FormSetupEntity
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
            this.m_loadSetup_button = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.m_SaveSetup_Button = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // m_loadSetup_button
            // 
            this.m_loadSetup_button.Location = new System.Drawing.Point(47, 63);
            this.m_loadSetup_button.Name = "m_loadSetup_button";
            this.m_loadSetup_button.Size = new System.Drawing.Size(125, 25);
            this.m_loadSetup_button.TabIndex = 0;
            this.m_loadSetup_button.Values.Text = "Load setup";
            this.m_loadSetup_button.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // m_SaveSetup_Button
            // 
            this.m_SaveSetup_Button.Location = new System.Drawing.Point(47, 105);
            this.m_SaveSetup_Button.Name = "m_SaveSetup_Button";
            this.m_SaveSetup_Button.Size = new System.Drawing.Size(125, 25);
            this.m_SaveSetup_Button.TabIndex = 1;
            this.m_SaveSetup_Button.Values.Text = "Save setup";
            this.m_SaveSetup_Button.Click += new System.EventHandler(this.m_SaveSetup_Button_Click);
            // 
            // FormSetupEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_SaveSetup_Button);
            this.Controls.Add(this.m_loadSetup_button);
            this.Name = "FormSetupEntity";
            this.Text = "FormSetupEntity";
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton m_loadSetup_button;
        private ComponentFactory.Krypton.Toolkit.KryptonButton m_SaveSetup_Button;
    }
}