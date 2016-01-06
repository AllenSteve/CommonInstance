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
            this.CB_CityList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.T_OrderId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.T_encdata = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CB_ContractType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CB_ContractName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.T_ContractStampNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Btn_GetContractPage = new System.Windows.Forms.Button();
            this.T_ContractTemplateId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CB_Mobile = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CB_CityList
            // 
            this.CB_CityList.FormattingEnabled = true;
            this.CB_CityList.Location = new System.Drawing.Point(437, 22);
            this.CB_CityList.Name = "CB_CityList";
            this.CB_CityList.Size = new System.Drawing.Size(126, 20);
            this.CB_CityList.TabIndex = 33;
            this.CB_CityList.SelectedIndexChanged += new System.EventHandler(this.CB_Type_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(368, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 18);
            this.label3.TabIndex = 28;
            this.label3.Text = "城市列表";
            // 
            // T_OrderId
            // 
            this.T_OrderId.Location = new System.Drawing.Point(110, 22);
            this.T_OrderId.Name = "T_OrderId";
            this.T_OrderId.Size = new System.Drawing.Size(233, 21);
            this.T_OrderId.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(41, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "订单号";
            // 
            // T_encdata
            // 
            this.T_encdata.Location = new System.Drawing.Point(110, 152);
            this.T_encdata.MaxLength = 327670;
            this.T_encdata.Multiline = true;
            this.T_encdata.Name = "T_encdata";
            this.T_encdata.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.T_encdata.Size = new System.Drawing.Size(454, 282);
            this.T_encdata.TabIndex = 34;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(41, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 18);
            this.label10.TabIndex = 35;
            this.label10.Text = "请求链接";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(368, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 18);
            this.label6.TabIndex = 31;
            this.label6.Text = "手机号";
            // 
            // CB_ContractType
            // 
            this.CB_ContractType.FormattingEnabled = true;
            this.CB_ContractType.Location = new System.Drawing.Point(110, 55);
            this.CB_ContractType.Name = "CB_ContractType";
            this.CB_ContractType.Size = new System.Drawing.Size(233, 20);
            this.CB_ContractType.TabIndex = 37;
            this.CB_ContractType.SelectedIndexChanged += new System.EventHandler(this.CB_ContractType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(41, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 18);
            this.label1.TabIndex = 36;
            this.label1.Text = "合同类型";
            // 
            // CB_ContractName
            // 
            this.CB_ContractName.FormattingEnabled = true;
            this.CB_ContractName.Location = new System.Drawing.Point(110, 87);
            this.CB_ContractName.Name = "CB_ContractName";
            this.CB_ContractName.Size = new System.Drawing.Size(233, 20);
            this.CB_ContractName.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(41, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 18);
            this.label4.TabIndex = 41;
            this.label4.Text = "合同名称";
            // 
            // T_ContractStampNo
            // 
            this.T_ContractStampNo.Location = new System.Drawing.Point(437, 87);
            this.T_ContractStampNo.Name = "T_ContractStampNo";
            this.T_ContractStampNo.Size = new System.Drawing.Size(126, 21);
            this.T_ContractStampNo.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(368, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 18);
            this.label5.TabIndex = 39;
            this.label5.Text = "合同章号";
            // 
            // Btn_GetContractPage
            // 
            this.Btn_GetContractPage.Location = new System.Drawing.Point(590, 22);
            this.Btn_GetContractPage.Name = "Btn_GetContractPage";
            this.Btn_GetContractPage.Size = new System.Drawing.Size(107, 23);
            this.Btn_GetContractPage.TabIndex = 43;
            this.Btn_GetContractPage.Text = "获取合同页面";
            this.Btn_GetContractPage.UseVisualStyleBackColor = true;
            this.Btn_GetContractPage.Click += new System.EventHandler(this.Btn_GetContractPage_Click);
            // 
            // T_ContractTemplateId
            // 
            this.T_ContractTemplateId.Location = new System.Drawing.Point(110, 119);
            this.T_ContractTemplateId.Name = "T_ContractTemplateId";
            this.T_ContractTemplateId.Size = new System.Drawing.Size(453, 21);
            this.T_ContractTemplateId.TabIndex = 45;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(41, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 18);
            this.label7.TabIndex = 44;
            this.label7.Text = "合同模板ID";
            // 
            // CB_Mobile
            // 
            this.CB_Mobile.FormattingEnabled = true;
            this.CB_Mobile.Location = new System.Drawing.Point(437, 54);
            this.CB_Mobile.Name = "CB_Mobile";
            this.CB_Mobile.Size = new System.Drawing.Size(126, 20);
            this.CB_Mobile.TabIndex = 46;
            // 
            // DoSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 483);
            this.Controls.Add(this.CB_Mobile);
            this.Controls.Add(this.T_ContractTemplateId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Btn_GetContractPage);
            this.Controls.Add(this.CB_ContractName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.T_ContractStampNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CB_ContractType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.T_encdata);
            this.Controls.Add(this.CB_CityList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.T_OrderId);
            this.Controls.Add(this.label2);
            this.Name = "DoSignature";
            this.Text = "DoSignature";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_CityList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox T_OrderId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox T_encdata;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CB_ContractType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_ContractName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox T_ContractStampNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Btn_GetContractPage;
        private System.Windows.Forms.TextBox T_ContractTemplateId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CB_Mobile;
    }
}