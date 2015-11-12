namespace CocurrencyTest
{
    partial class CocurrencyLoader
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.cocurrencyAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ConsoleInterface = new System.Windows.Forms.TextBox();
            this.requestAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.requestStr = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(722, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "执行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cocurrencyAmount
            // 
            this.cocurrencyAmount.Location = new System.Drawing.Point(114, 12);
            this.cocurrencyAmount.Name = "cocurrencyAmount";
            this.cocurrencyAmount.Size = new System.Drawing.Size(100, 21);
            this.cocurrencyAmount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "并发数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(516, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "单次请求数量";
            // 
            // ConsoleInterface
            // 
            this.ConsoleInterface.Location = new System.Drawing.Point(45, 130);
            this.ConsoleInterface.Multiline = true;
            this.ConsoleInterface.Name = "ConsoleInterface";
            this.ConsoleInterface.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleInterface.Size = new System.Drawing.Size(664, 230);
            this.ConsoleInterface.TabIndex = 4;
            // 
            // requestAmount
            // 
            this.requestAmount.Location = new System.Drawing.Point(609, 11);
            this.requestAmount.Name = "requestAmount";
            this.requestAmount.Size = new System.Drawing.Size(100, 21);
            this.requestAmount.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "请求字符串";
            // 
            // requestStr
            // 
            this.requestStr.Location = new System.Drawing.Point(114, 50);
            this.requestStr.Multiline = true;
            this.requestStr.Name = "requestStr";
            this.requestStr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.requestStr.Size = new System.Drawing.Size(595, 47);
            this.requestStr.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(722, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "清空";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CocurrencyLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 372);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.requestStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.requestAmount);
            this.Controls.Add(this.ConsoleInterface);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cocurrencyAmount);
            this.Controls.Add(this.button1);
            this.Name = "CocurrencyLoader";
            this.Text = "压力测试工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox cocurrencyAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ConsoleInterface;
        private System.Windows.Forms.TextBox requestAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox requestStr;
        private System.Windows.Forms.Button button2;
    }
}

