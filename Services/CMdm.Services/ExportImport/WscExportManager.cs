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
    public partial class WscExportManager : IWscExportManager
    {
        #region Ctor

        public WscExportManager()
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
        public virtual byte[] ExportDocumentsToXlsx(IList<WrongSchemeCode> documents)
        {
            //property array
            var properties = new[]
            {

                new PropertyByName<WrongSchemeCode>("Customer ID", p => p.CIF_ID),
                new PropertyByName<WrongSchemeCode>("Account Number", p => p.FORACID),

                new PropertyByName<WrongSchemeCode>("Scheme Code", p => p.SCHM_CODE),
                new PropertyByName<WrongSchemeCode>("Account Officer Code", p => p.ACCOUNTOFFICER_CODE),
                new PropertyByName<WrongSchemeCode>("Account Officer", p => p.ACCOUNTOFFICER_NAME),
                new PropertyByName<WrongSchemeCode>("Scheme Classification", p => p.SCHMECODE_CLASSIFIATION),

                new PropertyByName<WrongSchemeCode>("Account Name", p => p.ACCT_NAME),
                new PropertyByName<WrongSchemeCode>("Branch Code", p => p.SOL_ID),
                new PropertyByName<WrongSchemeCode>("Customer Type", p => p.CUSTOMER_TYPE),
                new PropertyByName<WrongSchemeCode>("Run Date", p => p.DATE_OF_RUN.ToString()),
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
