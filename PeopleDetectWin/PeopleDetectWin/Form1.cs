using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PeopleDetectWin
{
    public partial class Form_PeopleDetect : Form
    {
        bool selfSVM = false;
        bool Train = false;
        int PosSamNo = 0;
        int NegSamNo = 0;
        int Condition = 0;
        string PosSamList;
        string NegSamList;
        string PosSamPath;
        string NegSamPath;
        string VideoName;
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键

        public Form_PeopleDetect()
        {
            InitializeComponent();
        }

        [DllImport("PeopleTrackDll.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int Tracking(bool selfSVM, string VideoName);

        [DllImport("PeopleTrackDll.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int trainSVM(string PosSamPath, string PosSamList, int PosSamNO, string NegSamPath, string NegSamList, int NegSamNO);

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.textBox_PosSamNo.Enabled = true;
                this.textBox_PosSamList.Enabled = true;
                this.textBox_PosSamPath.Enabled = true;
                this.textBox_NegSamNo.Enabled = true;
                this.textBox_NegSamList.Enabled = true;
                this.textBox_NegSamPath.Enabled = true;
                this.button_PosSamList.Enabled = true;
                this.button_NegSamList.Enabled = true;
                this.button_PosSamPath.Enabled = true;
                this.button_NegSamPath.Enabled = true;
                Train = true;
            }
            else
            {
                this.textBox_PosSamNo.Enabled = false;
                this.textBox_PosSamList.Enabled = false;
                this.textBox_PosSamPath.Enabled = false;
                this.textBox_NegSamNo.Enabled = false;
                this.textBox_NegSamList.Enabled = false;
                this.textBox_NegSamPath.Enabled = false;
                this.button_PosSamList.Enabled = false;
                this.button_NegSamList.Enabled = false;
                this.button_PosSamPath.Enabled = false;
                this.button_NegSamPath.Enabled = false;
                Train = false;
                PosSamNo = 0;
                NegSamNo = 0;
            }
        }

        private void button_PosSamList_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_PosSamList.SelectedText = dialog.FileName;
                PosSamList = dialog.FileName;
            }
        }

        private void button_NegSamList_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_NegSamList.SelectedText = dialog.FileName;
                NegSamList = dialog.FileName;
            }
        }

        private void button_PosSamPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                this.textBox_PosSamPath.Text = folder.SelectedPath;
                PosSamPath = folder.SelectedPath + "\\";
            }
        }

        private void button_NegSamPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                this.textBox_NegSamPath.Text = folder.SelectedPath;
                NegSamPath = folder.SelectedPath + "\\";
            }
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_VideoPath.SelectedText = dialog.FileName;
                VideoName = dialog.FileName;
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (Train)
            {
                PosSamNo = Convert.ToInt32(this.textBox_PosSamNo.Text);
                NegSamNo = Convert.ToInt32(this.textBox_NegSamNo.Text);
                Condition = trainSVM(PosSamPath, PosSamList, PosSamNo, NegSamPath, NegSamList, NegSamNo);
                if (Condition == -1)
                    MessageBox.Show("无法正常进行训练，请重新选择！");
                else
                    MessageBox.Show("训练成功，文件保存为“SVM_HOG.xml”。");
            }
            else
            {
                if (VideoName == null)
                    MessageBox.Show("请选择待检测的视频！");
                else
                    Condition = Tracking(selfSVM, VideoName);
                if (Condition == -1)
                    MessageBox.Show("无法正常进行检测，请确认视频为.avi格式！");
            }
        }

        private void button_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            for (double d = 1; d > 0; d -= 0.08)
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
                this.Opacity = d;
                this.Refresh();
            }
            Application.Exit();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                this.selfSVM = true;
            else
                this.selfSVM = false;
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            for (double d = 1; d > 0; d -= 0.08)
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
                this.Opacity = d;
                this.Refresh();
            }
            Application.Exit();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//如果按下的是鼠标左键
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)//如果鼠标左键是按下的状态
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)//如果鼠标左键是松开的状态
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        private void Form1_load(object sender, EventArgs e)
        {
            Random r = new Random();
            this.BackgroundImage = Image.FromFile("Res\\bg.jpg");
            this.button_min.Image = Image.FromFile("Res\\min_normal.png");
            this.button_close.Image = Image.FromFile("Res\\close_normal.png");
            for (double d = 0.01; d < 1; d += 0.08)
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
                this.Opacity = d;
                this.Refresh();
            }
            this.Hide();
            this.Show();
        }
    }
}
