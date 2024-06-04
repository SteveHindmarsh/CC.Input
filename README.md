# CC.Input
## Installation
Install Visual Studio 2022 and clone this repository to your local disk.
Two browser instances will be running when the solution starts up.
The database can be hosted on your local SQLExpress which you can view using Visual Studio 2022 -> View -> SQLServer Object Explorer after it is created with the command below.
Ensure both these projects are configured in Visual Studio 2022 as Multiple Startup Projects, right click the solution and select Configure Startup Projects.
1. Blazor UI https://localhost:7027/

   PROJECT: CC.Input.UI.WebApp
3. Web API https://localhost:7209/swagger/index.html

   PROJECT: CC.Input.API
   
   DB: CC
   
   Using Visual Studio 2022 -> Tools -> NuGet Package Manager -> Package Manager Console
   
   Select the project 'CC.Input.API' in the project dropdown in the Package Manager Console.
   
   Type the following:
   
   PM> Update-Database
   
   That will create the CC DB on the host configured in appsettings.json (currently configured for your local SQLExpress).
   
   Also say yes to installing the SSL certificate (You may get 2 prompts).
   
   Some Test files to upload exist in ..\CC.Input\CC.Input.Logic.Testing.Unit\Data
   
## Talking Points  
1. I have used a C#/SQL long int in place of Numeric(13,0) due to familiarity but would look into why Numeric(13,0) is specified, I believe longint is more optimal at this point?
2. Mpan MPAN field name conventions in C#? Also other SQL/JS/API naming conventions.
3. Should we use DB Id column? How unique is MPAN?
4. Which fields may require indexes and what types of indexes, further use cases?
5. Object Models what scope should be used, should we have model mappers for use with Data Transfer Objects (DTO's)?
6. CC.Input.Data currently has a reference to CC.Input.Logic.Model which should ideally not be referenced in that direction. 
7. Blazor UI & API Exception handling / security /logging? / UX feedback & buzy indicators (progress bar with callback perhaps, 2 stage process upload/api?)
8. Validation is seperate from Entity model to make it more useful/portable.
9. The Unit tests demonstrate the seperation of concerns but are not comprehensive.
10. Need to further specify the parameters of the file, max file size, max lines, changeabilty expectation - how is the file prepared?
11. According to spec, API should, but mine does not, "Retrieve all data from an uploaded file or Retrieve an individual item from within a file" but they do obtain them from the DB, is there a reason to get them from file for API or is the spec incorrectly worded?
12. What do you think about my code? In particular...

    1. Solution and project namespace convention mirrored with project file placement?
    2. Seperation of concerns giving rise to concise, small and reusable projects? Are there too many - does this help or hinder?       

## TODO 
Reasearch 
1. Use sql update from a stored procedure for optimal import from either full or segmented file parts.
2. Or something like this utilising the C# ORM objects created from the file perhaps...
```using (System.Data.SqlClient.SqlBulkCopy bulkCopy = 
new System.Data.SqlClient.SqlBulkCopy(sqlConnection))
{
    bulkCopy.DestinationTableName = destinationTableName;
    bulkCopy.BatchSize = 1000; // 1000 rows
    bulkCopy.WriteToServer(dataTable); // May also pass in DataRow[]
}
```
3. Break the upload stream into data 'chunks' in both the UI & API. The file data is  currently streamed through both the UI & API for optimal in memory only processing, however would storing it as a file at some point be necessary in order to action point 4 below.
4. Depending on all of the above how should this be transactioned paying particular attention to failure/retry processing.

