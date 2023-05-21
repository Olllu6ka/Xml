using System;
using System.Xml;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
namespace _4._1
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
        void OnClickChooseColorAndText(object Sender, EventArgs e)
        {
            if (chooseColorDialog.ShowDialog() == DialogResult.OK)
                this.BackColor = chooseColorDialog.Color;
            this.listBox1.ForeColor = chooseColorDialog.Color;
        }
        bool ReadSettings()
        {
            NameValueCollection allAppSettings = ConfigurationManager.AppSettings;
            if (allAppSettings.Count < 1) { return (false); }
            int red = Convert.ToInt32(allAppSettings["BackGroundColor.R"]);
            int green = Convert.ToInt32(allAppSettings["BackGroundColor.G"]);
            int blue = Convert.ToInt32(allAppSettings["BackGroundColor.G"]);
            this.BackColor = Color.FromArgb(red, green, blue);
            this.listBox1.ForeColor = Color.FromArgb(red, green, blue);
            int sizetext = Convert.ToInt32(allAppSettings["Size.Text"]);
            string styletext = allAppSettings["Style.Text"];
            listBox1.Font = new Font("", sizetext, FontStyle.Italic);
            listBox1.Items.Add("Цвет текста: " + listBox1.ForeColor.ToString());
            listBox1.Items.Add("Шрифт текста: " + styletext);
            listBox1.Items.Add("Размер шрифта: " + sizetext);

            listBox1.Items.Add("Цвет фона: " + BackColor.Name);
            int X = Convert.ToInt32(allAppSettings["X"]);
            int Y = Convert.ToInt32(allAppSettings["Y"]);
            this.DesktopLocation = new Point(X, Y);
            listBox1.Items.Add("Расположение: " + DesktopLocation.ToString());

            this.Height = Convert.ToInt32(allAppSettings["Window.Height"]);
            this.Width = Convert.ToInt32(allAppSettings["Window.Width"]);

            listBox1.Items.Add("Размер: " + new Size(Width, Height).ToString());
            string winState = allAppSettings["Window.State"];
            listBox1.Items.Add("Состояние окна: " + winState);
            this.WindowState = (FormWindowState)FormWindowState.Parse(WindowState.GetType(), winState);
            return (true);
        }
        void SaveSettings(){
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string[] keys = new string[] {"BackGroundColor.R", "BackGroundColor.G","BackGroundColor.B","Size.Text",
                "Style.Text","X","Y","Window.Height", "Window.Width", "Window.State"};
            string[] values = new string[] { BackColor.R.ToString(),
                                             BackColor.G.ToString(),
                                             BackColor.B.ToString(),
                                             listBox3.SelectedItem.ToString(),
                                             listBox2.SelectedItem.ToString(),
                                             DesktopLocation.X.ToString(),
                                             DesktopLocation.Y.ToString(),
                                             Height.ToString(),
                                             Width.ToString(),
                                             WindowState.ToString() };

            for (int i = 0; i < keys.Length; i++){
                XmlElement element = node.SelectSingleNode(string.Format("//add[@key='{0}']", keys[i])) as XmlElement;
                if (element != null) { element.SetAttribute("value", values[i]); }
                else{
                    element = doc.CreateElement("add");
                    element.SetAttribute("key", keys[i]);
                    element.SetAttribute("value", values[i]);
                    node.AppendChild(element);
                }
            }
            doc.Save(Assembly.GetExecutingAssembly().Location + ".config");
        }
		private static XmlDocument loadConfigDocument(){
            XmlDocument doc = null;
            try{
                doc = new XmlDocument();
                doc.Load(Assembly.GetExecutingAssembly().Location + ".config");
                return doc;
            }
            catch (System.IO.FileNotFoundException e){
                throw new Exception("No configuration file found.", e);
            }
        }

        private void button2_Click(object sender, EventArgs e){
            SaveSettings();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
