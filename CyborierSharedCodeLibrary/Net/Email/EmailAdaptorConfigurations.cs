using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyborier.Shared.Net.Email
{
    /// <summary>
    /// Configuration for Email Adaptor
    /// </summary>
    public class EmailAdaptorConfigurations
        : Serialization.XMLSerializableObject
    {
        #region Properties
        public String SmtpServer { get; set; }
        public int Port { get; set; }
        public String Domain { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public bool EnableSsl { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Deserialize Instance from File.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static EmailAdaptorConfigurations Deserialize(string fileName)
        {
            return (EmailAdaptorConfigurations)Deserialize(typeof(EmailAdaptorConfigurations), fileName);
        }
        #endregion

        /// <summary>
        /// TEst Serialize Object.
        /// </summary>
        public static void TestSerialize(string filePath)
        {
            var config = new EmailAdaptorConfigurations {                  
                 Domain = "example.com",
                 EnableSsl = true,
                 UserName = "testUSer@example.com",
                 Password = "@example123",
                 SmtpServer = "smtp.example.com",
                 Port = 25               
            };

            config.fileName = filePath;
            config.Serialize();            
        }
    }
}
