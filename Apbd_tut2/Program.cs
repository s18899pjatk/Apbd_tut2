using Apbd_tut2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Apbd_tut2
{
    class Program
    {
        private static StreamWriter _sw;
        private static int _countIT;
        private static int _countMedia;
        public static void Main(string[] args)
        {
            _sw = new StreamWriter(@"log.txt");
            var path = args.Length > 0 ? args[0] : throw new ArgumentNullException();
            var students = ConvertToSet(path);
            var activeStudies = new HashSet<ActiveStudies>();
            var itStudents = new ActiveStudies
            {
                name = "Computer Science",
                numberOfStudents = _countIT
            };

            var multimediaStudents = new ActiveStudies
            {
                name = "New Media Art",
                numberOfStudents = _countMedia
            };
            activeStudies.Add(itStudents);
            activeStudies.Add(multimediaStudents);
            Console.WriteLine(String.Concat("num of students: ", students.Count));

            if (args[2] == "xml")
            {
                var xmlFile = args.Length > 1 ? args[1] : throw new ArgumentNullException();
                FileStream writer = new FileStream(xmlFile, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(HashSet<Student>), new XmlRootAttribute("university"));
                serializer.Serialize(writer, students);
               // serializer.Serialize(writer, activeStudies);
            }
        }

        public static HashSet<Student> ConvertToSet(String str)
        {
            HashSet<Student> students = new HashSet<Student>(new CustomComparer());
            //Read from file
            try
            {
                var fi = new FileInfo(str);

                using (var stream = new StreamReader(fi.OpenRead()))
                {
                    string line = null;

                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');
                     //   var param1 = columns[0];
                      //  var date = DateTime.Parse(columns[5]);
                        if (columns.Length != 9)
                        {
                            _sw.WriteLine(String.Concat(DateTime.Now, line, " was not added"));
                        }

                        var student = getStudent(columns);
                        students.Add(student);
                        if (!students.Add(student))
                        {
                            // log to the log.txt
                            _sw.WriteLine(String.Concat(DateTime.Now, " element was not added"));
                        }
                        Console.WriteLine(student);
                    }
                }
            }
            catch (FileNotFoundException) { Console.WriteLine("File not found!!!"); }
            return students;
        }

        public static Student getStudent(string[] columns)
        {
            var st = new Student
            {
                IndexNumber = columns[4],
                FirstName = columns[0],
                LastName = columns[1],
                BirthDate = DateTime.Parse(columns[5]),
                Email = columns[6],
                FatherName = columns[7],
                MotherName = columns[8],
                Studies = new Studies
                {
                    name = columns[2],
                    mode = columns[3]
                }
            };

            if (columns[2].StartsWith("Informatyka"))
            {
                _countIT++;
            } else if(columns[2].StartsWith("Sztuka"))
            {
                _countMedia++;
            }

            return st;
        }


    }
}
