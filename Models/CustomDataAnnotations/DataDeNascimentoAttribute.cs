using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Recarga.Models.CustomDataAnnotations
{
    public class DataDeNascimentoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            DateTime dataDeNascimento = DateTime.Now.AddYears(-18);

            DateTime InsertedDate = (DateTime)value;


            if (InsertedDate > dataDeNascimento)
                return false;

            return true;
        }
    }
}