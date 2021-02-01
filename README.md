# Cambium

The project contains both API and UI. It is using .NET Core API 3.1 and Angular and MSTest are used for unit testing.  
  
To run the project please follow below steps:

1. In API `Startup.cs` change cors `WithOrigins` to point to your local `<address:port>`
2. Make sure the width and height of plateau is correct in `RoverController.cs` on `APIProject` `var plateau = new Plateau().PopulatePlateau(5, 5);`
3. Make sure both projects are set as start.
4. Run `npm install` on `UI` project - not needed if run with VS.

The API will place the rover on plateau/grid using multi-deimensional array and returns its new position and status. 
Bootstrap grid is used to show the result.
If two rovers have same instructions it will place it one move behind the occupied position. 

The UI uses pipe delimated .csv file to read the name and instructions using below format:

 > Rover1|MRML
 > Rover2|MRM
 > Rover3|MRM
