# tech-library

## Instructions 
1. Install Visual Studio 2019 with .NET Core 3.1 SDK or later
2. Clone the github repository locally 
3. Open **TechLibrary.sln**
4. Right Click the project “TechLibrary.Web”, select “Open Command Prompt Here” 
5. `$ npm install`   *(install the vue js client npm packages)* 
6. `$ npm run serve` *(run the client on localhost)*
7. **F5** in Visual Studio to launch the API project in Debug mode 
8. Select “Yes” to the dialog prompt to “trust the ASP.NET Core SSL certificate” 

## Future to-do list
1. Request input validation at the beginning of each API route
2. Error handling (on both the UI and in the API)
3. Better naming in API endpoints (use nouns instead of verbs)
4. Code refactoring (i.e. move UI constants to a separate "config" component)
5. General UI improvements, and more sophisticated pagination controls
6. Better documentation