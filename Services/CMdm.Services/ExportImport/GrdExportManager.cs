using CMdm.Entities.Domain.GoldenRecord;
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
    public partial class GrdExportManager : IGrdExportManager
    {
        #region Ctor

        public GrdExportManager()
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
        public virtual byte[] ExportDocumentsToXlsx(IList<CdmaGoldenRecord> documents)
        {
            //property array
            var properties = new[]
            {

                new PropertyByName<CdmaGoldenRecord>("Record ID", p => p.GOLDEN_RECORD),
                new PropertyByName<CdmaGoldenRecord>("Customer ID", p => p.CUSTOMER_NO),
                new PropertyByName<CdmaGoldenRecord>("Account Number", p => p.ACCOUNT_NO),
                new PropertyByName<CdmaGoldenRecord>("Scheme Code", p => p.SCHEME_CODE),

                new PropertyByName<CdmaGoldenRecord>("BVN", p => p.BVN),
                new PropertyByName<CdmaGoldenRecord>("Name", p => p.FULL_NAME),
                new PropertyByName<CdmaGoldenRecord>("Date of Birth", p => p.DATE_OF_BIRTH.ToString()),
                new PropertyByName<CdmaGoldenRecord>("Residential Address", p => p.RESIDENTIAL_ADDRESS),

                new PropertyByName<CdmaGoldenRecord>("Customer Type", p => p.CUSTOMER_TYPE),
                new PropertyByName<CdmaGoldenRecord>("Gender", p => p.SEX),
                new PropertyByName<CdmaGoldenRecord>("Branch Code", p => p.BRANCH_CODE),
                new PropertyByName<CdmaGoldenRecord>("Phone Number", p => p.PHONE_NUMBER),

                new PropertyByName<CdmaGoldenRecord>("Email", p => p.EMAIL),
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
