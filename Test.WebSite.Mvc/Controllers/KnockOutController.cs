using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newton.Validation;
using Newton.Extensions;
using Newtonsoft.Json;

namespace Test.WebSite.Mvc.Controllers
{
    public class KnockOutController : Controller
    {
        private readonly IEntityRuleProviderFactory ruleProviderFactory;

        public KnockOutController(IEntityRuleProviderFactory ruleProviderFactory)
        {
            this.ruleProviderFactory = ruleProviderFactory;
        }

        public ActionResult Index()
        {
            var model = new TestUser()
            {
                FirstName = "Daniel",
                LastName = "Le Pelley",
                Address = new TestAddress()
                {
                    Number = 123,
                    Street = "Main Street",
                    Postcode = "CR2 3DS"
                }
            };

            var converter = new JsonRuleConverter();
            var json = converter.Convert(ruleProviderFactory.Create<TestUser>());

            ViewData["Rules"] = json;

            return View(model);
        }

        public ActionResult Save(TestUser testUser)
        {
            var modelState = ruleProviderFactory.Create<TestUser>().ValidateForMvc(testUser);

            if (!modelState.IsValid)
                return new JsonErrorResult(modelState);
            else
            {
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

    public class JsonRuleConverter
    {
        public string Convert<T>(IEntityRuleProvider<T> ruleProvider)
        {
            var outputDictionary = new Dictionary<string, Dictionary<string, string>[]>();

            foreach (var fieldRule in ruleProvider.FieldRules)
            {
                var rulesDictionary = new List<Dictionary<string, string>>();
                foreach (var rule in fieldRule.Value.GetIRules())
                {
                    var maximumLengthRule = rule as MaximumLengthRule;
                    if (maximumLengthRule != null)
                    {
                        var ruleDictionary = new Dictionary<string, string>();
                        ruleDictionary.Add("rule", "MaxLength");
                        ruleDictionary.Add("maxLength", maximumLengthRule.Length.ToString());
                        rulesDictionary.Add(ruleDictionary);
                    }

                    var minimumLengthRule = rule as MinimumLengthRule;
                    if (minimumLengthRule != null)
                    {
                        var ruleDictionary = new Dictionary<string, string>();
                        ruleDictionary.Add("rule", "MinLength");
                        ruleDictionary.Add("minLength", minimumLengthRule.Length.ToString());
                        rulesDictionary.Add(ruleDictionary);
                    }

                    if (rule.GetType().IsAssignableFrom(typeof(IsRequiredRule<string>)))
                    {
                        var ruleDictionary = new Dictionary<string, string>();
                        ruleDictionary.Add("rule", "IsRequired");
                        rulesDictionary.Add(ruleDictionary);
                    }
                }

                outputDictionary.Add(fieldRule.Key, rulesDictionary.ToArray());
            }

            return JsonConvert.SerializeObject(outputDictionary);
        }

    }
}
