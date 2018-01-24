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
    /// This class is used by the CustomDataSourceRequestAttribute, which again is beeing
    /// adapted in all read methods for kendo grids. The current binding context is beeing
    /// passed to the original kendo modelbinder. We then transform critical datetime filter
    /// in a correct format and return the request to the action
    /// </summary>
    /// <summary>
	/// DateTime filtering is horribly unintuitive in Kendo Grids when a non-default (00:00:00) time is attached
	/// to the grid's datetime data. We use this custom model binder to transform the grid filters to return 
	/// results that ignore the attached time, leading to intuitive results that make users happy.
	/// 
	/// To use the code, substitute the [DataSourceRequest] attribute for [CustomDataSourceRequest] in your MVC controller
	/// </summary>
	public class CustomDataSourceRequestModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Get an instance of the original kendo model binder and call the binding method
            var baseBinder = new DataSourceRequestModelBinder();
            var request = (Kendo.Mvc.UI.DataSourceRequest)baseBinder.BindModel(controllerContext, bindingContext);

            if (request.Filters != null && request.Filters.Count > 0)
            {
                var transformedFilters = request.Filters.Select(TransformFilterDescriptors).ToList();
                request.Filters = transformedFilters;
            }

            return request;
        }

        private IFilterDescriptor TransformFilterDescriptors(IFilterDescriptor filter)
        {
            if (filter is CompositeFilterDescriptor)
            {
                var compositeFilterDescriptor = filter as CompositeFilterDescriptor;
                var transformedCompositeFilterDescriptor = new CompositeFilterDescriptor { LogicalOperator = compositeFilterDescriptor.LogicalOperator };
                foreach (var filterDescriptor in compositeFilterDescriptor.FilterDescriptors)
                {
                    transformedCompositeFilterDescriptor.FilterDescriptors.Add(TransformFilterDescriptors(filterDescriptor));
                }
                return transformedCompositeFilterDescriptor;
            }
            if (filter is FilterDescriptor)
            {
                var filterDescriptor = filter as FilterDescriptor;
                if (filterDescriptor.Value is DateTime)
                {
                    var value = (DateTime)filterDescriptor.Value;
                    switch (filterDescriptor.Operator)
                    {
                        case FilterOperator.IsEqualTo:
                            //convert the "is equal to <date><time>" filter to a "is greater than or equal to <date> 00:00:00" AND "is less than or equal to <date> 23:59:59"
                            var isEqualCompositeFilterDescriptor = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.And };
                            isEqualCompositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(filterDescriptor.Member,
                                FilterOperator.IsGreaterThanOrEqualTo, new DateTime(value.Year, value.Month, value.Day, 0, 0, 0)));
                            isEqualCompositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(filterDescriptor.Member,
                                FilterOperator.IsLessThanOrEqualTo, new DateTime(value.Year, value.Month, value.Day, 23, 59, 59)));
                            return isEqualCompositeFilterDescriptor;

                        case FilterOperator.IsNotEqualTo:
                            //convert the "is not equal to <date><time>" filter to a "is less than <date> 00:00:00" OR "is greater than <date> 23:59:59"
                            var notEqualCompositeFilterDescriptor = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.Or };
                            notEqualCompositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(filterDescriptor.Member,
                                FilterOperator.IsLessThan, new DateTime(value.Year, value.Month, value.Day, 0, 0, 0)));
                            notEqualCompositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(filterDescriptor.Member,
                                FilterOperator.IsGreaterThan, new DateTime(value.Year, value.Month, value.Day, 23, 59, 59)));
                            return notEqualCompositeFilterDescriptor;

                        case FilterOperator.IsGreaterThanOrEqualTo:
                            //convert the "is greater than or equal to <date><time>" filter to a "is greater than or equal to <date> 00:00:00"
                            filterDescriptor.Value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
                            return filterDescriptor;

                        case FilterOperator.IsGreaterThan:
                            //convert the "is greater than <date><time>" filter to a "is greater than <date> 23:59:59"
                            filterDescriptor.Value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
                            return filterDescriptor;

                        case FilterOperator.IsLessThanOrEqualTo:
                            //convert the "is less than or equal to <date><time>" filter to a "is less than or equal to <date> 23:59:59"
                            filterDescriptor.Value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
                            return filterDescriptor;

                        case FilterOperator.IsLessThan:
                            //convert the "is less than <date><time>" filter to a "is less than <date> 00:00:00"
                            filterDescriptor.Value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
                            return filterDescriptor;

                        default:
                            throw new Exception(string.Format("Filter operator '{0}' is not supported for DateTime member '{1}'", filterDescriptor.Operator, filterDescriptor.Member));
                    }
                }
            }
            return filter;
        }
    }
}