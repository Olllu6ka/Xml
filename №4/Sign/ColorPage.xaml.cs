using Haley.Services;
using Haley.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace _4.Sign
{
    /// <summary>
    /// Логика взаимодействия для ColorPage.xaml
    /// </summary>
    public partial class ColorPage : Page
    {
        private ColorDialog colorDialog = new ColorDialog();
        public ColorPage()
        {
            InitializeComponent();
            ButtonChangeColor.Click += Onclick;
            try
            {
                if (ReadSettings() == false)
                {
                    TextColor.Text = "Нету информации . . .";
                }
                else
                {
                    TextColor.Text = "Информация добавлена";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e){
            Environment.Exit(0);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        bool ReadSettings()
        {
            NameValueCollection valueCollection = ConfigurationManager.AppSettings;
            if (valueCollection.Count < 1) { return (false); }
            int a = Convert.ToInt32(valueCollection["BackGrungColor.A"]);
            int red = Convert.ToInt32(valueCollection["BackGrungColor.R"]);
            int green = Convert.ToInt32(valueCollection["BackGrungColor.G"]);
            int blue = Convert.ToInt32(valueCollection["BackGrungColor.B"]);
            ColorPageMain.Background = new SolidColorBrush(Color.FromArgb((byte)a,(byte)red, (byte)green, (byte)blue));
            TextColor.Text = $"Цвет - {ColorPageMain.Background}";
            int X = Convert.ToInt32(valueCollection["X"]);
            int Y = Convert.ToInt32(valueCollection["Y"]);
            TextColor.Text = $"Расположение по X -{X} и по Y - {Y}";
            string windowState = valueCollection["Window.State"];
            TextColor.Text = $"Сотояние окна = {windowState}";
            return (true);
        }
        private void ButtonChangeColor_Click(object sender, RoutedEventArgs e)
        {
            colorDialog.ShowDialog();
        }
        void Onclick(object Sender, EventArgs e)
        {
            byte a = colorDialog.Color.A;
            byte r = colorDialog.Color.R;
            byte g = colorDialog.Color.G;
            byte b = colorDialog.Color.B;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            ColorPageMain.Background = new SolidColorBrush(Color.FromArgb(a,r,g,b));
            SaveSettings();
        }
        private System.ComponentModel.IContainer components = null;
        
        void SaveSettings()
        {
            XmlDocument doc = loadConfiguration();
            XmlNode node = doc.SelectSingleNode("//appSettings");
            string[] keys = new string[]
            {
                "BackGroundColor.A",
                "BackGroundColor.R",
                "BackGroundColor.G",
                "BackGroundColor.B",
                "X",
                "Y",
                "Widow.Height",
                "Widow.Width",
                "Widow.State"
            };
            string[] values = new string[] {
                colorDialog.Color.A.ToString(),
                colorDialog.Color.R.ToString(),
                colorDialog.Color.G.ToString(),
                colorDialog.Color.B.ToString(),

            };
            for (int i = 0; i < keys.Length; i++)
            {
                XmlElement element = node.SelectSingleNode(string.Format("//add[@key='{0}']", keys[i])) as XmlElement;
                if (element != null)
                {
                    element.SetAttribute("value", values[i]);
                }
                //else
                //{
                //    element = doc.CreateElement("add");
                //    element.SetAttribute("key", keys[i]);
                //    element.SetAttribute("value", values[i]);
                //    node.AppendChild(element);
                //}
            }
            doc.Save(Assembly.GetExecutingAssembly().Location + ".config");
        }
        private static XmlDocument loadConfiguration()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(Assembly.GetExecutingAssembly().Location + ".config");
                return doc; 
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No config find", e);
            }
        }
    }
}
