####
This example implements API versioning. Project created in Visual Studio Code with .NET Core 8.

####
1.- dotnet new webapi -n API_StringList_Versioning --framework net8.0

####
A.- In Query string.
####
B.- Inside HEADERS key-value pair.
####
C.- Inside URL.

#####
How to test.

#####
1. http://localhost:5000/api/stringlist?api-version=1.0

#####
2. http://localhost:5000/api/stringlist
#####
In HEADERS add
#####
Accept : application/json;ver=2.0

##### 
3.- http://localhost:5000/api/v3/stringlist
