## Whats done and how to use ##
* When you run the application for the first time contacts list is empty, locations list is empty, so the only thing you can do is to type something in "Add location" textbox.
* If you enter 3 or more characters(for example 'lon') in textbox app sends request to solicitors.com for allowed locations if any locations found they apper under the text box. 
* If you click "Add" button near some locations it will be added to "Active Locations" list
* On "Active Locations" list you have some additional information on location (when it was last updated from solicitors.com, total number of contacts, number of new contacts). Also you have 2 actions "Delete" will delete location from "Active Locations", "Feab Data" - it will initiate process of grabbing contacts data for given location and adding it to the database.
* When you grab some contacts they immediately appear in contacts grid.
* Text boxes on top of colum titles are search boxes that allow to filter results by any field excep "Rating"

## Problems that was found and not resolved
1. **Duplicates** - there are some contacts that appear multiple times in the same location with different addresses and phones. It's not clear how to distingvish the case when it's two different contacts or the same contact that changed some values. So, I add only the first instance and ignore following ones.
2. **Nondeterministic search results** - https://www.solicitors.com/ for particular location does not return full list of contacts but just some subset and each time it is different subset choosen using some ranking algorithm. The best example of this is London. If you grab it for the first time it will return about 70 contacts but with any next grab this number will increase after many times I've got more than 500. So, on locations with big number of contacts there is no guaranted way to get them all.
## Next steps that I whould do if it would not just a demo ##
1. Reorgenise front-end code - it is not as clean as I want it to be (there are a lot of last-moment changes). So, if I had time I would extract some componens + cleanup states.
2. Add loading indication - disable controls when data is loading + show some spinner or another load indication.
3. Error handling - in current implementation I just hide errors silently. Ther should be some UI to show when something goes wrong.
4. Add results sorting.
5. Potentially results sorting, filtering and paging could be moved to back-end. It depends on how many data we have and how many request per hour.
6. Cover both back-end and front-end with unit-tests.
7. May be it makes sense when removing a location from database also remove related contacts. For now I decided not to do it.
## Installation steps ##
#### Prerequisites ####
* backend requires .net sdk 10 
* frontend requires node 22.13.0 and npm 11.0.0 (may be it can work on lower versions but didn't check)

#### Configuration ####
* backend configuration:
  * /Backend/InfoTrackDemo/Properties/launchSettings.json - all you may need to change here is "applicationUrl"
default value is "http://localhost:5268"
  * /Backend/InfoTrackDemo/appsettings.json or appsettings.Development.json. Here we have two important options: "DefaultConnection" - it should contain a valid connection string to postgresql server, user should have admin permissions on the database (app will create database and tables on the very first run); "AllowedOrigins" - this oune should contain url of front-end application for CORS, default value is "http://localhost:3000".
* frontend configuration:
  * http://localhost:3000/ is default url. If need to change - create .env file in /Frontend and add custom port.
  * /Frontend/src/services/base-service.ts Following code sets backend url - change it if needed
  ```
    constructor() {
        this.baseUrl = "http://localhost:5268/";
    }
  ```
#### Installation ####
* To build and run backend use following commands:
```
cd Backend
dotnet build
dotnet run --project ./InfoTrackDemo
```
On the first run it will create the database with locations and contacts tables.
If everything goes right then you can see swagger UI by following url http://localhost:5268/swagger/index.html.


* To install frontend use following commands:
```
cd Frontend
npm install
npm start
```
If everything goes right you could see the application by following url http://localhost:3000/
* ![Screenshot](/images/Screenshot1.PNG "Screenshot")
