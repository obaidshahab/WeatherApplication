"# WeatherApplication" 
This is Weather Application Console program that gets the weather details of cities provided as an input in form of json.
To Execute this code, start the program in VisualStudio 2017 and go to Program.cs file and execute the code.
This code is contains various folders:
  1. Data: This contains the city.json files which has the details of city i.e CityID and Name
  2. BusinessLogic: This Contains WeatherAppBussinessLogic.cs file which contains methods to read the json files from the above folder. It also contains the HTTP request logic to get the weather details of each city.
  3. Models: This contains the model class of the application.
 
 The weatherDetails response recieved from WeatherAppBussinessLogic is saved into a json file in OutputFolder of the current directory. The OutputFolder can be found in bin/debug directory as currently the application is running in debug mode.
  
