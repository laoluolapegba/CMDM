﻿//using CMdm.Entities.Core;
using CMdm.Core;
using CMdm.Core.Data;
using CMdm.Data;
using CMdm.Data.DAC;
using CMdm.Entities.Domain.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.Logging
{
    /// <summary>
    /// Default logger
    /// </summary>
    public partial class DefaultLogger : ILogger
    {
        #region Fields

        //private readonly IRepository<Log> _logRepository;
        private readonly IWebHelper _webHelper;
        private readonly IDbContext _dbContext;
        //private readonly IDataProvider _dataProvider;
        //private readonly CommonSettings _commonSettings;
        private LogDAC _logDAC;
        

        #endregion

        #region Ctor

        ///// <summary>
        ///// Ctor
        ///// </summary>
        ///// <param name="logRepository">Log repository</param>
        ///// <param name="webHelper">Web helper</param>
        ///// <param name="dbContext">DB context</param>
        ///// <param name="dataProvider">WeData provider</param>
        ///// <param name="commonSettings">Common settings</param>
        //public DefaultLogger()
        //{
        //    this._logRepository = logRepository;
        //  //  this._webHelper = webHelper;
        //    this._dbContext = dbContext;
        // //   this._dataProvider = dataProvider;
        // //   this._commonSettings = commonSettings;

        //}
        public DefaultLogger(IWebHelper webHelper)
        {
            this._webHelper = webHelper;
            _logDAC = new LogDAC();
        }
        #endregion

        #region Utitilities

        /// <summary>
        /// Gets a value indicating whether this message should not be logged
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Result</returns>
        protected virtual bool IgnoreLog(string message)
        {
            return false;
            //if (!_commonSettings.IgnoreLogWordlist.Any())
            //    return false;

            //if (String.IsNullOrWhiteSpace(message))
            //    return false;

            //return _commonSettings
            //    .IgnoreLogWordlist
            //    .Any(x => message.IndexOf(x, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        public virtual bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Deletes a log item
        /// </summary>
        /// <param name="log">Log item</param>
        public virtual void DeleteLog(Log log)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            //_logRepository.Delete(log);
        }

        /// <summary>
        /// Deletes a log items
        /// </summary>
        /// <param name="logs">Log items</param>
        public virtual void DeleteLogs(IList<Log> logs)
        {
            if (logs == null)
                throw new ArgumentNullException("logs");

            //_logRepository.Delete(logs);
        }

        /// <summary>
        /// Clears a log
        /// </summary>
        public virtual void ClearLog()
        {
            //if (_commonSettings.UseStoredProceduresIfSupported && _dataProvider.StoredProceduredSupported)
            //{
            //    //although it's not a stored procedure we use it to ensure that a database supports them
            //    //we cannot wait until EF team has it implemented - http://data.uservoice.com/forums/72025-entity-framework-feature-suggestions/suggestions/1015357-batch-cud-support


            //    //do all databases support "Truncate command"?
            //    string logTableName = _dbContext.GetTableName<Log>();
            //    _dbContext.ExecuteSqlCommand(String.Format("TRUNCATE TABLE [{0}]", logTableName));
            //}
            //else
            //{
            //    var log = _logRepository.Table.ToList();
            //    foreach (var logItem in log)
            //        _logRepository.Delete(logItem);
            //}
        }

        /// <summary>
        /// Gets all log items
        /// </summary>
        /// <param name="fromUtc">Log item creation from; null to load all records</param>
        /// <param name="toUtc">Log item creation to; null to load all records</param>
        /// <param name="message">Message</param>
        /// <param name="logLevel">Log level; null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Log item items</returns>
        public virtual IPagedList<Log> GetAllLogs(DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = "", LogLevel? logLevel = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            List<Log> result = default(List<Log>);
            // Step 1 - Calling Select on the DAC.
            result = _logDAC.Select(fromUtc, toUtc, message, logLevel, pageIndex, pageSize);

            var queitems = new PagedList<Log>(result, pageIndex, pageSize);
            return queitems;

            //var query = _logRepository.Table;
            
            //var log = new PagedList<Log>(query, pageIndex, pageSize);
            //return log;
        }

        /// <summary>
        /// Gets a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>Log item</returns>
        public virtual Log GetLogById(int logId)
        {
            if (logId == 0)
                return null;

            return _logDAC.SelectById(logId);
            //_logRepository.GetById(logId);
        }

        /// <summary>
        /// Get log items by identifiers
        /// </summary>
        /// <param name="logIds">Log item identifiers</param>
        /// <returns>Log items</returns>
        public virtual IList<Log> GetLogByIds(int[] logIds)
        {
            if (logIds == null || logIds.Length == 0)
                return new List<Log>();

            return _logDAC.SelectByIds(logIds);

            //var query = from l in _logRepository.Table
            //            where logIds.Contains(l.Id)
            //            select l;
            //var logItems = query.ToList();
            ////sort by passed identifiers
            //var sortedLogItems = new List<Log>();
            //foreach (int id in logIds)
            //{
            //    var log = logItems.Find(x => x.Id == id);
            //    if (log != null)
            //        sortedLogItems.Add(log);
            //}
            //return sortedLogItems;
        }

        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="customer">The customer to associate log record with</param>
        /// <returns>A log item</returns>
        public virtual Log InsertLog1(LogLevel logLevel, string shortMessage, string fullMessage = "", string CustomerId ="", string UserId = "")  //Customer customer = null
        {
            //check ignore word/phrase list?
            if (IgnoreLog(shortMessage) || IgnoreLog(fullMessage))
                return null;

            var log = new Log
            {
                //LogLevel = logLevel,
                //ShortMessage = shortMessage,
                //FullMessage = fullMessage,
                //IpAddress = "", //_webHelper.GetCurrentIpAddress(),
                //CustomerId = CustomerId,
                //PageUrl = "", // _webHelper.GetThisPageUrl(true),
                //ReferrerUrl = "",// _webHelper.GetUrlReferrer(),
                //Createdby = UserId,
                //CreatedDate = DateTime.Now
            };

            //_logRepository.Insert(log);

            return log;
        }
        public virtual Log AddLog(LogLevel logLevel, string shortMessage, string fullMessage = "", string CustomerId = "", string UserId = "")  //Customer customer = null
        {
            //check ignore word/phrase list?
            if (IgnoreLog(shortMessage) || IgnoreLog(fullMessage))
                return null;
            string fullMsg = string.Empty;
            if (fullMessage.Length >= 4000)
                fullMsg = fullMessage.Substring(0, 3900);
            var log = new Log
            {
                LOGLEVELID = (int)logLevel,
                SHORTMESSAGE = shortMessage,
                FULLMESSAGE = fullMsg,
                IPADDRESS = _webHelper.GetCurrentIpAddress(),
                CUSTOMERID = CustomerId,
                PAGEURL = _webHelper.GetThisPageUrl(true),
                REFERRERURL = _webHelper.GetUrlReferrer(),
                CREATEDBY = UserId,
                CREATEDDATE = DateTime.Now
            };
            //log.ID = null;
            _logDAC.Insert(log);
            //_logRepository.Insert(log);

            return log;
        }

        #endregion
    }
}
