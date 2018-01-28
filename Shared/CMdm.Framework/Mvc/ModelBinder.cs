using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.ModelBinding;
using System.Web.Mvc;
using System.ComponentModel;

namespace CMdm.Framework.Mvc
{
    public class ModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            if (model is BaseModel)
            {
                ((BaseModel)model).BindModel(controllerContext, bindingContext);
            }
            return model;
        }

        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, object value)
        {
            //check if data type of value is System.String
            if (propertyDescriptor.PropertyType == typeof(string))
            {
                //developers can mark properties to be excluded from trimming with [NoTrim] attribute
                if (propertyDescriptor.Attributes.Cast<object>().All(a => a.GetType() != typeof(NoTrimAttribute)))
                {
                    var stringValue = (string)value;
                    value = string.IsNullOrEmpty(stringValue) ? stringValue : stringValue.Trim();
                }
            }

            base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        }
    }
    /// <summary>
    /// Attribute indicating that entered values should not be trimmed
    /// </summary>
    public class NoTrimAttribute : Attribute
    {
    }
}
