using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyborier.Shared.Database
{
    /// <summary>
    /// Base Class for all DataTables.
    /// </summary>
    public abstract class BaseDataTable
    {
        #region Variables
        protected readonly string selectTemplate = "SELECT {0} FROM {1}";
        protected readonly string insertTemplate = "INSERT INTO {0} ({1}) VALUES ({2})";
        protected readonly string deleteTemplate = "DELETE {0} WHERE {1}";
        protected readonly string updateTemplate = "UPDATE {0} SET {1} WHERE {2}";

        protected BaseDataAdaptor dataAdaptor;        
        #endregion

        #region Constructors
        /// <summary>
        /// Create New Instance DataTable
        /// </summary>
        /// <param name="dataAdaptor"></param>
        public BaseDataTable(BaseDataAdaptor dataAdaptor)
        {
            this.dataAdaptor = dataAdaptor;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get the SQL Template for Select
        /// </summary>
        /// <returns></returns>
        protected abstract string GetSelectSQLTemplate();

        /// <summary>
        /// Get the SQL Template for Insert
        /// </summary>
        /// <returns></returns>
        protected abstract string GetInsertSQLTemplate();

        /// <summary>
        /// Get The SQL Template for Delete
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDeleteSQLTemplate();

        /// <summary>
        /// Get the SQL Template for Update.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetUpdateSQLTemplate();

        #endregion

    }
}
