using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApplication.Models;

namespace WeatherApplication.BusinessLogic
{
    public class WeatherAppBusinessLogic
    {
        #region CityList
        /// <summary>
        /// GetCityList() reads the city.json file and return list of cities
        /// </summary>
        /// <returns>List<CityModel></returns>
        public List<CityModel> GetCityList()
        {
            List<CityModel> cityList = new List<CityModel>();
            try
            {
                using (StreamReader reader = new StreamReader("Data/City.json"))
                {
                    Console.WriteLine("Reading json file to get list of Cities.");
                    string cityData = reader.ReadToEnd();
                    cityList = JsonConvert.DeserializeObject<List<CityModel>>(cityData);
                    Console.WriteLine("List of Cities fetched successfully!!");
                }

            }// end of try
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message.ToString());
            }// end of catch

            return cityList;
        }// end of GetCityList

        #endregion

        #region GetWeatherByCityID

        /// <summary>
        /// GetWeatherDetailsById is used to make API call weatherService to get weatherDetails and returns list of
        /// weather details of all the cities provided as an input
        /// </summary>
        /// <param name="CityList"></param>
        /// <returns>weatherDetails</returns>
        public async Task<List<WeatherbyCityIdModel>> GetWeatherDetailsById(List<CityModel> CityList)
        {
            var client = new HttpClient();
            string url = string.Empty;
            string appID = string.Empty;
            string baseUrl = string.Empty;
            List<WeatherbyCityIdModel> weatherDetails = new List<WeatherbyCityIdModel>();

            try
            {
                /*fetching AppID and baseUrl from config
                 * Reason to add APPID and BaseUrl in config is to if any one them is changed we dont need to modify the code 
                 */
                appID = ConfigurationManager.AppSettings["AppID"].ToString();
                baseUrl = ConfigurationManager.AppSettings["BaseURL"].ToString();
                HttpResponseMessage response;
                //Verify if AppID and BaseUrl is present in config                
                if (appID != null & baseUrl != null & appID!="" & baseUrl!="")
                {
                    Console.WriteLine("AppID & BaseUrl successfully fetched from config.");
                    Console.WriteLine("API Call to WeatherService Started");
                    foreach (var city in CityList)
                    {
                        try
                        {
                            url = baseUrl + "?id=" + city.Id + "&appid=" + appID;
                            //making HTTPGET call to weatherService
                            response = await client.GetAsync(url);
                            string responseText = await response.Content.ReadAsStringAsync();
                            // converting the jsonresponse to Modeld
                            var cityWeather = JsonConvert.DeserializeObject<WeatherbyCityIdModel>(responseText);

                            weatherDetails.Add(cityWeather);

                        }// end of outer catch
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }// end of inner catch

                    }// end  of for each

                }// end of If
                else
                {
                Console.WriteLine("AppID/BaseURL not found in App.Config!!!");

                }// end of else
            }// end of outer try
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }// end of outer catch

            Console.WriteLine("API Call to WeatherService completed");
            return weatherDetails;

        }// end of function

        #endregion

    }// end of Class
}// end of namespace
