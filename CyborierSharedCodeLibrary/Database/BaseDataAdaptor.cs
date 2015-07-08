/*
 * **************************************************************************
 * Developer : Pratap Singh, Manish (mpratap@Cyboriercompany.com)              *
 * Date : 23/10/2012                                                        *
 * Copyright  © 2012 Cyborier Sistemas, India Pvt Ltd. - All Rights Reserved   *
 * Unauthorized copying of this file, via any medium is strictly prohibited *
 * Proprietary and confidential.                                             *
 * **************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
namespace Cyborier.Shared.Database
{
    /// <summary>
    /// Abstract Class for the data Adaptor
    /// </summary>
    public abstract class BaseDataAdaptor
    {
        #region Variables
        protected DbConnection connection;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        protected BaseDataAdaptor()
        { }

        /// <summary>
        /// Create new instance of BaseDataAdaptor
        /// </summary>
        /// <param name="dbConnection"></param>
        protected BaseDataAdaptor(DbConnection dbConnection)
        {
            this.connection = dbConnection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets if Connected to the database or not
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return this.connection.State == ConnectionState.Open ? true : false;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Connect to the Database
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                this.connection.Open();
                if (this.connection.State == ConnectionState.Open)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return false;
        }

        /// <summary>
        /// Disconnect with Database
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            try
            {
                this.connection.Close();
                if (this.connection.State == ConnectionState.Closed)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                // TODO: Add Loggin ghere....
                throw;
            }
            return false;
        }

        /// <summary>
        /// check connection with the database
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            try
            {
                this.connection.Open();
                this.connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                // TODO: add Fucking logging./.
                throw;
            }
        }

        /// <summary>
        /// Insert New Record to the database
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(String command);

        public abstract int ExecuteNonQuery(DbCommand command);

        public abstract DataTable ExecuteReader(string query);

        public abstract DataTable ExecuteReader(DbCommand command);

        public abstract DbTransaction BeginTransaction();

        /// <summary>
        /// Returns the Command Instance for particuler DataAdaptor
        /// </summary>
        /// <returns></returns>
        public abstract DbCommand GetCommandInstance();
        #endregion

    }
}
