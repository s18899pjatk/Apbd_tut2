using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Apbd_tut2.Models
{
    public class University
    {
        [XmlAttribute(attributeName: "createdAt")]
        public DateTime time { get; set; }

        [XmlAttribute]
        public string author { get; set; }
        public HashSet<Student> students { get; set; }

        public HashSet<ActiveStudies> ActiveStudies { get; set; }
    }
}
