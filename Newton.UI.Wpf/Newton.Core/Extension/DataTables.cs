using System.Collections.Generic;
using System.Data;

namespace Pluto.Extensions
{
    /// <summary>
    /// Class of functions that work on DataTables
    /// </summary>
    public static class DataTables
    {
        #region DataRows

        /// <summary>
        /// Checks each of the columns exist in the datarow passed.
        /// </summary>
        public static bool ContainsColumn(this DataRow dataRow, string column)
        {
            return ContainsColumns(dataRow.Table, new string[] { column }, false);
        }

        /// <summary>
        /// Checks each of the columns exist in the datarow passed.
        /// </summary>
        public static bool ContainsColumn(this DataRow dataRow, string column, bool caseSensitive)
        {
            return ContainsColumns(dataRow.Table, new string[] { column }, caseSensitive);
        }

        /// <summary>
        /// Checks each of the columns exist in the datarow passed.
        /// </summary>
        public static bool ContainsColumns(this DataRow dataRow, string[] columns)
        {
            return ContainsColumns(dataRow.Table, columns, false);
        }

        /// <summary>
        /// Checks each of the columns exist in the datarow passed.
        /// </summary>
        public static bool ContainsColumns(this DataRow dataRow, string[] columns, bool caseSensitive)
        {
            return ContainsColumns(dataRow.Table, columns, caseSensitive);
        }

        /// <summary>
        /// Checks each of the columns exist in the datatable passed.
        /// </summary>
        public static bool ContainsColumn(this DataTable dataTable, string column)
        {
            return ContainsColumns(dataTable, new string[] { column }, false);
        }

        /// <summary>
        /// Checks each of the columns exist in the datatable passed.
        /// </summary>
        public static bool ContainsColumn(this DataTable dataTable, string column, bool caseSensitive)
        {
            return ContainsColumns(dataTable, new string[] { column }, caseSensitive);
        }

        /// <summary>
        /// Checks each of the columns exist in the datatable passed.
        /// </summary>
        public static bool ContainsColumns(this DataTable dataTable, string[] columns)
        {
            return ContainsColumns(dataTable, columns, false);
        }

        /// <summary>
        /// Checks each of the columns exist in the datatable passed.
        /// </summary>
        public static bool ContainsColumns(this DataTable dataTable, string[] columns, bool caseSensitive)
        {
            return columns.FoundAllInArray(dataTable.GetColumnHeaders(), caseSensitive);
        }

        /// <summary>
        /// Checks each of the columns exist in the datatable passed.
        /// </summary>
        public static string[] GetColumnHeaders(this DataTable dataTable)
        {
            List<string> columnNames = new List<string>();
            foreach (DataColumn column in dataTable.Columns)
            {
                columnNames.Add(column.ColumnName);
            }
            return columnNames.ToArray();
        }

        #endregion

        #region DataTables

        /// <summary>
        /// Returns a collection of the table names for all the datatables
        /// </summary>
        public static IEnumerable<string> TableNames(this DataTableCollection source)
        {
            List<string> list = new List<string>();
            foreach (DataTable dataTable in source)
            {
                list.Add(dataTable.TableName);
            }
            return list;
        }

        #endregion

    }
}
