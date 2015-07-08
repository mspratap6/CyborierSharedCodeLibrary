using System;
using System.IO;
using System.Text;
using Cyborier.Shared.Net.Ftp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharedCodeLibraryTest
{
    [TestClass]
    public class FtpClientUnitTest
    {
        [TestMethod]
        public void UploadFileTest()
        {
            const string fptServer = "indraindia.com";
            const string userName = "publicftpuser";
            const string pass = "indra@123";

            var ftpclient =
                new FtpClient(new FtpClientConfig { ServerAddress = fptServer, UserName = userName, Password = pass });

            const string fileName = "TestFile.txt";
            var fileToUpload = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            var fileToDownload = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName + ".dwn");
            var logSb = new StringBuilder();
            ftpclient.LogWritter = new StringWriter(logSb);

            var res = ftpclient.UploadFile(fileToUpload, fileName);
            if (!res)
            {
                Assert.Fail(string.Format("File Upload Failed. Log: \n{0}", logSb));
                return;
            }
            res = false;
            res = ftpclient.IsFileExists(fileName);
            if (!res)
            {
                Assert.Fail(string.Format("UPloaded File not Found in Server. log:\n{0}", logSb));
                return;
            }

            res = false;
            res = ftpclient.DownloadFile(fileName, fileToDownload);
            if (!res || !File.Exists(fileToDownload))
            {
                Assert.Fail(string.Format("File Download Failed. Log: \n{0}", logSb));
                return;
            }
        }
    }
}
