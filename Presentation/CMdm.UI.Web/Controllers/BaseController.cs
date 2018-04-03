using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMdm.Framework.UI;
using CMdm.Framework.Kendoui;
using System.Text;
using CMdm.Framework.Mvc;
using Newtonsoft.Json.Converters;
using CMdm.Services.Logging;
using CMdm.Core;

namespace CMdm.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        private ILogger logger;
        private IWebHelper _webHelper;
        public BaseController()
        {
            
            _webHelper = new WebHelper(this.HttpContext);
            logger = new DefaultLogger(_webHelper);
        }
        //var logger = EngineContext.Current.Resolve<ILogger>();
        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected virtual ActionResult AccessDeniedView()
        {
            //return new HttpUnauthorizedResult();
            return RedirectToAction("AccessDenied", "Security", new { pageUrl = this.Request.RawUrl });
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(string viewName, object model)
        {
            //Original source code: http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
            if (string.IsNullOrEmpty(viewName))
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");

            this.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="exc">Exception</param>
        protected void LogException(Exception exc)
        {
            //var workContext = EngineContext.Current.Resolve<IWorkContext>();
            //var logger = EngineContext.Current.Resolve<ILogger>();

            //var customer = workContext.CurrentCustomer;
            logger.Error(exc.Message, exc);
            WriteToTextFile(exc.Message);
        }
        /// <summary>
        /// Display success notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true, bool dismissable = false)
        {
            AddNotification(AlertStyles.Success, message, persistForTheNextRequest, dismissable);
        }
        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true, bool dismissable = false)
        {
            AddNotification(AlertStyles.Danger, message, persistForTheNextRequest, dismissable);
        }
        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        /// <param name="logException">A value indicating whether exception should be logged</param>
        protected virtual void ErrorNotification(Exception exception, bool persistForTheNextRequest = true, bool logException = true, bool dismissable = false)
        {
            if (logException)
                LogException(exception);
            AddNotification(AlertStyles.Danger, exception.Message, persistForTheNextRequest, dismissable);
        }
        /// <summary>
        /// Display warning notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void WarningNotification(string message, bool persistForTheNextRequest = true, bool dismissable = false)
        {
            AddNotification(AlertStyles.Warning, message, persistForTheNextRequest, dismissable);
        }
        /// <summary>
        /// Display notification
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        /// /// <param name="dismissable">A value indicating whether a user can close the notification</param>
        protected virtual void AddNotification(string alertStyle, string message, bool persistForTheNextRequest, bool dismissable)
        {
            //string dataKey = string.Format("notifications.{0}", type);
            //if (persistForTheNextRequest)
            //{
            //    if (TempData[dataKey] == null)
            //        TempData[dataKey] = new List<string>();
            //    ((List<string>)TempData[dataKey]).Add(message);
            //}
            //else
            //{
            //    if (ViewData[dataKey] == null)
            //        ViewData[dataKey] = new List<string>();
            //    ((List<string>)ViewData[dataKey]).Add(message);
            //}


            var alerts = TempData.ContainsKey(Alert.TempDataKey)
               ? (List<Alert>)TempData[Alert.TempDataKey]
               : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }

        /// <summary>
        /// Error's json data for kendo grid
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns>Error's json data</returns>
        protected JsonResult ErrorForKendoGridJson(string errorMessage)
        {
            var gridModel = new DataSourceResult
            {
                Errors = errorMessage
            };

            return Json(gridModel);
        }


        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult"/> object that serializes the specified object to JavaScript Object Notation (JSON) format using the content type, content encoding, and the JSON request behavior.
        /// http://rion.io/2013/04/28/handling-larger-json-string-values-in-net-and-avoiding-exceptions/
        /// </summary>
        /// 
        /// <returns>
        /// The result object that serializes the specified object to JSON format.
        /// </returns>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="behavior">The JSON request behavior</param>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            //return new JsonResult()
            //{
            //    Data = data,
            //    ContentType = contentType,
            //    ContentEncoding = contentEncoding,
            //    JsonRequestBehavior = behavior,
            //    MaxJsonLength = Int32.MaxValue // Use this value to set your maximum size for all of your Requests
            //};
            //Json fix issue with dates in KendoUI grid
            //use json with IsoDateTimeConverter
            bool UseIsoDateTimeConverterInJson = true;
            var result = UseIsoDateTimeConverterInJson ? new ConverterJsonResult(new IsoDateTimeConverter()) : new JsonResult();

            result.Data = data;
            result.ContentType = contentType;
            result.ContentEncoding = contentEncoding;
            result.JsonRequestBehavior = behavior;

            //Json fix for admin area
            //sometime our entities have big text values returned (e.g. product desriptions)
            //of course, we can set and return them as "empty" (we already do it so). Furthermore, it's a perfoemance optimization
            //but it's better to avoid exceptions for other entities and allow maximum JSON length
            result.MaxJsonLength = int.MaxValue;

            return result;
            //return base.Json(data, contentType, contentEncoding, behavior);
        }

        /// <summary>
        /// On exception
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
                LogException(filterContext.Exception);
            base.OnException(filterContext);
        }
        public static void WriteToTextFile(string textLog)
        {
            try
            {
                FileStream objFS = null;
                string strFilepath = AppDomain.CurrentDomain.BaseDirectory + "\\exceptions\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + "AppDevWebSvcLog.log";
                if (!System.IO.File.Exists(strFilepath))
                {
                    objFS = new FileStream(strFilepath, FileMode.Create);
                }
                else { objFS = new FileStream(strFilepath, FileMode.Append); }
                using (StreamWriter Sr = new StreamWriter(objFS))
                {
                    Sr.WriteLine(System.DateTime.Now.ToShortTimeString() + "---" + textLog);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}