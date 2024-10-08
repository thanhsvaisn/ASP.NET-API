using DMAWS_T2305M_KimQuangMinh.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2305M_LuuQuangThanh.Attributes
{
    public class CompareDates : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var project = (Project)validationContext.ObjectInstance;
            if (project.ProjectEndDate.HasValue && project.ProjectStartDate >= project.ProjectEndDate.Value)
            {
                return new ValidationResult("ProjectStartDate must be less than ProjectEndDate.");
            }
            return ValidationResult.Success;
        }
    }
}
