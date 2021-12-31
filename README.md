# MovieCollection
![](https://github.com/DavoodArj-Mani/MovieCollection/blob/master/MovieCollection.png)
![](https://github.com/DavoodArj-Mani/MovieCollection/blob/master/MovieCollection-Movie.png)

In this project several technologies have been used such as
+ ASP.NET Core 5.13
    + Entity Framework
    + Authentication 
    + JWT (Role)
 + Angular 12
    + Material
    + Reactive Form
    + Creative Tim (Template) 
    + Support Base64 for image
 + Docker
 + Unit Test (XUnit)
 
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
+ In the Docker-Composer Database is configured to be used 
+ After running the application under Docker 
+ go to 'https://github.com/DavoodArj-Mani/MovieCollection/blob/master/MovieCollection/Model/sql.txt' copy and run the script in your desier database

#### FrontEnd 
The UI is made with Angular
+ Open visual studio code 
+ Select sub directory in the main cloned folder `MovieCollectionWeb`
+ Config the connection url in the `src/app/shared/rest-api.service.ts` change `apiURL = "http://localhost:49501";` to backend url.
#### In case of not using the docker use this
+ In the Terminal 
+ `npm install` in order to install all dependencies
+ After it finished `ng serve`
+ The angular web page will be run in `localhost:4200` but it can be changed in `protractor.conf.js` file.

#### Run Application under Docker
+ go to Main directory with Terminal/CMD 'MovieCollection' where the 'docker-compose.yml' is located
+ Run 'docker-compose up'
+ Note: my machine does not have enogh resorces, so i was not able to work with the docker like this. I just executed the apllication with the terminal for UI and Docker-Compose in Visual Studio. 
 
### End
### Have Fun
