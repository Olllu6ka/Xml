using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace _3
{
    class Program
    {
        //Используя Visual Studio, создайте проект по шаблону Console Application.
        //Создайте программу, которая будет использовать XML файл из примера 1 и будет
        //выводить на консоль только номера телефонов.
        static void Main(string[] args)
        {
            XDocument document = XDocument.Load("TelephoneBook1.xml");
            foreach (XElement xml in document.Element("MyContacts").Elements("Contact")){
                XElement Telephonenumber = xml.Element("TelephoneNumber");
                Console.WriteLine(@"Номер человека - " + Telephonenumber.Value);
            }
            Console.ReadKey();
        }
    }
}
