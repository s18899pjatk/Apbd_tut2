﻿using Apbd_tut2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            var destFile = args.Length > 1 ? args[1] : throw new ArgumentNullException();
            var mode = args.Length > 2 ? args[2] : throw new ArgumentNullException();
            var students = ConvertToSet(path);
            var studies = new HashSet<ActiveStudies>();

            var itStudents = new ActiveStudies
            {
                name = "Computer Science",
                numberOfStudents = _countIT
  
            };
            studies.Add(itStudents);
            var mediaStudents = new ActiveStudies
            {
                name = "New Media Art",
                numberOfStudents = _countMedia

            };
            studies.Add(mediaStudents);

            University university = new University
            {
                author = "Artem Rymar",
                time = DateTime.Now,
                students = students,
                ActiveStudies = studies
            };

            //         Console.WriteLine(String.Concat("num of students: ", students.Count));

            serialize(destFile,mode,university);
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
           
                        if (columns.Length != 9)
                        {
                            _sw.WriteLine(String.Concat(DateTime.Now, " ", line, " incorrect line"));
                        }

                         foreach (string v in columns)
                        {
                            if (string.IsNullOrEmpty(v))
                            {
                                _sw.WriteLine(String.Concat(DateTime.Now, " ", line, " incorrect line"));
                            }  
                        }

                        var student = getStudent(columns);
                        students.Add(student);

                        if (!students.Add(student))
                        {
                            _sw.WriteLine(String.Concat(DateTime.Now, line, " ", " was not added becasue of its the same person"));
                        }
                        // Console.WriteLine(student);
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
        public static void serialize(String destFile, String mode, University university) {

            if (mode == "xml")
            {
                var xmlFile = destFile;
                FileStream writer = new FileStream(xmlFile, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(University), new XmlRootAttribute("university"));
                serializer.Serialize(writer, university);
            }
            else if (mode == "json")
            {
                var jsonFile = destFile;
                FileStream writer = new FileStream(jsonFile, FileMode.Create);
                byte[] jsonUtf8Bytes;
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(university, options);
                writer.Write(jsonUtf8Bytes);
            }
        }
    }
}
