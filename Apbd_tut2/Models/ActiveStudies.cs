using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Apbd_tut2.Models
{
    public class ActiveStudies
    {
        [XmlAttribute(attributeName: "name")]
        public string name { get; set; }

        [XmlAttribute(attributeName: "numberOfStudents")]
        public int numberOfStudents { get; set; }
    }
}
