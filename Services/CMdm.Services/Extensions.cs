using CMdm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace CMdm.Services
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj,
           bool markCurrentAsSelected = true, int[] valuesToExclude = null, bool useLocalization = true) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumObj");

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                         select new { ID = Convert.ToInt32(enumValue), Name = useLocalization ? enumValue.ToString() : CommonHelper.ConvertEnum(enumValue.ToString()) };
            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);
            return new SelectList(values, "ID", "Name", selectedValue);
        }

        public static SelectList ToSelectList<T>(this T objList, Func<BaseEntity, string> selector) where T : IEnumerable<BaseEntity>
        {
            return new SelectList(objList.Select(p => new { ID = p.Id, Name = selector(p) }), "ID", "Name");
        }
    }
}
