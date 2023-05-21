using System.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4._2
{
    public partial class Form1 : Form
    {
        private ColorDialog chooseColorDialog = new ColorDialog();
        

        public Form1()
        {
            InitializeComponent();
            button1.Click += new EventHandler(OnClickChooseColorAndText);
            try
            {
                if (ReadSettings() == false)
                {
                    listBox1.Items.Add("В файле конфигурации нет информации...");
                }
                else
                {
                    listBox1.Items.Add("Информация успешно загружена из файла конфигурации...");
                }

                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception e)
            {
                listBox1.Items.Add("Возникала проблема при загрузке данных из файла конфигурации:");
                listBox1.Items.Add(e.Message);
            }
        }
        
        void OnClickChooseColorAndText(object Sender, EventArgs e)
        {
            if (chooseColorDialog.ShowDialog() == DialogResult.OK)
                this.BackColor = chooseColorDialog.Color;
            this.listBox1.ForeColor = chooseColorDialog.Color;
        }
        bool ReadSettings()
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey wkey = registry.OpenSubKey("Software", true);
            RegistryKey newKey = wkey.OpenSubKey("XMl_Dz");
            if (newKey.ValueCount < 1) { return (false); }
            int red = Convert.ToInt32(newKey.GetValue("BackGroundColor.R"));
            int green = Convert.ToInt32(newKey.GetValue("BackGroundColor.G"));
            int blue = Convert.ToInt32(newKey.GetValue("BackGroundColor.B"));
            this.BackColor = Color.FromArgb(red, green, blue);
            this.listBox1.ForeColor = Color.FromArgb(red, green, blue);
            int sizetext = Convert.ToInt32(newKey.GetValue("Size.Text"));
            string styletext = (string)newKey.GetValue("Style.Text");
            listBox1.Font = new Font("", sizetext, FontStyle.Italic);
            listBox1.Items.Add("Цвет текста: " + listBox1.ForeColor.ToString());
            listBox1.Items.Add("Шрифт текста: " + styletext);
            listBox1.Items.Add("Размер шрифта: " + sizetext);

            listBox1.Items.Add("Цвет фона: " + BackColor.Name);
            int X = Convert.ToInt32(newKey.GetValue("X"));
            int Y = Convert.ToInt32(newKey.GetValue("Y"));
            this.DesktopLocation = new Point(X, Y);
            listBox1.Items.Add("Расположение: " + DesktopLocation.ToString());

            this.Height = Convert.ToInt32(newKey.GetValue("Window.Height"));
            this.Width = Convert.ToInt32(newKey.GetValue("Window.Width"));

            listBox1.Items.Add("Размер: " + new Size(Width, Height).ToString());
            string winState = (string)newKey.GetValue("Window.State");
            listBox1.Items.Add("Состояние окна: " + winState);
            this.WindowState = (FormWindowState)FormWindowState.Parse(WindowState.GetType(), winState);
            return (true);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        void Register() {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey wkey = registry.OpenSubKey("Software", true);
            RegistryKey newKey = wkey.CreateSubKey("XMl_Dz");
            newKey.SetValue("BackGroundColor.R", BackColor.R.ToString());
            newKey.SetValue("BackGroundColor.G", BackColor.G.ToString());
            newKey.SetValue("BackGroundColor.B", BackColor.B.ToString());
            newKey.SetValue("Size.Text", listBox3.SelectedItem.ToString());
            newKey.SetValue("Style.Text", listBox2.SelectedItem.ToString());
            newKey.SetValue("X", DesktopLocation.X.ToString());
            newKey.SetValue("Y", DesktopLocation.Y.ToString());
            newKey.SetValue("Window.Height", Height.ToString());
            newKey.SetValue("Window.Width", Width.ToString());
            newKey.SetValue("Window.State", WindowState.ToString());
            wkey.Close();
            newKey.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register();
        }
    }
}
