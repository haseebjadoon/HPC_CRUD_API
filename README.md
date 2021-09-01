Hello, welcome to the CRUD API documentation.

The application provides the following features:
* CRUD Backend in .Net
* CRUD Frontend in Angular
* In-Memory database support with Entity framework.
* Action file for github Continuous Integration.
* A sample Test Application

How to run:
* Please use the already existing Dockerfile to run the project.
* There is also a batch file named **docker_start** to help you save some typing. If run from command line, it would build and run the API on docker. Inside docker, the application runs on port 80 but I have forwarded that to 8080 for the local machine. Please use the URL http://localhost:8080 to perform the CRUD operations.

Known Issues:
* Some test cases fail because of bug inside them not the API. Test case methods are async and they are sharing same database context thus failing.
* Github action would also show an error after running the test cases on push and pull. 
