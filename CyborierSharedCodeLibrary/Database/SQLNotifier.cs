using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Cyborier.Shared.Database
{
    /// <summary>
    /// SQL Notifier , Notifies for the Change in the database.
    /// </summary>
    public class SQLNotifier : IDisposable
    {
        #region Variables
        /// <summary>
        /// Connection string will be used for the building connection
        /// </summary>
        protected string connectionString;
        /// <summary>
        /// Connection to be used for the Notification
        /// </summary>
        protected SqlConnection connection;

        /// <summary>
        /// Command will be used for notification
        /// </summary>
        private string sqlCommand;

        /// <summary>
        /// Current Command for the Notification
        /// </summary>
        private SqlCommand _CurrentCommand;
        #endregion

        #region Costructors
        /// <summary>
        /// Create new instance of SQL Notifier.
        /// </summary>
        protected SQLNotifier(string connectionString)
        {
            this.connectionString = connectionString;

            SqlDependency.Start(this.ConnectionString);
        }

        /// <summary>
        /// Create new instance of SQL Notifier
        /// </summary>
        /// <param name="connectionString">Connection string to the database server</param>
        /// <param name="Command">command which will be notified onthe change in db.</param>
        public SQLNotifier(string connectionString, string Command)
            : this(connectionString)
        {
            this.sqlCommand = Command;
        }
        #endregion

        #region Events
        /// <summary>
        /// Event to notify for the new notification from database
        /// </summary>
        public event EventHandler<SqlNotificationEventArgs> NewNotification
        {
            add
            {
                this._newMessage += value;
            }
            remove
            {
                this._newMessage -= value;
            }
        }

        private event EventHandler<SqlNotificationEventArgs> _newMessage;
        #endregion

        #region Properties
        /// <summary>
        /// Current Command Which we will use for the notification.
        /// </summary>
        public SqlCommand CurrentCommand
        {
            get
            {
                return this._CurrentCommand;
            }
            set
            {
                this._CurrentCommand = value;
            }
        }

        public SqlConnection CurrentConnection
        {
            get
            {
                this.connection = this.connection ?? new SqlConnection(this.ConnectionString);
                return this.connection;
            }
        }

        /// <summary>
        /// Gets the Connection string used in building connection.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }
        #endregion

        #region Methods

        protected virtual void OnNewMessage(SqlNotificationEventArgs notification)
        {
            if (this._newMessage != null)
                this._newMessage(this, notification);
        }

        /// <summary>
        /// Register Dependency.
        /// </summary>
        /// <returns></returns>
        public DataTable RegisterDependency()
        {

            this.CurrentCommand = new SqlCommand(this.sqlCommand, this.CurrentConnection);
            this.CurrentCommand.Notification = null;


            SqlDependency dependency = new SqlDependency(this.CurrentCommand);
            dependency.OnChange += this.dependency_OnChange;


            if (this.CurrentConnection.State == ConnectionState.Closed)
                this.CurrentConnection.Open();
            try
            {

                DataTable dt = new DataTable();
                dt.Load(this.CurrentCommand.ExecuteReader(CommandBehavior.CloseConnection));
                return dt;
            }
            catch { return null; }

        }
        #endregion

        #region EventHandlers
        void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;

            dependency.OnChange -= new OnChangeEventHandler(dependency_OnChange);

            this.OnNewMessage(e);
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            SqlDependency.Stop(this.ConnectionString);
        }

        #endregion
    }
}
