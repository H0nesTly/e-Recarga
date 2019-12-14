using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Recarga.Models.CustomDataAnnotations
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            DateTime CurrentDate = DateTime.Now;

            DateTime InsertedDate = (DateTime)value;


            if (InsertedDate < CurrentDate)
                return false;
            

            return true;
        }
    }
}