namespace BaseFunction.BaseTools
{
    partial class TransactionService
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
            this.Btn_createQueryString = new System.Windows.Forms.Button();
            this.T_queryString = new System.Windows.Forms.TextBox();
            this.CB_returnURL = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.T_soufunID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.T_payAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.T_tradeAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.T_price = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CB_title = new System.Windows.Forms.ComboBox();
            this.T_subject = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.T_extraParam = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Btn_postRequest = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.T_response = new System.Windows.Forms.TextBox();
            this.Btn_clearResult = new System.Windows.Forms.Button();
            this.Btn_copyQueryString = new System.Windows.Forms.Button();
            this.CB_queryStringType = new System.Windows.Forms.ComboBox();
            this.Btn_testConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_createQueryString
            // 
            this.Btn_createQueryString.Location = new System.Drawing.Point(579, 21);
            this.Btn_createQueryString.Name = "Btn_createQueryString";
            this.Btn_createQueryString.Size = new System.Drawing.Size(75, 23);
            this.Btn_createQueryString.TabIndex = 0;
            this.Btn_createQueryString.Text = "生成参数";
            this.Btn_createQueryString.UseVisualStyleBackColor = true;
            this.Btn_createQueryString.Click += new System.EventHandler(this.Btn_createQueryString_Click);
            // 
            // T_queryString
            // 
            this.T_queryString.Location = new System.Drawing.Point(101, 195);
            this.T_queryString.Multiline = true;
            this.T_queryString.Name = "T_queryString";
            this.T_queryString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.T_queryString.Size = new System.Drawing.Size(454, 188);
            this.T_queryString.TabIndex = 1;
            // 
            // CB_returnURL
            // 
            this.CB_returnURL.FormattingEnabled = true;
            this.CB_returnURL.Location = new System.Drawing.Point(101, 21);
            this.CB_returnURL.Name = "CB_returnURL";
            this.CB_returnURL.Size = new System.Drawing.Size(454, 20);
            this.CB_returnURL.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "ReturnURL";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "搜房ID";
            // 
            // T_soufunID
            // 
            this.T_soufunID.Location = new System.Drawing.Point(101, 50);
            this.T_soufunID.Name = "T_soufunID";
            this.T_soufunID.Size = new System.Drawing.Size(126, 21);
            this.T_soufunID.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(359, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "请求类型";
            // 
            // T_payAmount
            // 
            this.T_payAmount.Location = new System.Drawing.Point(101, 76);
            this.T_payAmount.Name = "T_payAmount";
            this.T_payAmount.Size = new System.Drawing.Size(126, 21);
            this.T_payAmount.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(32, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "支付金额";
            // 
            // T_tradeAmount
            // 
            this.T_tradeAmount.Location = new System.Drawing.Point(101, 105);
            this.T_tradeAmount.Name = "T_tradeAmount";
            this.T_tradeAmount.Size = new System.Drawing.Size(126, 21);
            this.T_tradeAmount.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(32, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "订单金额";
            // 
            // T_price
            // 
            this.T_price.Location = new System.Drawing.Point(428, 79);
            this.T_price.Name = "T_price";
            this.T_price.Size = new System.Drawing.Size(126, 21);
            this.T_price.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(359, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "单价";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(359, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "业务名称";
            // 
            // CB_title
            // 
            this.CB_title.FormattingEnabled = true;
            this.CB_title.Location = new System.Drawing.Point(428, 108);
            this.CB_title.Name = "CB_title";
            this.CB_title.Size = new System.Drawing.Size(126, 20);
            this.CB_title.TabIndex = 14;
            // 
            // T_subject
            // 
            this.T_subject.Location = new System.Drawing.Point(101, 134);
            this.T_subject.Name = "T_subject";
            this.T_subject.Size = new System.Drawing.Size(454, 21);
            this.T_subject.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(32, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 18);
            this.label8.TabIndex = 15;
            this.label8.Text = "订单描述";
            // 
            // T_extraParam
            // 
            this.T_extraParam.Location = new System.Drawing.Point(101, 161);
            this.T_extraParam.Name = "T_extraParam";
            this.T_extraParam.Size = new System.Drawing.Size(454, 21);
            this.T_extraParam.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(32, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 18);
            this.label9.TabIndex = 17;
            this.label9.Text = "额外参数";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(32, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 18);
            this.label10.TabIndex = 19;
            this.label10.Text = "请求链接";
            // 
            // Btn_postRequest
            // 
            this.Btn_postRequest.Location = new System.Drawing.Point(579, 50);
            this.Btn_postRequest.Name = "Btn_postRequest";
            this.Btn_postRequest.Size = new System.Drawing.Size(75, 23);
            this.Btn_postRequest.TabIndex = 20;
            this.Btn_postRequest.Text = "POST请求";
            this.Btn_postRequest.UseVisualStyleBackColor = true;
            this.Btn_postRequest.Click += new System.EventHandler(this.Btn_postRequest_Click);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(32, 392);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 18);
            this.label11.TabIndex = 22;
            this.label11.Text = "响应信息";
            // 
            // T_response
            // 
            this.T_response.Location = new System.Drawing.Point(101, 389);
            this.T_response.Multiline = true;
            this.T_response.Name = "T_response";
            this.T_response.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.T_response.Size = new System.Drawing.Size(454, 83);
            this.T_response.TabIndex = 21;
            // 
            // Btn_clearResult
            // 
            this.Btn_clearResult.Location = new System.Drawing.Point(579, 79);
            this.Btn_clearResult.Name = "Btn_clearResult";
            this.Btn_clearResult.Size = new System.Drawing.Size(75, 23);
            this.Btn_clearResult.TabIndex = 23;
            this.Btn_clearResult.Text = "清理结果";
            this.Btn_clearResult.UseVisualStyleBackColor = true;
            this.Btn_clearResult.Click += new System.EventHandler(this.Btn_clearResult_Click);
            // 
            // Btn_copyQueryString
            // 
            this.Btn_copyQueryString.Location = new System.Drawing.Point(579, 108);
            this.Btn_copyQueryString.Name = "Btn_copyQueryString";
            this.Btn_copyQueryString.Size = new System.Drawing.Size(75, 23);
            this.Btn_copyQueryString.TabIndex = 24;
            this.Btn_copyQueryString.Text = "拷贝请求";
            this.Btn_copyQueryString.UseVisualStyleBackColor = true;
            this.Btn_copyQueryString.Click += new System.EventHandler(this.Btn_copyQueryString_Click);
            // 
            // CB_queryStringType
            // 
            this.CB_queryStringType.FormattingEnabled = true;
            this.CB_queryStringType.Location = new System.Drawing.Point(428, 47);
            this.CB_queryStringType.Name = "CB_queryStringType";
            this.CB_queryStringType.Size = new System.Drawing.Size(126, 20);
            this.CB_queryStringType.TabIndex = 25;
            // 
            // Btn_testConnection
            // 
            this.Btn_testConnection.Location = new System.Drawing.Point(579, 134);
            this.Btn_testConnection.Name = "Btn_testConnection";
            this.Btn_testConnection.Size = new System.Drawing.Size(75, 23);
            this.Btn_testConnection.TabIndex = 26;
            this.Btn_testConnection.Text = "测试连接";
            this.Btn_testConnection.UseVisualStyleBackColor = true;
            this.Btn_testConnection.Click += new System.EventHandler(this.Btn_testConnection_Click);
            // 
            // TransactionService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 486);
            this.Controls.Add(this.Btn_testConnection);
            this.Controls.Add(this.CB_queryStringType);
            this.Controls.Add(this.Btn_copyQueryString);
            this.Controls.Add(this.Btn_clearResult);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.T_response);
            this.Controls.Add(this.Btn_postRequest);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.T_extraParam);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.T_subject);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CB_title);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.T_price);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.T_tradeAmount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.T_payAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.T_soufunID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CB_returnURL);
            this.Controls.Add(this.T_queryString);
            this.Controls.Add(this.Btn_createQueryString);
            this.Name = "TransactionService";
            this.Text = "TransactionService";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_createQueryString;
        private System.Windows.Forms.TextBox T_queryString;
        private System.Windows.Forms.ComboBox CB_returnURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox T_soufunID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox T_payAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox T_tradeAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox T_price;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CB_title;
        private System.Windows.Forms.TextBox T_subject;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox T_extraParam;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button Btn_postRequest;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox T_response;
        private System.Windows.Forms.Button Btn_clearResult;
        private System.Windows.Forms.Button Btn_copyQueryString;
        private System.Windows.Forms.ComboBox CB_queryStringType;
        private System.Windows.Forms.Button Btn_testConnection;
    }
}