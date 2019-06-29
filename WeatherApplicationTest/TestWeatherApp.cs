using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApplication.BusinessLogic;

namespace WeatherApplicationTest
{
    [TestClass]
    public class TestWeatherApp
    {
        [TestMethod]
        public void TestGetCityList()
        {
            WeatherAppBusinessLogic objBL = new WeatherAppBusinessLogic();
            var cityList = objBL.GetCityList();
            Assert.IsTrue(cityList.Count > 0);

        }

        [TestMethod]
        public void TestGetWeatherByCityID()
        {
            WeatherAppBusinessLogic objBL = new WeatherAppBusinessLogic();
            var cityList = objBL.GetCityList();

            var weatherDetails = objBL.GetWeatherDetailsById(cityList);
            Assert.AreEqual(cityList.Count,weatherDetails.Result.Count);

        }
        [TestMethod]
        public void TestSaveWeatherDetailstoFiles()
        {
            var expectedMessage = "Files Saved successfully!!!";

            WeatherAppBusinessLogic objBL = new WeatherAppBusinessLogic();
            var cityList = objBL.GetCityList();

            var weatherDetails = objBL.GetWeatherDetailsById(cityList).Result;
                

            var actualMessage = WeatherApplication.Program.SaveWeatherDetailstoFiles(weatherDetails);

            Assert.AreEqual(expectedMessage, actualMessage, true);


        }
    }
}
