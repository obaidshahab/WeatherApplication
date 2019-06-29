using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WeatherApplication.BusinessLogic;
using WeatherApplication.Models;


namespace WeatherApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Weather Application Started:");
            WeatherAppBusinessLogic objBL = new WeatherAppBusinessLogic();
            // Getting list of cities
            var cityList = objBL.GetCityList();

            // Getting weather details
            var weatherDetails = objBL.GetWeatherDetailsById(cityList).Result;

            // Saving weatherDetails to Json
            var message = SaveWeatherDetailstoFiles(weatherDetails);
            Console.WriteLine(message);

            Console.ReadLine();
        }

        /// <summary>
        /// To Save the weather details into json file
        /// </summary>
        /// <param name="weatherDetails"></param>
        /// <returns></returns>

        public static string SaveWeatherDetailstoFiles(List<WeatherbyCityIdModel> weatherDetails)
        {
            try
            {
                Console.WriteLine("Saving weather details to json file");
                // converting List to json String
                string jsonData = JsonConvert.SerializeObject(weatherDetails, Formatting.None);

                //getting current directory
                var currentDirectory=Environment.CurrentDirectory;
                string path = currentDirectory + "\\OutputFolder";
                // verifying whether outputfolder exists. If not, code will create a newfolder
                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);

                //Giving filename in dateformat to manintain history
                string fileName = DateTime.Now.ToString("MM/dd/yyyy")+".json";
                               
                var fullPath = path + "\\"+fileName;
                
                //saving the file
                System.IO.File.WriteAllText(fullPath, jsonData);

            }// end of try
            catch(Exception ex)
            {
                return "Exception Occurred while saving Data!";
            }// end of catch

            return "Files Saved successfully!!!";
        }// end of function


    }// end of class
}//end of namespace
