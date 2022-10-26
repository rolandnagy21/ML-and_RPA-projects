using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RPAprojectFW;

namespace RPAprojectUnitTest
{
    [TestClass]
    public class HasznaltalmaAPItests
    {

        string url;
        string searchWord;
        string workingDirectory;
        HasznaltalmaAPI api;

        [TestInitialize]
        public void BeforeEachTest()
        {
            url = "https://hasznaltalma.hu/";
            searchWord = "iPhone";

            api = new HasznaltalmaAPI();
        }

        [TestCleanup]
        public void AfterEachTest()
        {
            //api.driver.Dispose();
        }

        [TestMethod]
        public void TestStartSession()
        {
            api.StartSession(url, searchWord);
            Assert.AreEqual(url, api.driver.Url);
        }

        [TestMethod]
        public void TestSaveCurrentPage()
        {
            api.SaveCurrentPage(url);
        }
    }
}
