NZWalks is a Solution handles The Walk ,Region and the Diffeculity of a Walk .
The Solution has two projects an ASP.Net Web API project for handling the Server Side and ASP.Net MVC project for the UI.
The Backend Server Side handles the Requests via Controllers.
I added a repository pattern to make it loosely coupled.
Also I used the Identity library to make a user accounts and handles the Authorization correctly.
In the MVC project, I fetch the data by using HttpRequestMessage.
I applied a CRUD operations in the UI MVC project.
