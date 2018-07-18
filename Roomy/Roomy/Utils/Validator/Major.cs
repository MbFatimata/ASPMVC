using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roomy.Utils.Validator
{
    public class Major : ValidationAttribute
    {
        private int years;
        public Major (int years)
        {
            this.years = years;
        }
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                var dt = (DateTime)value; // je met mon objet en datetime
                return dt.AddYears(18) <= DateTime.Now; //test majorité
            }
            return false;
        }
    }
}