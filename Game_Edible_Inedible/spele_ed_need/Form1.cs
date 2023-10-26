using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace spele_ed_need
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.Width = Lxp;
            panel1.Height = Lyp;
            panel2.Left = Lxp + 30;
            timer1.Enabled = false;
        }
        int N = 0;  // priek6metu skaits - nosaka Level izvele
        int L = 30; // p[k] izmeri
        PictureBox[] p;
        int Lxp = 400, Lyp=400; // speles lauka (panel1) izmeri
        Random R = new Random();
        int score = 0;
        int Level = 1;

        void PicArr(int N)
        {
            panel1.Controls.Clear();
            p = new PictureBox[N];
            for (int k = 0; k < N; k++)
            {
                p[k] = new PictureBox();
                p[k].Width = p[k].Height = L * 13 / 10;
                p[k].Left = R.Next(0, Lxp - L);
                p[k].Top = R.Next(-Lyp, 0);
                // sakuma - virs lauka, speles gaitaa kritis
                int r = R.Next(0, 250);

                if (r < 50) // ed, 
                {
                    // p[k].BackColor = Color.Green;  // lāgošanai
                    // jāizvēlas nejauši viens no piecien "ēdamiem" atēliem (f1...f5) 
                    p[k].ImageLocation = "f1.png";
                    p[k].Tag = "Y";
                }
                if (r > 50 && r<100)
                {
                    p[k].ImageLocation = "f2.jpg";
                    p[k].Tag = "Y";
                }
                if (r > 100 && r < 150)
                {
                    p[k].ImageLocation = "f3.jpg";
                    p[k].Tag = "Y";
                }
                if (r > 150 && r < 200)
                {
                    p[k].ImageLocation = "f4.jpg";
                    p[k].Tag = "Y";
                }
                if (r > 200 && r < 250)
                {
                    p[k].ImageLocation = "f5.png";
                    p[k].Tag = "Y";
                }

                else // need
                {
                    // p[k].BackColor = Color.Red; // lāgošanai
                    // jāizvēlas nejauši viens no piecien "neēdamiem" atēliem (a1...a5) 
                    p[k].ImageLocation = "a1.png";
                    p[k].Tag = "N";
                }
                p[k].SizeMode = PictureBoxSizeMode.StretchImage;
                p[k].Parent = panel1;
                p[k].MouseDown += new MouseEventHandler(p_MouseDown);
            } // for
        }

        void InitValues(int Level)
        {
            if (Level == 1)
            {
                N = 6; // bez if: N=4+2*Level;
                timer1.Interval = 100; // bez if: timer1.Interval = 200-Level*30;
            }
            else
                   if (Level == 2)
            {
                N = 10;
                timer1.Interval = 80; // nosaka Level 2
            }
            else
            {
                N = 20;
                timer1.Interval = 50; // nosaka Level 
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Level=(int)(numericUpDown1.Value);
            timer1.Enabled = true;
            InitValues(Level);
            PicArr(N);
            timer1.Enabled = true;
            score = 0;
        }  // button1_Click

        void NewScore(int plusminus)
        {
            score += plusminus;
            label2.Text = score.ToString();
        }
        private void p_MouseDown(object sender, MouseEventArgs e)
        { // p[k] reakcija uz klik6ki
            PictureBox s = sender as PictureBox;
            // ja pareizi klik6kinaja
            if (e.Button == MouseButtons.Left && s.Tag.ToString() == "Y"
                || e.Button == MouseButtons.Right && s.Tag.ToString() == "N")
            {
                NewScore(10); 
                s.Left = R.Next(0, Lxp - L);
                s.Top = R.Next(-Lyp, 0);
            }
            else // ja nepareizi
            {
                NewScore(-10);            }
          //  label2.Text = "score:" + NewScoreInString;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int k = 0; k < N; k++)
            {
                p[k].Top += 5; // vai R.Next?
                p[k].Left += R.Next(-10, 11); // vēlāk: bloķēt iespēju aizbēgt
                if (p[k].Top>Lyp) // nokrita
                {
                    NewScore(-10);
                    label2.Text = score.ToString();
                    // atkal paradas virs lauka
                    p[k].Left = R.Next(0, Lxp - L);
                    p[k].Top = R.Next(-Lyp, 0);
                }

            }

        } // timer1_Tick

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled= false;

            string TimeInString = "";
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;

            TimeInString = ((min < 10) ? "0" + min.ToString() : min.ToString());
            TimeInString += ":" + ((sec < 10) ? "0" + sec.ToString() : sec.ToString());

            label3.Text ="time:"+TimeInString;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                MessageBox.Show("Marija Sobole\n\nGame: Edible-Inedible.", "Info");
            }
        }

    }
}
