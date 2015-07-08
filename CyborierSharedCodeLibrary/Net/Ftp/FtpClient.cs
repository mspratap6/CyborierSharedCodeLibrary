using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Cyborier.Shared.Net.Ftp
{
    /// <summary>
    /// FTP Client For FTP Operations.
    /// </summary>
    public class FtpClient
    {
        private readonly FtpClientConfig _config;

        #region Fields
        #endregion

        #region Constructor
        /// <summary>
        /// Create Instance of Ftp Client.
        /// </summary>
        public FtpClient(FtpClientConfig config)
        {
            _config = config;
        }
        #endregion

        #region Properties

        public TextWriter LogWritter { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Upload File to FTP Server.
        /// </summary>
        /// <param name="sourceFilePath">source file Path</param>
        /// <param name="remoteFilePath">remote file reletive path</param>
        /// <returns>true if file uploaded false if failed.</returns>
        /// <exception cref="FileNotFoundException">thorows if source file not found in disk.</exception>
        public bool UploadFile(string sourceFilePath, string remoteFilePath)
        {
            if (!File.Exists(sourceFilePath))
            {
                throw new FileNotFoundException(string.Format("Source File: {0} Not Found.", sourceFilePath));
            }

            var fileInf = new FileInfo(sourceFilePath);
            var targetFilePath = String.Format("ftp://{0}/{1}", _config.ServerAddress, remoteFilePath);

            WriteLog(string.Format("Initiating File Upload. Server: {0}", _config.ServerAddress));
            WriteLog(string.Format("Source File: {0} , Target File: {1}", sourceFilePath, targetFilePath));


            var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(targetFilePath));
            reqFtp.Credentials = new NetworkCredential(_config.UserName, _config.Password);
            reqFtp.KeepAlive = false;
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.UseBinary = true;

            reqFtp.ContentLength = fileInf.Length;

            const int buffLength = 2048;
            var buff = new byte[buffLength];
            FileStream fs;

            try
            {
                fs = fileInf.OpenRead();
            }
            catch (Exception ex)
            {
                WriteLog("Error: Cant Open Source File to Read. Exception: \n" + ex);
                return false;
            }

            try
            {
                var start = DateTime.Now;
                var bytsTransferd = 0;
                WriteLog(string.Format("Initiating File Transfer Total File Size: {0} Bytes.", fileInf.Length));
                var strm = reqFtp.GetRequestStream();
                var contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    bytsTransferd += contentLen;
                    strm.Write(buff, 0, contentLen);
                    WriteLog(string.Format("{0} Bytes Transfered", bytsTransferd));

                    contentLen = fs.Read(buff, 0, buffLength);
                }
                WriteLog(string.Format("Transfer Completed in {0}", DateTime.Now - start));
                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                WriteLog(string.Format("Error: Can't Transfer File to FTP due to Exception: \n{0}", ex));
                return false;
            }

            WriteLog("Verifing File at Target Site.");
            try
            {
                reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(targetFilePath));
                reqFtp.Credentials = new NetworkCredential(_config.UserName, _config.Password);
                reqFtp.KeepAlive = false;
                reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;

                var response = (FtpWebResponse)reqFtp.GetResponse();
                var ftpStream = response.GetResponseStream();
                var fileSize = response.ContentLength;

                if (ftpStream != null) ftpStream.Close();
                response.Close();

                if (fileSize == fileInf.Length)
                {
                    WriteLog(string.Format("Target File {0} Verification Success", targetFilePath));
                    return true;
                }
                else
                {
                    WriteLog(string.Format("Target File {0} filesize fismatch, orignal file size: {1} remote file size: {2} , Upload Operation Failed.", targetFilePath, fileInf.Length, fileSize));
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteLog(string.Format("Target File Verification Failed. Exception: \n{0}", ex));
                return false;
            }
        }

        /// <summary>
        /// Download File From Server.
        /// </summary>
        /// <param name="remoteFileRelativePath">relative path of remote file</param>
        /// <param name="outFilePath">output file path</param>
        /// <returns>true if downloaded false if failed.</returns>
        public bool DownloadFile(string remoteFileRelativePath, string outFilePath)
        {
            if (string.IsNullOrEmpty(remoteFileRelativePath))
                throw new ArgumentNullException("remoteFileRelativePath");
            if (String.IsNullOrEmpty(outFilePath))
                throw new ArgumentNullException("outFilePath");

            var retVal = false;

            // Output stream for the out file.
            using (var outputStream = new FileStream(outFilePath, FileMode.Create))
            {

                // File path for the Remote File.
                var remoteFilePath = String.Format("ftp://{0}/{1}", _config.ServerAddress, remoteFileRelativePath);

                WriteLog(string.Format("Initiating File Download. Server: {0}", _config.ServerAddress));
                WriteLog(string.Format("Source File: {0} , Target File: {1}", remoteFilePath, outFilePath));

                var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(remoteFilePath));
                reqFtp.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential(_config.UserName, _config.Password);

                FtpWebResponse response;
                try
                {
                    WriteLog("Reqesting Server for Response.");
                    response = (FtpWebResponse)reqFtp.GetResponse();
                    WriteLog(string.Format("Response Received From Server, Status Code: {0} , Description: {1}",
                        response.StatusCode, response.StatusDescription));
                }
                catch (Exception ex)
                {
                    WriteLog("Can't Get Response From Server due to Exception: \n" + ex);
                    return false;
                }

                try
                {
                    WriteLog("Initating Download of File.");
                    var start = DateTime.Now;
                    var bytesDownloaded = 0;
                    var ftpStream = response.GetResponseStream();

                    var cl = response.ContentLength;
                    WriteLog(string.Format("File Size: {0} Bytes", cl));

                    const int bufferSize = 2048;
                    var buffer = new byte[bufferSize];

                    if (ftpStream != null)
                    {
                        var readCount = ftpStream.Read(buffer, 0, bufferSize);
                        while (readCount > 0)
                        {
                            bytesDownloaded += readCount;

                            outputStream.Write(buffer, 0, readCount);
                            WriteLog(string.Format("{0} bytes Downloaded", bytesDownloaded));
                            readCount = ftpStream.Read(buffer, 0, bufferSize);
                        }

                        ftpStream.Close();
                        if (File.Exists(outFilePath))
                        {
                            WriteLog(string.Format("File Download Completed in {0}", DateTime.Now - start));
                            return true;
                        }
                        else
                        {
                            WriteLog("Downloaded File Not found on disk.");
                        }
                    }
                    else
                    {
                        WriteLog("Error: No Data Stream found in the Response.");
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("Error: Can't Download File due to Exception: \n" + ex);
                }
                finally
                {
                    outputStream.Close();
                    response.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// List files in a directory
        /// </summary>
        /// <param name="directoryRelativePath">path of directory to list files in.</param>
        /// <returns>returns array of string contain file path</returns>
        protected string[] ListDirectoryFiles(string directoryRelativePath)
        {
            // TODO: we will implement this later.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rename a remote file.
        /// </summary>
        /// <param name="orignalFileName">original file relative path</param>
        /// <param name="newFileName">new File Name.</param>
        /// <returns></returns>
        protected bool RenameFile(string orignalFileName, string newFileName)
        {
            // TODO: we will implement it later.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if file exists on server.
        /// </summary>
        /// <param name="remoteFileRelativePath">path of file</param>
        /// <returns>true if file exists else false</returns>
        public bool IsFileExists(string remoteFileRelativePath)
        {
            try
            {
                var targetFilePath = String.Format("ftp://{0}/{1}", _config.ServerAddress, remoteFileRelativePath);
                var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(targetFilePath));
                reqFtp.Credentials = new NetworkCredential(_config.UserName, _config.Password);
                reqFtp.KeepAlive = false;
                reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
                var response = (FtpWebResponse)reqFtp.GetResponse();
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    WriteLog("File Not Found in server.");
                    return false;
                }

                response.Close();
                WriteLog(string.Format("File Found, Stauts: {0}", response.StatusCode));
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(string.Format("Target File Existence Check Failed. Exception: \n{0}", ex));
                return false;
            }
        }
        #endregion

        #region Private Metods
        /// <summary>
        /// Write log to Log Writter.
        /// </summary>
        /// <param name="message"></param>
        private void WriteLog(string message)
        {
            var logMessage = String.Format("{0}\t{1}", DateTime.Now.ToString("yy-MM-yyyy hh:mm:ss"),message);

            if (LogWritter != null)
            {
                LogWritter.WriteLine(logMessage);
            }
        }
        #endregion
    }

    public class FtpClientConfig
    {
        public string ServerAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // TODO: any more configuration will go here as required in feture.
    }
}
