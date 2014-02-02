using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace Test.WebSite.Mvc.Controllers
{
    public class KnockOutController : Controller
    {
        public KnockOutController()
        {
            //IEntityRuleProviderFactory entityRuleProviderFactory
        }

        public ActionResult Index()
        {
            return View(new TestUser() { FirstName = "Danny" });
        }

        public ActionResult Save(TestUser testUser)
        {
            var rules = new Newton.UI.Mvc.MvcRuleProvider<TestUser>();
            rules.AddRule<string>("FirstName", new Newton.Validation.MaximumLengthRule(4));
            rules.AddRule<string>("FirstName", new Newton.Validation.NoSpacesRule());
            rules.AddRule<string>("FirstName", new Newton.Validation.IsRequiredRule<string>());

            rules.AddRule<string>("LastName", new Newton.Validation.MaximumLengthRule(6));
            rules.AddRule<string>("LastName", new Newton.Validation.IsRequiredRule<string>());
            var modelState = rules.ValidateForMvc(testUser);
            
            if (!modelState.IsValid)
                return new JsonErrorResult(modelState);
            else
                return View(testUser);
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
