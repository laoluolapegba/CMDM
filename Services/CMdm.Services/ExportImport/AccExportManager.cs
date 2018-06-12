using CMdm.Entities.Domain.CustomModule.Fcmb;
using CMdm.Services.ExportImport.Help;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CMdm.Services.ExportImport
{
    /// <summary>
    /// Export manager
    /// </summary>
    public partial class AccExportManager : IAccExportManager
    {
        #region Ctor

        public AccExportManager()
        {

        }
        #endregion
        protected virtual void SetCaptionStyle(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            style.Font.Bold = true;
        }
        /// <summary>
        /// Export documents list to XLSX
        /// </summary>
        /// <param name="documents">documents</param>
        public virtual byte[] ExportDocumentsToXlsx(IList<AccountOfficer> documents)
        {
            //property array
            var properties = new[]
            {
                
                new PropertyByName<AccountOfficer>("Account Number", p => p.ACCOUNT_NUMBER),
                new PropertyByName<AccountOfficer>("Account Name", p => p.ACCOUNT_NAME),

                new PropertyByName<AccountOfficer>("Branch Code", p => p.SOL_ID),
                new PropertyByName<AccountOfficer>("Branch Name", p => p.BRANCH),
                new PropertyByName<AccountOfficer>("Schm Code", p => p.SCHM_CODE),        
                new PropertyByName<AccountOfficer>("Acct Open Date", p => p.ACCT_OPN_DATE.ToString()),             
                
                new PropertyByName<AccountOfficer>("A0 Code", p => p.AO_CODE),
                new PropertyByName<AccountOfficer>("AO Name", p => p.AO_NAME),
                new PropertyByName<AccountOfficer>("SBU Code", p => p.SBU_CODE),
                new PropertyByName<AccountOfficer>("SBU Name", p => p.SBU_NAME),
                new PropertyByName<AccountOfficer>("Broker Code", p => p.BROKER_CODE),
                new PropertyByName<AccountOfficer>("Broker Name", p => p.BROKER_NAME),
                new PropertyByName<AccountOfficer>("Run Date", p => p.RUN_DATE.ToString()),
            };

            return ExportToXlsx(properties, documents);
        }
        /// <summary>
        /// Export objects to XLSX
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="properties">Class access to the object through its properties</param>
        /// <param name="itemsToExport">The objects to export</param>
        /// <returns></returns>
        protected virtual byte[] ExportToXlsx<T>(PropertyByName<T>[] properties, IEnumerable<T> itemsToExport)
        {
            using (var stream = new MemoryStream())
            {
                // ok, we can run the real code of the sample now
                using (var xlPackage = new ExcelPackage(stream))
                {
                    // uncomment this line if you want the XML written out to the outputDir
                    //xlPackage.DebugMode = true; 

                    // get handles to the worksheets
                    var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(T).Name);
                    var fWorksheet = xlPackage.Workbook.Worksheets.Add("DataForFilters");
                    fWorksheet.Hidden = eWorkSheetHidden.VeryHidden;

                    //create Headers and format them 
                    var manager = new PropertyManager<T>(properties.Where(p => !p.Ignore));
                    manager.WriteCaption(worksheet, SetCaptionStyle);

                    var row = 2;
                    foreach (var items in itemsToExport)
                    {
                        manager.CurrentObject = items;
                        manager.WriteToXlsx(worksheet, row++, false, fWorksheet: fWorksheet);
                    }

                    xlPackage.Save();
                }
                return stream.ToArray();
            }
        }
    }
}
