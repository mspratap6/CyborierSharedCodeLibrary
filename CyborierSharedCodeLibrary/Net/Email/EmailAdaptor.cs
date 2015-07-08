using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Cyborier.Shared.Net.Email
{
    /// <summary>
    /// Adaptor to Send Emails
    /// </summary>
    public class EmailAdaptor : SmtpClient
    {
        #region Variables
        private EmailAdaptorConfigurations config;
        #endregion

        #region Constructor
        /// <summary>
        /// Create new instance of EmailAdaptor
        /// </summary>
        /// <param name="config"></param>
        public EmailAdaptor(EmailAdaptorConfigurations config)
            : this()
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            this.config = config;
            base.Host = config.SmtpServer;
            base.Port = config.Port;

            //this.smtpClient = new SmtpClient(config.SmtpServer , config.Port);
            var netCredential = new NetworkCredential();
            netCredential.Domain = config.Domain;
            netCredential.UserName = config.UserName;
            netCredential.Password = config.Password;

            base.Credentials = netCredential;
            base.EnableSsl = config.EnableSsl;
        }


        /// <summary>
        /// Initialize Instance of EmailAdaptor
        /// </summary>
        protected EmailAdaptor()
            : base()
        {
            //TODO: Default Initialization Goes Here..
        }
        #endregion
    }
}
