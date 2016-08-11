namespace ConvertArtToIBmini
{
    partial class Form1
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
            this.btnCreateTokens = new System.Windows.Forms.Button();
            this.btnCreateTiles = new System.Windows.Forms.Button();
            this.btnCreateItemIcons = new System.Windows.Forms.Button();
            this.btnCreatePortraits = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateTokens
            // 
            this.btnCreateTokens.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateTokens.Location = new System.Drawing.Point(12, 154);
            this.btnCreateTokens.Name = "btnCreateTokens";
            this.btnCreateTokens.Size = new System.Drawing.Size(216, 39);
            this.btnCreateTokens.TabIndex = 4;
            this.btnCreateTokens.Text = "Create Combat Tokens";
            this.btnCreateTokens.UseVisualStyleBackColor = true;
            this.btnCreateTokens.Click += new System.EventHandler(this.btnCreateTokens_Click);
            // 
            // btnCreateTiles
            // 
            this.btnCreateTiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateTiles.Location = new System.Drawing.Point(12, 107);
            this.btnCreateTiles.Name = "btnCreateTiles";
            this.btnCreateTiles.Size = new System.Drawing.Size(216, 39);
            this.btnCreateTiles.TabIndex = 5;
            this.btnCreateTiles.Text = "Create Tiles";
            this.btnCreateTiles.UseVisualStyleBackColor = true;
            this.btnCreateTiles.Click += new System.EventHandler(this.btnCreateTiles_Click);
            // 
            // btnCreateItemIcons
            // 
            this.btnCreateItemIcons.Enabled = false;
            this.btnCreateItemIcons.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateItemIcons.Location = new System.Drawing.Point(12, 13);
            this.btnCreateItemIcons.Name = "btnCreateItemIcons";
            this.btnCreateItemIcons.Size = new System.Drawing.Size(216, 39);
            this.btnCreateItemIcons.TabIndex = 6;
            this.btnCreateItemIcons.Text = "Create Item Icons";
            this.btnCreateItemIcons.UseVisualStyleBackColor = true;
            this.btnCreateItemIcons.Click += new System.EventHandler(this.btnCreateItemIcons_Click);
            // 
            // btnCreatePortraits
            // 
            this.btnCreatePortraits.Enabled = false;
            this.btnCreatePortraits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreatePortraits.Location = new System.Drawing.Point(12, 60);
            this.btnCreatePortraits.Name = "btnCreatePortraits";
            this.btnCreatePortraits.Size = new System.Drawing.Size(216, 39);
            this.btnCreatePortraits.TabIndex = 7;
            this.btnCreatePortraits.Text = "Create Portraits";
            this.btnCreatePortraits.UseVisualStyleBackColor = true;
            this.btnCreatePortraits.Click += new System.EventHandler(this.btnCreatePortraits_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(11, 244);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(216, 39);
            this.btnHelp.TabIndex = 8;
            this.btnHelp.Text = "Instructions";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 294);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCreatePortraits);
            this.Controls.Add(this.btnCreateItemIcons);
            this.Controls.Add(this.btnCreateTiles);
            this.Controls.Add(this.btnCreateTokens);
            this.Name = "Form1";
            this.Text = "Convert Art to IBmini";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateTokens;
        private System.Windows.Forms.Button btnCreateTiles;
        private System.Windows.Forms.Button btnCreateItemIcons;
        private System.Windows.Forms.Button btnCreatePortraits;
        private System.Windows.Forms.Button btnHelp;
    }
}

