namespace PeopleDetectWin
{
    partial class Form_PeopleDetect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_PeopleDetect));
            this.button_start = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label_PosSamNo = new System.Windows.Forms.Label();
            this.textBox_PosSamNo = new System.Windows.Forms.TextBox();
            this.label_NegSamNo = new System.Windows.Forms.Label();
            this.textBox_NegSamNo = new System.Windows.Forms.TextBox();
            this.textBox_PosSamList = new System.Windows.Forms.TextBox();
            this.textBox_NegSamList = new System.Windows.Forms.TextBox();
            this.textBox_PosSamPath = new System.Windows.Forms.TextBox();
            this.textBox_NegSamPath = new System.Windows.Forms.TextBox();
            this.button_PosSamList = new System.Windows.Forms.Button();
            this.button_NegSamList = new System.Windows.Forms.Button();
            this.button_PosSamPath = new System.Windows.Forms.Button();
            this.button_NegSamPath = new System.Windows.Forms.Button();
            this.textBox_VideoPath = new System.Windows.Forms.TextBox();
            this.button_open = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button_close = new System.Windows.Forms.Button();
            this.button_min = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_start.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_start.Location = new System.Drawing.Point(289, 227);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(85, 23);
            this.button_start.TabIndex = 16;
            this.button_start.Text = "运行程序";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(25, 40);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 21);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "进行训练";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label_PosSamNo
            // 
            this.label_PosSamNo.AutoSize = true;
            this.label_PosSamNo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_PosSamNo.Location = new System.Drawing.Point(126, 44);
            this.label_PosSamNo.Name = "label_PosSamNo";
            this.label_PosSamNo.Size = new System.Drawing.Size(68, 17);
            this.label_PosSamNo.TabIndex = 1;
            this.label_PosSamNo.Text = "正样本数量";
            // 
            // textBox_PosSamNo
            // 
            this.textBox_PosSamNo.Enabled = false;
            this.textBox_PosSamNo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_PosSamNo.Location = new System.Drawing.Point(200, 41);
            this.textBox_PosSamNo.Name = "textBox_PosSamNo";
            this.textBox_PosSamNo.Size = new System.Drawing.Size(85, 23);
            this.textBox_PosSamNo.TabIndex = 2;
            // 
            // label_NegSamNo
            // 
            this.label_NegSamNo.AutoSize = true;
            this.label_NegSamNo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_NegSamNo.Location = new System.Drawing.Point(306, 44);
            this.label_NegSamNo.Name = "label_NegSamNo";
            this.label_NegSamNo.Size = new System.Drawing.Size(68, 17);
            this.label_NegSamNo.TabIndex = 3;
            this.label_NegSamNo.Text = "负样本数量";
            // 
            // textBox_NegSamNo
            // 
            this.textBox_NegSamNo.Enabled = false;
            this.textBox_NegSamNo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_NegSamNo.Location = new System.Drawing.Point(380, 41);
            this.textBox_NegSamNo.Name = "textBox_NegSamNo";
            this.textBox_NegSamNo.Size = new System.Drawing.Size(85, 23);
            this.textBox_NegSamNo.TabIndex = 4;
            // 
            // textBox_PosSamList
            // 
            this.textBox_PosSamList.Enabled = false;
            this.textBox_PosSamList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_PosSamList.Location = new System.Drawing.Point(124, 70);
            this.textBox_PosSamList.Name = "textBox_PosSamList";
            this.textBox_PosSamList.Size = new System.Drawing.Size(341, 23);
            this.textBox_PosSamList.TabIndex = 6;
            // 
            // textBox_NegSamList
            // 
            this.textBox_NegSamList.Enabled = false;
            this.textBox_NegSamList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_NegSamList.Location = new System.Drawing.Point(124, 100);
            this.textBox_NegSamList.Name = "textBox_NegSamList";
            this.textBox_NegSamList.Size = new System.Drawing.Size(341, 23);
            this.textBox_NegSamList.TabIndex = 8;
            // 
            // textBox_PosSamPath
            // 
            this.textBox_PosSamPath.Enabled = false;
            this.textBox_PosSamPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_PosSamPath.Location = new System.Drawing.Point(124, 129);
            this.textBox_PosSamPath.Name = "textBox_PosSamPath";
            this.textBox_PosSamPath.Size = new System.Drawing.Size(341, 23);
            this.textBox_PosSamPath.TabIndex = 10;
            // 
            // textBox_NegSamPath
            // 
            this.textBox_NegSamPath.Enabled = false;
            this.textBox_NegSamPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_NegSamPath.Location = new System.Drawing.Point(124, 161);
            this.textBox_NegSamPath.Name = "textBox_NegSamPath";
            this.textBox_NegSamPath.Size = new System.Drawing.Size(341, 23);
            this.textBox_NegSamPath.TabIndex = 12;
            // 
            // button_PosSamList
            // 
            this.button_PosSamList.Enabled = false;
            this.button_PosSamList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_PosSamList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_PosSamList.Location = new System.Drawing.Point(25, 68);
            this.button_PosSamList.Name = "button_PosSamList";
            this.button_PosSamList.Size = new System.Drawing.Size(93, 23);
            this.button_PosSamList.TabIndex = 5;
            this.button_PosSamList.Text = "正样本列表";
            this.button_PosSamList.UseVisualStyleBackColor = true;
            this.button_PosSamList.Click += new System.EventHandler(this.button_PosSamList_Click);
            // 
            // button_NegSamList
            // 
            this.button_NegSamList.Enabled = false;
            this.button_NegSamList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_NegSamList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_NegSamList.Location = new System.Drawing.Point(25, 98);
            this.button_NegSamList.Name = "button_NegSamList";
            this.button_NegSamList.Size = new System.Drawing.Size(93, 23);
            this.button_NegSamList.TabIndex = 7;
            this.button_NegSamList.Text = "负样本列表";
            this.button_NegSamList.UseVisualStyleBackColor = true;
            this.button_NegSamList.Click += new System.EventHandler(this.button_NegSamList_Click);
            // 
            // button_PosSamPath
            // 
            this.button_PosSamPath.Enabled = false;
            this.button_PosSamPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_PosSamPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_PosSamPath.Location = new System.Drawing.Point(25, 127);
            this.button_PosSamPath.Name = "button_PosSamPath";
            this.button_PosSamPath.Size = new System.Drawing.Size(93, 23);
            this.button_PosSamPath.TabIndex = 9;
            this.button_PosSamPath.Text = "正样本路径";
            this.button_PosSamPath.UseVisualStyleBackColor = true;
            this.button_PosSamPath.Click += new System.EventHandler(this.button_PosSamPath_Click);
            // 
            // button_NegSamPath
            // 
            this.button_NegSamPath.Enabled = false;
            this.button_NegSamPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_NegSamPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_NegSamPath.Location = new System.Drawing.Point(25, 156);
            this.button_NegSamPath.Name = "button_NegSamPath";
            this.button_NegSamPath.Size = new System.Drawing.Size(93, 23);
            this.button_NegSamPath.TabIndex = 11;
            this.button_NegSamPath.Text = "负样本路径";
            this.button_NegSamPath.UseVisualStyleBackColor = true;
            this.button_NegSamPath.Click += new System.EventHandler(this.button_NegSamPath_Click);
            // 
            // textBox_VideoPath
            // 
            this.textBox_VideoPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VideoPath.Location = new System.Drawing.Point(124, 190);
            this.textBox_VideoPath.Name = "textBox_VideoPath";
            this.textBox_VideoPath.Size = new System.Drawing.Size(341, 23);
            this.textBox_VideoPath.TabIndex = 14;
            // 
            // button_open
            // 
            this.button_open.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_open.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_open.Location = new System.Drawing.Point(25, 188);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(93, 23);
            this.button_open.TabIndex = 13;
            this.button_open.Text = "打开视频";
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox2.Location = new System.Drawing.Point(157, 229);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(126, 21);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.Text = "使用新训练的SVM";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_close.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_close.Location = new System.Drawing.Point(453, 0);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(35, 20);
            this.button_close.TabIndex = 19;
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_min
            // 
            this.button_min.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_min.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_min.Location = new System.Drawing.Point(423, 0);
            this.button_min.Name = "button_min";
            this.button_min.Size = new System.Drawing.Size(30, 20);
            this.button_min.TabIndex = 18;
            this.button_min.UseVisualStyleBackColor = true;
            this.button_min.Click += new System.EventHandler(this.button_min_Click);
            // 
            // button_exit
            // 
            this.button_exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_exit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_exit.Location = new System.Drawing.Point(380, 227);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(85, 23);
            this.button_exit.TabIndex = 17;
            this.button_exit.Text = "退出";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // Form_PeopleDetect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 273);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.button_min);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_open);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.textBox_VideoPath);
            this.Controls.Add(this.button_NegSamPath);
            this.Controls.Add(this.button_PosSamPath);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button_NegSamList);
            this.Controls.Add(this.label_PosSamNo);
            this.Controls.Add(this.button_PosSamList);
            this.Controls.Add(this.textBox_PosSamNo);
            this.Controls.Add(this.textBox_NegSamPath);
            this.Controls.Add(this.label_NegSamNo);
            this.Controls.Add(this.textBox_PosSamPath);
            this.Controls.Add(this.textBox_NegSamNo);
            this.Controls.Add(this.textBox_NegSamList);
            this.Controls.Add(this.textBox_PosSamList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_PeopleDetect";
            this.Text = "行人跟踪";
            this.Load += new System.EventHandler(this.Form1_load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label_PosSamNo;
        private System.Windows.Forms.TextBox textBox_PosSamNo;
        private System.Windows.Forms.Label label_NegSamNo;
        private System.Windows.Forms.TextBox textBox_NegSamNo;
        private System.Windows.Forms.TextBox textBox_PosSamList;
        private System.Windows.Forms.TextBox textBox_NegSamList;
        private System.Windows.Forms.TextBox textBox_PosSamPath;
        private System.Windows.Forms.TextBox textBox_NegSamPath;
        private System.Windows.Forms.Button button_PosSamList;
        private System.Windows.Forms.Button button_NegSamList;
        private System.Windows.Forms.Button button_PosSamPath;
        private System.Windows.Forms.Button button_NegSamPath;
        private System.Windows.Forms.TextBox textBox_VideoPath;
        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_min;
        private System.Windows.Forms.Button button_exit;
    }
}

