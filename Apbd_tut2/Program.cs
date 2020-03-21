using Apbd_tut2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Apbd_tut2
{
    class Program
    {
        public static void Main(string[] args)
        {
            using var sw = new StreamWriter(@"log.txt");
            var path = args.Length > 0 ? args[0] : throw new ArgumentNullException();

            //Read from file
            try
            {
                var fi = new FileInfo(path);
                using (var stream = new StreamReader(fi.OpenRead()))
                {
                    string line = null;

                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');
                        var param1 = columns[0];
                        var date = DateTime.Parse(columns[5]);
                        if (columns.Length != 9)
                        {
                            sw.WriteLine(String.Concat(DateTime.Now, line, " was not added"));
                        }
                        Console.WriteLine(line);
                    }
                }
            } catch (FileNotFoundException) { Console.WriteLine("File not found!!!"); }

            var students = new HashSet<Student>(new CustomComparer());

            var st = new Student
            {
                IndexNumber = "s1",
                FirstName = "Bob",
                LastName = "Marley",
                BirthDate = DateTime.Parse("12.01.1991"),
                Email = "s@test.com",
                FatherName = "John",
                MotherName = "Lily"
            };

            var st1 = new Student
            {
                IndexNumber = "s1",
                FirstName = "Bob",
                LastName = "Marley",
                BirthDate = DateTime.Parse("12.01.1991"),
                Email = "s@test.com",
                FatherName = "John",
                MotherName = "Lily"
            };

            students.Add(st);
            students.Add(st1);

            Console.WriteLine(String.Concat("num of students: ", students.Count));

            if (!students.Add(st1))
            {
                // log to the log.txt
                sw.WriteLine(String.Concat(DateTime.Now, " element was not added"));
            }

            var xmlFile = args.Length > 1 ? args[1] : throw new ArgumentNullException();
            FileStream writer = new FileStream(xmlFile, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(HashSet<Student>), new XmlRootAttribute("university"));
            serializer.Serialize(writer,students);
        }
    }
}
