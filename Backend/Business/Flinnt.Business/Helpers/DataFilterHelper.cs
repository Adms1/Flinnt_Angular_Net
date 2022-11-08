using Flinnt.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using static System.Boolean;

namespace Flinnt.Business.Helpers
{
    public static class DataFilterHelper
    {
        public static PropertyInfo GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            while (true)
            {
                if (propName == null) throw new ArgumentException("Value cannot be null.", nameof(propName));

                if (propName.Contains(".")) //complex type nested
                {
                    var temp = propName.Split(new[] { '.' }, 2);
                    src = Activator.CreateInstance(GetPropertyValue(src, temp[0]).PropertyType);
                    propName = temp[1];
                }
                else
                {
                    return src.GetType().GetProperty(propName);
                }
            }
        }

        public static string CreateSearchString<T>(DataTableAjaxPostModel model)
        {
            const string opertatorAnd = " && ";
            const string opertator = " || ";
            var searchStr = new StringBuilder();
            if (model.Search != null && !string.IsNullOrEmpty(model.Search.Value) && model.Search.Value != null && model.Search.ColumnNameList != null &&
                    model.Search.ColumnNameList.Count > 0)
            {
                foreach (var objectSerach in model.Search.ColumnNameList)
                {
                    var te = Activator.CreateInstance(typeof(T));
                    var t = GetPropertyValue(te, objectSerach);
                    string tableNotNUll = string.Empty;
                    string[] tableObject = new string[] { };

                    if (objectSerach.Contains("."))
                    {
                        tableObject = objectSerach.Split('.');
                    }
                    if (tableObject.Any())
                    {
                        tableNotNUll = "( it." + tableObject[0] + " != null && ";
                    }
                    string notNUll = "( it." + objectSerach + " != null && ";
                    if (t == null) continue;
                    //if (t.PropertyType.Name.ToLower().Contains("null")) continue;

                    if (t.PropertyType.FullName.ToLower().Contains("date") || t.PropertyType.Name.ToLower().Contains("time"))
                    {
                        var serchString = objectSerach;
                        if (!t.PropertyType.Name.ToLower().Contains("date") || !t.PropertyType.Name.ToLower().Contains("time"))
                        {
                            serchString = objectSerach + ".HasValue && " + objectSerach + ".Value.ToString()";
                        }
                        if (searchStr.Length == 0)
                        {
                            searchStr.Append(tableNotNUll);
                        }
                        else
                        {
                            searchStr.Append(opertator + tableNotNUll);
                        }
                        searchStr.Append(notNUll);

                        searchStr.AppendFormat(" it." + serchString +
                                               ".ToString().Contains(@0))");
                        if (tableObject.Any())
                        {
                            searchStr.Append(")");
                        }
                    }
                    else
                    if (t.PropertyType.Name.ToLower().Contains("int") ||
                             t.PropertyType.Name.ToLower().Contains("decimal"))
                    {
                        //if (!isstring) continue;

                        if (searchStr.Length == 0)
                        {
                            searchStr.Append(tableNotNUll);
                        }
                        else
                        {
                            searchStr.Append(opertator + tableNotNUll);
                        }
                        searchStr.Append(notNUll);
                        searchStr.Append(" it." + objectSerach + ".ToString().Contains(@0))");
                        if (tableObject.Any())
                        {
                            searchStr.Append(")");
                        }

                    }
                    else if (t.PropertyType.Name.ToLower().Contains("bool"))
                    {
                        TryParse(model.Search.Value, out var parsedValue);

                        if (parsedValue)
                        {
                            searchStr.Append("it." + objectSerach + "= (@0) ");
                        }
                    }
                    else
                    {
                        //if (isstring) continue;    
                        if (searchStr.Length == 0)
                        {
                            searchStr.Append(tableNotNUll);
                        }
                        else
                        {
                            searchStr.Append(opertator + tableNotNUll);
                        }
                        searchStr.Append(notNUll);
                        searchStr.Append(" it." + objectSerach + ".ToString().ToLower().Contains(@0))");
                        if (tableObject.Any())
                        {
                            searchStr.Append(")");
                        }

                    }
                }
            }
            if (model.Columns != null)
            {
                foreach (var column in model.Columns)
                {
                    var objectSerach = column.Name;
                    var searchValue = column.Search.Value;
                    if (!string.IsNullOrEmpty(searchValue) || !string.IsNullOrEmpty(column.Search.endDate))
                    {
                        var te = Activator.CreateInstance(typeof(T));
                        var t = GetPropertyValue(te, objectSerach);
                        string tableNotNUll = string.Empty;
                        string[] tableObject = new string[] { };

                        if (objectSerach.Contains("."))
                        {
                            tableObject = objectSerach.Split('.');
                        }
                        if (tableObject.Any())
                        {
                            tableNotNUll = "( it." + tableObject[0] + " != null && ";
                        }
                        string notNUll = "( it." + objectSerach + " != null && ";
                        if (t == null) continue;


                        if (t.PropertyType.FullName.ToLower().Contains("date") || t.PropertyType.Name.ToLower().Contains("time"))
                        {
                            var serchString = objectSerach;
                            if (!t.PropertyType.Name.ToLower().Contains("date") || !t.PropertyType.Name.ToLower().Contains("time"))
                            {
                                serchString = objectSerach + ".HasValue && " + " it." + objectSerach + ".Value";
                            }
                            if (searchStr.Length == 0)
                            {
                                searchStr.Append(tableNotNUll);
                            }
                            else
                            {
                                searchStr.Append(opertatorAnd + tableNotNUll);
                            }
                            searchStr.Append(notNUll);
                            if (column.Search.isSearchBetweenDates)
                            {
                                if (!String.IsNullOrWhiteSpace(column.Search.endDate) && !String.IsNullOrWhiteSpace(column.Search.Value))
                                {
                                    searchStr.AppendFormat(" it." + serchString + ".Date >=Convert.ToDateTime(\"" + searchValue.ToString() + "\").Date" + opertatorAnd
                                        + " it." + serchString + ".Date <=Convert.ToDateTime(\"" + column.Search.endDate.ToString() + "\").Date)");
                                }
                                else if (String.IsNullOrWhiteSpace(column.Search.endDate) && !String.IsNullOrWhiteSpace(column.Search.Value))
                                {
                                    searchStr.AppendFormat(" it." + serchString + ".Date >=Convert.ToDateTime(\"" + Convert.ToDateTime(searchValue) + "\").Date)");
                                }
                                else if (!String.IsNullOrWhiteSpace(column.Search.endDate) && String.IsNullOrWhiteSpace(column.Search.Value))
                                {
                                    searchStr.AppendFormat(" it." + serchString + ".Date <=Convert.ToDateTime(\"" + Convert.ToDateTime(column.Search.endDate) + "\").Date)");
                                }
                            }
                            else
                            {
                                searchStr.AppendFormat(" it." + serchString +
                                                       ".Date.ToString().Contains(\"" + Convert.ToDateTime(searchValue) + "\"))");
                            }
                            if (tableObject.Any())
                            {
                                searchStr.Append(")");
                            }
                        }
                        else
                        if (t.PropertyType.FullName.ToLower().Contains("int") ||
                                 t.PropertyType.FullName.ToLower().Contains("decimal"))
                        {
                            //if (!isstring) continue;

                            if (searchStr.Length == 0)
                            {
                                searchStr.Append(tableNotNUll);
                            }
                            else
                            {
                                searchStr.Append(opertatorAnd + tableNotNUll);
                            }
                            searchStr.Append(notNUll);
                            searchStr.Append(" it." + objectSerach + "=" + searchValue + ")");
                            if (tableObject.Any())
                            {
                                searchStr.Append(")");
                            }

                        }
                        else if (t.PropertyType.Name.ToLower().Contains("bool"))
                        {
                            TryParse(column.Search.Value, out var parsedValue);

                            if (parsedValue)
                            {
                                searchStr.Append("it." + objectSerach + "= (" + searchValue + ") ");
                            }
                        }
                        else
                        {
                            //if (isstring) continue;    
                            if (searchStr.Length == 0)
                            {
                                searchStr.Append(tableNotNUll);
                            }
                            else
                            {
                                searchStr.Append(opertatorAnd + tableNotNUll);
                            }
                            searchStr.Append(notNUll);
                            if (column.Search.isSearchBetweenDates)
                            {
                                if (column.Search.endDate != null && column.Search.Value != null)
                                {
                                    searchStr.AppendFormat(" it." + objectSerach + " >=Convert.ToDateTime(" + searchValue + ")" + opertatorAnd
                                        + " it." + objectSerach + ".Date <=Convert.ToDateTime(" + column.Search.endDate + "))");
                                }
                                else if (column.Search.endDate == null && column.Search.Value != null)
                                {
                                    searchStr.AppendFormat(" it." + objectSerach + " >=Convert.ToDateTime(\"" + Convert.ToDateTime(searchValue) + "\"))");
                                }
                                else if (column.Search.endDate != null && column.Search.Value == null)
                                {
                                    searchStr.AppendFormat(" it." + objectSerach + " <=Convert.ToDateTime(\"" + Convert.ToDateTime(column.Search.endDate) + "\"))");
                                }
                            }
                            else
                            {

                                searchStr.Append(" it." + objectSerach + ".ToString().ToLower().Contains(\"" + searchValue + "\"))");
                            }
                            if (tableObject.Any())
                            {
                                searchStr.Append(")");
                            }

                        }
                    }
                }
            }
            return searchStr.ToString();
        }

