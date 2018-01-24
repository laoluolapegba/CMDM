using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.UI;

namespace CMdm.Framework.Kendoui
{
    /// <summary>
    /// This class serves as a wrapper for the Kendo DataSourceRequestAttribute, which maps
    /// the incoming request data in aDataSourceRequest Object. This usually happens with the
    /// DataSourceRequestModelBinder. To get the result of the kendo model binder
    /// we need to use a custom model binder CustomDataSourceRequestModelBinder
    /// </summary>
    public class CustomDataSourceRequestAttribute : DataSourceRequestAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new CustomDataSourceRequestModelBinder();
        }
    }
}