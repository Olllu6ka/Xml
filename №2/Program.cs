using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2
{
    class Program
    {
        //Используя Visual Studio, создайте проект по шаблону Console Application.
        //Создайте программу, которая будет использовать XML файл из предыдущего примера
        //выводить всю информации о данном файле на консоль.
        static void Main(string[] args)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("TelephoneBook1.xml");
            XmlElement xmlElement = xml.DocumentElement;
            Console.WriteLine(xml.InnerText);
            Console.WriteLine(xml.InnerXml);
            Console.ReadKey();
        }
    }
}
