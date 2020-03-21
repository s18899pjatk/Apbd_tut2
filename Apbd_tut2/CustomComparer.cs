using Apbd_tut2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Apbd_tut2
{
    public class CustomComparer : IEqualityComparer<Student>
    {
        public bool Equals( Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.FirstName}{x.LastName}{x.IndexNumber}{x.Email}",
               $"{y.FirstName}{y.LastName}{y.IndexNumber}{y.Email}");
        }

        public int GetHashCode([DisallowNull] Student obj)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .GetHashCode($"{obj.FirstName}{obj.LastName}{obj.IndexNumber}{obj.Email}");
        }
    }
}
