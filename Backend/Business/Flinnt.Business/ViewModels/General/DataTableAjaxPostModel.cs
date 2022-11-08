using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flinnt.Business.ViewModels
{
    public class DataTableAjaxPostModel
    {
        public DataTableAjaxPostModel()
        {
            PageSize = 10;
        }
        //public int Draw { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Column> Columns { get; set; }
        public Search Search { get; set; }
        public List<Order> Order { get; set; }
    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
        public List<string> ColumnNameList { get; set; }
        public string Regex { get; set; }
        //start:search data between dates 
        public bool isSearchBetweenDates { get; set; }
        public string endDate { get; set; }
        //End:search data between dates 
    }

    public class Order
    {
        public int Column { get; set; }
        public string ColumnName { get; set; }
        public string Dir { get; set; }
    }

    public class DataTableResponceModel<T>
    {
        //public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public List<T> data { get; set; }

        public object error { get; set; }
        public int totalPageCount { get; set; }
    }
}
