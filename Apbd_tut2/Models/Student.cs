using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Apbd_tut2.Models
{
    public class Student
    {
        private string _id;
        [XmlAttribute(attributeName:"index")]
        public string IndexNumber 
        { 
            get { return _id; } 
            set { _id = value ?? throw new ArgumentNullException(); } 
        }

        private string _fname;
        [XmlElement(elementName: "fname")]
        public string FirstName
        {
            get { return _fname; }
            set { _fname = value ?? throw new ArgumentNullException(); }
        }

        private string _lname;
        [XmlElement(elementName:"lname")]
        public string LastName 
        {
            get { return _lname; }
            set { _lname = value ?? throw new ArgumentNullException(); }
        }

        private DateTime _birthdate;
        [XmlElement(elementName: "birthDate")]
        public DateTime BirthDate
        {
            get { return _birthdate; }
            set { _birthdate = value; }
        }

        private string _mail;
        [XmlElement(elementName: "mail")]
        public string Email 
        {
            get { return _mail; }
            set { _mail = value ?? throw new ArgumentNullException(); }
        }

        private string _mothername;
        [XmlElement(elementName: "motherName")]
        public string MotherName
        {
            get { return _mothername; }
            set { _mothername = value ?? throw new ArgumentNullException(); }
        }

        private string _fatherName;
        [XmlElement(elementName: "fatherName")]
        public string FatherName
        {
            get { return _fatherName; }
            set { _fatherName = value ?? throw new ArgumentNullException(); }
        }

        public Studies Studies { get; set; }

    }
}
