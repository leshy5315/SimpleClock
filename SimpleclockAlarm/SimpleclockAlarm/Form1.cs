using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimpleclockAlarm
{
    public partial class Form1 : Form
    {
        private int Music = 0;
        private string NameFile = "";

        private string Hour = "";
        private string Minutes = "";
        private string Seconds = "";

        private string HourNow = "";
        private string MinutesNow = "";
        private string SecondsNow = "";

        WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Tick += new EventHandler(Timer1_Tick);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Hour = DateTime.Now.Hour.ToString();
            Minutes = DateTime.Now.Minute.ToString();
            Seconds = DateTime.Now.Minute.ToString();

            if (Hour.Length == 1)
            {
                Hour = "0" + Hour;
            }
            if (Minutes.Length == 1)
            {
                Minutes = "0" + Minutes;
            }
            if (Seconds.Length == 1)
            {
                Seconds = "0" + Seconds;
            }

            textBox1.Text = Hour;
            textBox2.Text = Minutes;
            textBox3.Text = Seconds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string extension = "";

            if (Music == 0)
            {

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    NameFile = openFileDialog1.FileName;
                    extension = Path.GetExtension(NameFile);

                    if (extension != ".mp3")
                    {
                        MessageBox.Show("Файл повинен мати розштрення mp3");
                        return;
                    }

                    button1.Text = NameFile.Substring(0, 14) + "...";

                }
            }
            else
            {
                WMP.controls.stop();
                button1.Text = NameFile.Substring(0, 14) + "...";
                Music = 0;
            }
        }



        private void Timer1_Tick(object Sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString("hh:mm:ss");
            HourNow = DateTime.Now.Hour.ToString();
            MinutesNow = DateTime.Now.Minute.ToString();
            SecondsNow = DateTime.Now.Second.ToString();

            if (HourNow.Length == 1)
            {
                HourNow = "0" + HourNow;
            }

            if (MinutesNow.Length == 1)
            {
                MinutesNow = "0" + MinutesNow;
            }

            if (SecondsNow.Length == 1)
            {
                SecondsNow = "0" + SecondsNow;
            }

            if(Hour == HourNow && Minutes == MinutesNow && Seconds == SecondsNow)
            {
                WMP.URL = NameFile;
                WMP.settings.volume = 100;
                WMP.controls.play();

                Music = 1;

                button1.Text = "Виключити музику";
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {

            if (button2.Text == "Стоп")
            {
                if (Music == 1)
                {
                    WMP.controls.stop();
                    button1.Text = NameFile.Substring(0, 14) + "...";
                    Music = 0;
                }

                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;

                timer1.Enabled = false;

             
                button2.Text = "Запустити";
            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Поле годинник пусте");
                    return;
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Поле годинник пусте");
                    return;
                }
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Поле годинник пусте");
                    return;
                }

                if (!(Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox1.Text) <= 23))
                {
                    MessageBox.Show("Некоректно вказані години");
                }

                if (!(Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox2.Text) <= 59))
                {
                    MessageBox.Show("Некоректно вказані хвилини");
                }

                if (!(Convert.ToInt32(textBox3.Text) >= 0 && Convert.ToInt32(textBox3.Text) <= 59))
                {
                    MessageBox.Show("Некоректно вказані секунди");
                }

                if (button1.Text == "Вибрати файл mp3")
                {
                    MessageBox.Show("Виберіть пісню mp3");
                }
                else
                {
                    button2.Text = "Стоп";

                    Hour = textBox1.Text;
                    Minutes = textBox2.Text;
                    Seconds = textBox3.Text;

                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                }

                timer1.Enabled = true;

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                if (textBox1.Text.Length >= 0 && textBox1.Text.Length <= 1)
                {
                    return;
                }
            }

            if(Char.IsControl (e.KeyChar))
            {
                if(e.KeyChar == (char)Keys.Back)
                {
                    return;
                }
            }

            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                if (textBox2.Text.Length >= 0 && textBox2.Text.Length <= 1)
                {
                    return;
                }
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    return;
                }
            }

            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                if (textBox3.Text.Length >= 0 && textBox3.Text.Length <= 1)
                {
                    return;
                }
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    return;
                }
            }

            e.Handled = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 1)
            {
                textBox1.Text = "0" + textBox1.Text;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 1)
            {
                textBox2.Text = "0" + textBox2.Text;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 1)
            {
                textBox3.Text = "0" + textBox3.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeTheme(SystemColors.Control, SystemColors.ControlText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeTheme(SystemColors.ControlText, SystemColors.Control);

        }

        private void ChangeTheme(Color backgroundColor, Color textColor)
        {
            BackColor = backgroundColor;
            ForeColor = textColor;

            foreach (Control control in Controls)
            {
                control.BackColor = backgroundColor;
                control.ForeColor = textColor;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeTheme(Color.LightGreen, SystemColors.ControlText);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeTheme(Color.Aquamarine, SystemColors.ControlText);

        }
    }
}
