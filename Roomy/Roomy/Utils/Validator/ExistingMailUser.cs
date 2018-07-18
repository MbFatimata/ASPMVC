using Roomy.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roomy.Utils.Validator
{
    public class ExistingMailUser : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            using (RoomyDbContext db = new RoomyDbContext())
            {
                return !db.Users.Any(x => x.Mail == value.ToString()); 
            }
        }
    }
}