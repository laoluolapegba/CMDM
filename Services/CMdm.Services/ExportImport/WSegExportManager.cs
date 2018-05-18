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
    public partial class WSegExportManager : IWSegExportManager
    {
        #region Ctor

        public WSegExportManager()
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
        public virtual byte[] ExportDocumentsToXlsx(IList<WrongSegment> documents)
        {
            //property array
            var properties = new[]
            {

                new PropertyByName<WrongSegment>("Customer ID", p => p.ORGKEY),

                new PropertyByName<WrongSegment>("First Name", p => p.CUST_FIRST_NAME),
                new PropertyByName<WrongSegment>("Middle Name", p => p.CUST_MIDDLE_NAME),
                new PropertyByName<WrongSegment>("Last Name", p => p.CUST_LAST_NAME),
                new PropertyByName<WrongSegment>("Date of Birth", p => p.CUST_DOB),
                new PropertyByName<WrongSegment>("Branch Code", p => p.PRIMARY_SOL_ID),
                new PropertyByName<WrongSegment>("Segmentation Class", p => p.SEGMENTATION_CLASS),
                new PropertyByName<WrongSegment>("Segment Name", p => p.SEGMENTNAME),
                new PropertyByName<WrongSegment>("SubSegment", p => p.SUBSEGMENT),
                new PropertyByName<WrongSegment>("Corp ID", p => p.CORP_ID),
                new PropertyByName<WrongSegment>("Date of Run", p => p.DATE_OF_RUN),
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
