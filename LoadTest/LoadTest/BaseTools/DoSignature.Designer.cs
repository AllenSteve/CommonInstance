namespace LoadTest.BaseTools
{
    partial class DoSignature
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
            this.Btn_postRequest = new System.Windows.Forms.Button();
            this.Btn_createQueryString = new System.Windows.Forms.Button();
            this.CB_Type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.T_OrderId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.T_encdata = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.T_stampno = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Btn_postRequest
            // 
            this.Btn_postRequest.Location = new System.Drawing.Point(610, 56);
            this.Btn_postRequest.Name = "Btn_postRequest";
            this.Btn_postRequest.Size = new System.Drawing.Size(75, 23);
            this.Btn_postRequest.TabIndex = 22;
            this.Btn_postRequest.Text = "POST请求";
            this.Btn_postRequest.UseVisualStyleBackColor = true;
            this.Btn_postRequest.Click += new System.EventHandler(this.Btn_postRequest_Click);
            // 
            // Btn_createQueryString
            // 
            this.Btn_createQueryString.Location = new System.Drawing.Point(610, 27);
            this.Btn_createQueryString.Name = "Btn_createQueryString";
            this.Btn_createQueryString.Size = new System.Drawing.Size(75, 23);
            this.Btn_createQueryString.TabIndex = 21;
            this.Btn_createQueryString.Text = "生成参数";
            this.Btn_createQueryString.UseVisualStyleBackColor = true;
            this.Btn_createQueryString.Click += new System.EventHandler(this.Btn_createQueryString_Click);
            // 
            // CB_Type
            // 
            this.CB_Type.FormattingEnabled = true;
            this.CB_Type.Location = new System.Drawing.Point(437, 27);
            this.CB_Type.Name = "CB_Type";
            this.CB_Type.Size = new System.Drawing.Size(126, 20);
            this.CB_Type.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(368, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 18);
            this.label3.TabIndex = 28;
            this.label3.Text = "类型";
            // 
            // T_OrderId
            // 
            this.T_OrderId.Location = new System.Drawing.Point(110, 30);
            this.T_OrderId.Name = "T_OrderId";
            this.T_OrderId.Size = new System.Drawing.Size(233, 21);
            this.T_OrderId.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(41, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "订单号";
            // 
            // T_encdata
            // 
            this.T_encdata.Location = new System.Drawing.Point(110, 98);
            this.T_encdata.MaxLength = 327670;
            this.T_encdata.Multiline = true;
            this.T_encdata.Name = "T_encdata";
            this.T_encdata.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.T_encdata.Size = new System.Drawing.Size(454, 282);
            this.T_encdata.TabIndex = 34;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(41, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 18);
            this.label10.TabIndex = 35;
            this.label10.Text = "请求链接";
            // 
            // T_stampno
            // 
            this.T_stampno.Location = new System.Drawing.Point(437, 59);
            this.T_stampno.Name = "T_stampno";
            this.T_stampno.Size = new System.Drawing.Size(126, 21);
            this.T_stampno.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(368, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 18);
            this.label6.TabIndex = 31;
            this.label6.Text = "章号";
            // 
            // DoSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 404);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.T_encdata);
            this.Controls.Add(this.CB_Type);
            this.Controls.Add(this.T_stampno);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.T_OrderId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_postRequest);
            this.Controls.Add(this.Btn_createQueryString);
            this.Name = "DoSignature";
            this.Text = "DoSignature";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_postRequest;
        private System.Windows.Forms.Button Btn_createQueryString;
        private System.Windows.Forms.ComboBox CB_Type;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox T_OrderId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox T_encdata;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox T_stampno;
        private System.Windows.Forms.Label label6;
    }
}