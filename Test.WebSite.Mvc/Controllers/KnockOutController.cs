using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newton.Validation;
using Newton.Extensions;

namespace Test.WebSite.Mvc.Controllers
{
    public class KnockOutController : Controller
    {
        private IEntityRuleProviderFactory ruleProviderFactory;

        public KnockOutController(IEntityRuleProviderFactory ruleProviderFactory)
        {
            this.ruleProviderFactory = ruleProviderFactory;
        }

        public ActionResult Index()
        {
            var model = new TestUser() { FirstName = "d", LastName = "a" };


            return View(model);
        }

        public ActionResult Save(TestUser testUser)
        {
            var modelState = ruleProviderFactory.Create<TestUser>().ValidateForMvc(testUser);

            if (!modelState.IsValid)
                return new JsonErrorResult(modelState);
            else
            {
                testUser.FirstName += "1";
                testUser.LastName += "2";
                return Json(testUser);
            }
        }
    }

    public class JsonErrorResult : JsonResult
    {
        public JsonErrorResult(ModelStateDictionary modelStates)
        {
            _modelStates = modelStates;
        }

        private ModelStateDictionary _modelStates = null;

        private const string JSONREQUEST_GETNOTALLOWED = "This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.";

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(JSONREQUEST_GETNOTALLOWED);
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.StatusCode = (int)HttpStatusCode.BadRequest;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            var errors = new Dictionary<string, IEnumerable<string>>();
            foreach (var keyValue in _modelStates)
            {
                errors[keyValue.Key] = keyValue.Value.Errors.Select(e => e.ErrorMessage);
            }

            response.Write(new JavaScriptSerializer().Serialize(errors));
        }
    }
}