        public static DataTableResponceModel<T> GetFilteredData<T>(this IEnumerable<T> source, DataTableAjaxPostModel model)
        {
            try
            {
                var enumerable = source as T[] ?? source.ToArray();
                var result = enumerable.AsQueryable();
                var result2 = enumerable.AsQueryable();
                var totalcount = enumerable.Length;
                if (model.Search != null && model.Search.Value != null)
                    model.Search.Value = model.Search.Value.Trim().ToLower();
                #region filter records



                //var isstring = model.Search.Value.All(char.IsDigit);
                var searchCriteria = CreateSearchString<T>(model);
                if (!string.IsNullOrEmpty(searchCriteria))
                {
                    result = !string.IsNullOrEmpty(searchCriteria)
                        ? enumerable.AsQueryable().Where(searchCriteria, model.Search.Value.ToLower())
                        : enumerable.AsQueryable();
                }



                var filteredResultsCount = result.Any() ? result.Count() : 0;

                #endregion

                #region order by
                if (model.Order != null && model.Order.Any() && !string.IsNullOrEmpty(model.Order[0].ColumnName))
                {
                    if (model.Order[0].ColumnName.Contains("DateStr"))
                    {
                        var columnName = model.Order[0].ColumnName.Replace("DateStr", "").ToString();
                        model.Order[0].ColumnName = typeof(T).GetProperties().Any(p => p.Name == columnName) ? columnName : model.Order[0].ColumnName;
                    }
                    string objectSerach = model.Order[0].ColumnName;
                    string tableNotNUll = string.Empty;
                    var searchStr = new StringBuilder();
                    string[] tableObject = new string[] { };
                    if (objectSerach.Contains("."))
                    {
                        tableObject = objectSerach.Split('.');
                    }
                    if (tableObject.Any())
                    {
                        tableNotNUll = "( it." + tableObject[0] + " != null && ";
                    }
                    string notNUll = "( it." + objectSerach + " != null) ";
                    searchStr = searchStr.Append(tableNotNUll + notNUll);
                    if (tableObject.Any())
                    {
                        searchStr = searchStr.Append(")");
                    }
                    result2 = result.AsQueryable().Where(searchStr.ToString()).OrderBy(model.Order[0].ColumnName + " " + model.Order[0].Dir);
                    if (tableObject.Any())
                    {
                        result2 = result2.Concat(result.Where("it." + tableObject[0] + "==null"));
                    }
                    else
                    {
                        result2 = result2.Concat(result.Where("it." + objectSerach + "==null"));
                    }


                }
                else
                {
                    result2 = result;
                }


                #endregion

                var skipAmount = model.Page > 0 ? model.PageSize * (model.Page - 1) : 0;
                var mod = filteredResultsCount % model.PageSize;
                var totalPageCount = (filteredResultsCount / model.PageSize) + (mod == 0 ? 0 : 1);
                return new DataTableResponceModel<T>
                {
                    data = new List<T>(result2.Skip(skipAmount).Take(model.PageSize)),
                    recordsTotal = totalcount,
                    recordsFiltered = filteredResultsCount,
                    totalPageCount = totalPageCount
                };
            }
            catch (Exception ex)
            {
                return new DataTableResponceModel<T>
                {
                    data = new List<T>()
                };
            }
        }

    }
}
