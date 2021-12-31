# MovieCollection
![](https://github.com/DavoodArj-Mani/MovieCollection/blob/master/MovieCollection.png)
In this project several technologies have been used such as
+ ASP.NET
    + Entity Framework
    + Authentication 
    + JWT (Role)
 + Angular 12
    + Material
    + Creative Tim (Template) 
 + Docker
 
 ##### Using Visual Studio Code
 + Be sure that VS Code is installed in your machine `<link>` : <https://code.visualstudio.com/download>
 
 #### Git
 + Be sure that git is installed in your machine `<link>` : <https://git-scm.com/downloads>
 + Clone the project in your selected directory `$ git clone --branch master https://github.com/DavoodArj-Mani/MovieCollection.git`
 
 
 #### BackEnd 
- Open the project with Visual Studio 
- There is on extera directory `MovieCollectionWeb`, ignore this one after project has been opened.
- For db connection go to `appsettings.json` -> `DefaultConnection` change the connection string to your desire MSSQL server.
##### Directory 
+ Controller
+ Migration
+ Model
+ Peroperties
+ Services
+ ViewEntity

All the above directories are mostly sorted by three different factors 
+ App
  + Main metadata for the application such as Authentication, User, etc.
+ Core
  + Main metadata for the Movie Collection such as Movie, Collection, etc.
+ Shared
  + Shared metadata
  
##### Make Db ready 
+ Remove everything in migration directory 
+ Go to Package Manager Console
+ `Add-Migration createDbInformation` a new file will be created in 'Migration' directory.
+ The file will be opened automatically 
+ Delete the last part that will drop the tables that we want to create in the Db.
+ `Update-Database`
+ If the DB connection is correct, then tables will be created in your desired Db.

#### FrontEnd 
The UI is made with Angular
+ Open visual studio code 
+ Select sub directory in the main cloned folder `MovieCollectionWeb`
+ Config the connection url in the `src/app/shared/rest-api.service.ts` change `apiURL = "http://localhost:5000";` to backend url.
+ In the Terminal 
+ `npm install` in order to install all dependencies
+ After it finished `ng serve`
+ The angular web page will be run in `localhost:4200` but it can be changed in `protractor.conf.js` file.

#### Create Admin User
HTTP GET `http://localhost:5000/api/User/Install`

 
### End
### Have Fun
