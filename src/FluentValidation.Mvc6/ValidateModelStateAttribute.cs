using System.Collections;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace FluentValidation.Mvc6
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                var errors = new Hashtable();
                foreach (var modelStateEntry in filterContext.ModelState)
                {
                    if (modelStateEntry.Value.Errors.Count > 0)
                    {
                        errors[modelStateEntry.Key] =
                            modelStateEntry.Value.Errors.Select(p => p.ErrorMessage).ToList();
                    }
                }
                filterContext.Result = new JsonResult(new { success = false, errors })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            base.OnActionExecuting(filterContext);
        }
    }
}