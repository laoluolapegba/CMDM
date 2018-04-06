using CMdm.Core;
using CMdm.Entities.Domain.Logging;
using CMdm.Framework.Controllers;
using CMdm.Framework.Kendoui;
using CMdm.Services;
using CMdm.Services.Logging;
using CMdm.UI.Web.Models.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Controllers
{
    public partial class LogController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IWebHelper _webhelper;


        public LogController()
        {
            _webhelper = new WebHelper(this.HttpContext);
            this._logger = new DefaultLogger(_webhelper);
           
        }

        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var model = new LogListModel();
            model.AvailableLogLevels = LogLevel.Debug.ToSelectList(false).ToList();
            model.AvailableLogLevels.Insert(0, new SelectListItem { Text = "All", Value = "0" });

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult LogList(DataSourceRequest command, LogListModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            DateTime? createdOnFromValue = (model.CreatedOnFrom == null) ? null
                            : (DateTime?)model.CreatedOnFrom.Value;

            DateTime? createdToFromValue = (model.CreatedOnTo == null) ? null
                            : (DateTime?)model.CreatedOnTo.Value.AddDays(1);

            LogLevel? logLevel = model.LogLevelId > 0 ? (LogLevel?)(model.LogLevelId) : null;


            var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
                logLevel, command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = logItems.Select(x => new LogModel
                {
                    Id = x.ID,
                    LogLevel = x.LOGLEVELID.ToString(),
                    ShortMessage = x.SHORTMESSAGE,
                    //little performance optimization: ensure that "FullMessage" is not returned
                    FullMessage = "",
                    IpAddress = x.IPADDRESS,
                    CustomerId = x.CUSTOMERID,
                    CustomerEmail = "",
                    PageUrl = x.PAGEURL,
                    ReferrerUrl = x.REFERRERURL,
                    CreatedOn = x.CREATEDDATE
                }),
                Total = logItems.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("clearall")]
        public virtual ActionResult ClearAll()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            _logger.ClearLog();

            SuccessNotification("Cleared");
            return RedirectToAction("List");
        }

        public virtual ActionResult View(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var log = _logger.GetLogById(id);
            if (log == null)
                //No log found with the specified id
                return RedirectToAction("List");

            var model = new LogModel
            {
                Id = log.ID,
                LogLevel = log.LOGLEVELID.ToString(),
                ShortMessage = log.SHORTMESSAGE,
                FullMessage = log.FULLMESSAGE,
                IpAddress = log.IPADDRESS,
                CustomerId = log.CUSTOMERID,
                CustomerEmail =  "", //log.Customer != null ? log.Customer.Email : null,
                PageUrl = log.PAGEURL,
                ReferrerUrl = log.REFERRERURL,
                CreatedOn = log.CREATEDDATE
            };

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var log = _logger.GetLogById(id);
            if (log == null)
                //No log found with the specified id
                return RedirectToAction("List");

            _logger.DeleteLog(log);


            SuccessNotification("Deleted");
            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual ActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            if (selectedIds != null)
            {
                _logger.DeleteLogs(_logger.GetLogByIds(selectedIds.ToArray()).ToList());
            }

            return Json(new { Result = true });
        }
    }
}