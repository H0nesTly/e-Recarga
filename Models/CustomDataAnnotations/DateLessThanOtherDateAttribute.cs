using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Recarga.Models.CustomDataAnnotations
{
    public class DateLessThanOtherDateAttribute : ValidationAttribute
    {
        private string OtherProperty;

        public DateLessThanOtherDateAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null || OtherProperty == null)
                return new ValidationResult(ErrorMessage.ToString());

            DateTime dateEvaluate = (DateTime)value;
            DateTime otherDate = (DateTime)validationContext.ObjectInstance.GetType().GetProperty(OtherProperty).GetValue(validationContext.ObjectInstance, null);

            if (dateEvaluate < otherDate)
                return new ValidationResult(ErrorMessage.ToString());


            return ValidationResult.Success;
        }
    }
}