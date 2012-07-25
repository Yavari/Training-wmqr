Instructions
============

* Create a new Empty ASP.NET MVC4 Web Application
* Create a new Clas Library project called Training-wmqrTests
* Install the following packages:
	* Install-Package NUnit (test project)
	* Install-Package Moq (test project)
	* Install-Package System.Data.SQLite (both projects)
* Add references to the ActiveRecord dlls and add the Framework classes
* Setup in memory database in the test project and change the test server to build for x86
* Add the user model
* Add the document model
* Add relationship between users and documents
* I should be able to tag favourite documents
	