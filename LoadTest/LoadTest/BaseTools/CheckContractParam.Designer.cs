namespace LoadTest.BaseTools
{
    partial class CheckContractParam
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
            this.cryptograph = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.plaintext = new System.Windows.Forms.TextBox();
            this.Run = new System.Windows.Forms.Button();
            this.timestamp = new System.Windows.Forms.TextBox();
            this.soufunid = new System.Windows.Forms.TextBox();
            this.currenttime = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cryptograph
            // 
            this.cryptograph.Location = new System.Drawing.Point(90, 42);
            this.cryptograph.Name = "cryptograph";
            this.cryptograph.Size = new System.Drawing.Size(527, 21);
            this.cryptograph.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "密文";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "明文";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "时间戳";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "搜房ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "时间";
            // 
            // plaintext
            // 
            this.plaintext.Location = new System.Drawing.Point(90, 97);
            this.plaintext.Name = "plaintext";
            this.plaintext.Size = new System.Drawing.Size(369, 21);
            this.plaintext.TabIndex = 6;
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(532, 97);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 7;
            this.Run.Text = "解密";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // timestamp
            // 
            this.timestamp.Location = new System.Drawing.Point(90, 148);
            this.timestamp.Name = "timestamp";
            this.timestamp.Size = new System.Drawing.Size(369, 21);
            this.timestamp.TabIndex = 8;
            // 
            // soufunid
            // 
            this.soufunid.Location = new System.Drawing.Point(90, 192);
            this.soufunid.Name = "soufunid";
            this.soufunid.Size = new System.Drawing.Size(369, 21);
            this.soufunid.TabIndex = 9;
            // 
            // currenttime
            // 
            this.currenttime.Location = new System.Drawing.Point(90, 243);
            this.currenttime.Name = "currenttime";
            this.currenttime.Size = new System.Drawing.Size(369, 21);
            this.currenttime.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(532, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "生成密文";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 306);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(527, 21);
            this.textBox1.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "生成密文";
            // 
            // CheckContractParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 427);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.currenttime);
            this.Controls.Add(this.soufunid);
            this.Controls.Add(this.timestamp);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.plaintext);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cryptograph);
            this.Name = "CheckContractParam";
            this.Text = "CheckContractParam";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cryptograph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox plaintext;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.TextBox timestamp;
        private System.Windows.Forms.TextBox soufunid;
        private System.Windows.Forms.TextBox currenttime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
    }
}