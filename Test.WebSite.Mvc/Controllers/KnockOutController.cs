using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Newton.Validation;
using Newton.Extensions;
using Newtonsoft.Json;

namespace Test.WebSite.Mvc.Controllers
{
    public class KnockOutController : Controller
    {
        private readonly IEntityRuleProviderFactory _ruleProviderFactory;

        public KnockOutController(IEntityRuleProviderFactory ruleProviderFactory)
        {
            this._ruleProviderFactory = ruleProviderFactory;
        }

        public ActionResult Index()
        {
            return RedirectToAction<KnockOutController>(c => c.Save(new TestUser() { FirstName = "Serialised" }));
        }

        public ActionResult SetUp()
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
            var json = converter.Convert(_ruleProviderFactory.Create<TestUser>());

            ViewData["Rules"] = json;

            return View("Index", model);
        }

        protected RedirectToRouteResult RedirectToAction<T>(Expression<Action<T>> action) where T : Controller
        {
            var body = action.Body as MethodCallExpression;

            if (body == null)
            {
                throw new ArgumentException("Expression must be a method call.");
            }

            if (body.Object != action.Parameters[0])
            {
                throw new ArgumentException("Method call must target lambda argument.");
            }

            string actionName = body.Method.Name;

            var attributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute)attributes[0];
                actionName = actionNameAttr.Name;
            }

            string controllerName = typeof(T).Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }

            return RedirectToAction(
                actionName,
                controllerName
                );
        }

        protected RedirectToRouteResult RedirectToAction<T>(Expression<Action<T>> action, RouteValueDictionary values) where T : Controller
        {
            var body = action.Body as MethodCallExpression;

            if (body == null)
            {
                throw new ArgumentException("Expression must be a method call.");
            }

            if (body.Object != action.Parameters[0])
            {
                throw new ArgumentException("Method call must target lambda argument.");
            }

            string actionName = body.Method.Name;

            var attributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute)attributes[0];
                actionName = actionNameAttr.Name;
            }

            string controllerName = typeof(T).Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }

            //RouteValueDictionary defaults = LinkBuilder.BuildParameterValuesFromExpression(body) ?? new RouteValueDictionary();

            //values = values ?? new RouteValueDictionary();
            //values.Add("controller", controllerName);
            //values.Add("action", actionName);

            //if (defaults != null)
            //{
            //    foreach (var pair in defaults.Where(p => p.Value != null))
            //    {
            //        values.Add(pair.Key, pair.Value);
            //    }
            //}

            return new RedirectToRouteResult(values);
        }



        public ActionResult Save(TestUser testUser)
        {
            var modelState = _ruleProviderFactory.Create<TestUser>().ValidateForMvc(testUser);

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

            var response = context.HttpContext.Response;

            response.StatusCode = (int)HttpStatusCode.BadRequest;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

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
