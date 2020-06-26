using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Unit.Controllers
{
    public class ControllerTestBase
    {
        protected void ObjectValidatorExecutor(Controller controller, object model) 
        { 
            var results = new List<ValidationResult>(); 

            Validator.TryValidateObject(model, new ValidationContext(model), results);

            foreach (var result in results) 
            { 
                controller.ModelState.AddModelError(result.MemberNames.FirstOrDefault(), result.ErrorMessage); 
            } 
        } 
    }
}
