# CC.Input
## Installation
	Install Visual Studio 2022 and clone this repository to your local disk.
	Two browser instances will be running when the solution starts up.
 	The database is hosted on your local SQLExpress which you can view using Visual Studio 2022 -> View -> SQLServer Object Explorer after it is created with the command below.
 	Ensure both these projects are configured in Visual Studio 2022 as Multiple Startup Projects, right click the solution and select Configure Startup Projects.
    1. Blazor UI https://localhost:7027/
        PROJECT: CC.Input.UI.WebApp
    2. Web API https://localhost:7209/swagger/index.html
        PROJECT: CC.Input.API
        DB: CC 
		Visual Studio 2022 -> Tools -> NuGet Package Manager -> Package Manager Console
		Select the project 'CC.Input.API' in the project dropdown in the Package Manager Console.
		Type the following:
		PM> Update-Database
    That will create the CC DB on the host configured in appsettings.json.
   
## Talking Points  
1. I have used a C#/SQL long int in place of Numeric(13,0) due to familiarity but would look into why Numeric(13,0) is specified?
2. Mpan MPAN field name conventions?
3. Should we use DB Id column?
4. Which fields may require indexes and what types of indexes, further use cases?
5. Model what scope should be used, should we have model mappers for use with Data Transfer Objects (DTO's)?
6. CC.Input.Data currently has a reference to CC.Input.Logic.Model which should ideally not be referenced in that direction. 
7. Blazor UI & API Exception handling / security /logging?
8. Validation is seperate from Entity model to make it more useful/portable.

## TODO 
Reasearch 
1. Use optimal sql update command from SP for importing from file or something like this perhaps?
using (System.Data.SqlClient.SqlBulkCopy bulkCopy = 
new System.Data.SqlClient.SqlBulkCopy(sqlConnection))
{
    bulkCopy.DestinationTableName = destinationTableName;
    bulkCopy.BatchSize = 1000; // 1000 rows
    bulkCopy.WriteToServer(dataTable); // May also pass in DataRow[]
}
2. Upload with chunks in UI.
3. API should but does not "Retrieve all data from an uploaded file or Retrieve an individual item from within a file" but they do obtain them from the DB, is there a reason to get them from file?
