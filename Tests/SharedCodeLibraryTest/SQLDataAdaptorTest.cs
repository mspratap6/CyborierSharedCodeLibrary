using Cyborier.Shared.Database.SQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Diagnostics;

namespace SharedCodeLibraryTest
{


    /// <summary>
    ///This is a test class for SQLDataAdaptorTest and is intended
    ///to contain all SQLDataAdaptorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SQLDataAdaptorTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ExecuteNonQuery
        ///</summary>
        [TestMethod()]
        public void ExecuteNonQueryTestUsingQuery()
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Rooster;User Id=sa;Password=sa_2008;");
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
                return;
            }
            SQLDataAdaptor target = new SQLDataAdaptor(connection);
            string command = "insert into USER_LOGIN_HISTORY ( [USER_ID] , LOGIN_DATE_TIME) values ( 'gto','2012-09-29 19:30:00')";
            int expected = 1;
            int actual;
            actual = target.ExecuteNonQuery(command);
            try
            {
                Assert.AreEqual(expected, actual);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
            }
        }

        /// <summary>
        ///A test for ExecuteNonQuery
        ///</summary>
        [TestMethod()]
        public void ExecuteNonQueryTestUsingCommandObj()
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Rooster;User Id=sa;Password=sa_2008;");
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
                return;
            }
            SQLDataAdaptor target = new SQLDataAdaptor(connection);
            string query = "insert into USER_LOGIN_HISTORY ( [USER_ID] , LOGIN_DATE_TIME) values ( 'gto','2012-09-29 19:34:00')";
            SqlCommand command = new SqlCommand(query);
            int expected = 1;
            int actual;
          
            try
            {
                actual = target.ExecuteNonQuery(command);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
            }
        }

        /// <summary>
        ///A test for ExecuteReader
        ///</summary>
        [TestMethod()]
        public void ExecuteReaderTest()
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Rooster;User Id=sa;Password=sa_2008;");
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
                return;
            }
            SQLDataAdaptor target = new SQLDataAdaptor(connection);
            DbCommand command = new SqlCommand("Select * from USER_LOGIN_HISTORY");

            try
            {
                var table = target.ExecuteReader(command);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Debug.WriteLine("User ID : " + row["USER_ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Test Failed \n" + ex.Message, ex);
            }
            
        }

        /// <summary>
        ///A test for ExecuteReader
        ///</summary>
        [TestMethod()]
        public void ExecuteReaderTestUsingQuery()
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Rooster;User Id=sa;Password=sa_2008;");
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
                return;
            }
            SQLDataAdaptor target = new SQLDataAdaptor(connection);
            string command = "Select * from USER_LOGIN_HISTORY";

            try
            {
                var table = target.ExecuteReader(command);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Debug.WriteLine("User ID : " + row["USER_ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Test Failed \n" + ex.Message, ex);
            }
        }
    }
}
