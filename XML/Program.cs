using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XML
{
    class Program
    {
        //Создайте программу в которой создайте XML файл, который соответствовал бы
        //следующим требованиям:
        //1. имя файла: TelephoneBook.xml
        //2. корневой элемент: “MyContacts”
        //3. тег “Contact”, и в нем должно быть записано имя контакта и атрибут
        //“TelephoneNumber”
        //со значением номера телефона.
        //(* использовать программное создание XML файла)
        static void Main(string[] args)
        {
            XDocument document = new XDocument();
            XElement MyContacts = new XElement("MyContacts");
            Console.Write("Введите сколько вы хотите записать номеров: ");
            int Functoin = int.Parse(Console.ReadLine());
            for (int i = 0; i < Functoin; i++)
            {
                XElement Contact = new XElement("Contact");
                Console.Write("Введите Имя: ");
                string Contacts = Console.ReadLine();
                XElement Name = new XElement("Name", $"{Contacts}");
                Console.Write("Введите номер: ");
                string Number = Console.ReadLine();
                XElement AttributeTelephoneNumber = new XElement("TelephoneNumber", $"{Number}");
                Contact.Add(Name);
                Contact.Add(AttributeTelephoneNumber);
                MyContacts.Add(Contact);
            }
            document.Add(MyContacts);
            document.Save("TelephoneBook1.xml");
            
            //XmlTextWriter xml = new XmlTextWriter("TelephoneBook.xml", Encoding.UTF8);
            //xml.Formatting = Formatting.Indented;
            //xml.IndentChar = '\t';
            //xml.Indentation = 2;
            //xml.WriteStartDocument();
            //xml.WriteStartElement("MyContacts");
            //Console.Write("Введите сколько вы хотите записать номеров: ");
            //int Functoin = int.Parse(Console.ReadLine());
            //for (int g = 0; g < Functoin; g++){
            //    xml.WriteStartElement("Contact");
            //    Console.Write("Введите Имя: ");
            //    string Contacts = Console.ReadLine();
            //    xml.WriteStartAttribute("Name");
            //    xml.WriteString($"{Contacts}");
            //    xml.WriteStartAttribute("Telephone");
            //    Console.Write("Введите номер: ");
            //    string Number = Console.ReadLine();
            //    xml.WriteString($"{Number}");
            //    xml.WriteEndAttribute();
            //    xml.WriteEndElement();
            //}
            //xml.WriteEndElement();
            //xml.Close();
            Console.ReadKey();
        }
    }
}
