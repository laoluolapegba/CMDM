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
    public class AcLogExportManager : IAcLogExportManager
    {
        #region Ctor

        public AcLogExportManager()
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
        public virtual byte[] ExportDocumentsToXlsx(IList<ActivityLog> documents)
        {
            //property array
            var properties = new[]
            {

                new PropertyByName<ActivityLog>("Username", p => p.USER_NAME),
                new PropertyByName<ActivityLog>("Fullname", p => p.FULLNAME),

                new PropertyByName<ActivityLog>("Activity Description", p => p.ACTIVITY_DESC),
                new PropertyByName<ActivityLog>("Branch Code", p => p.BRANCH_CODE),
                new PropertyByName<ActivityLog>("Branch Name", p => p.BRANCH_NAME),
                new PropertyByName<ActivityLog>("Activity Date", p => p.ACTIVITY_DATE.ToString()),
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
