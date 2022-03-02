# user-points

1. Add points to user
2. Remove points from user
3. Get points by user

Method | End Point | Description | Input
------ | --------- | ----------- | -------
Put   | /AddPoints | Add the points to an user base on the user ID |{"userId": "text","points": number}
Put   | /RemovePoints | Substract the amount of points that are sent as a parameter. |{"userId": "text","points": number}
Get    | /PointsByUser| Returns the numer of ponts of a user | ?userId=userId

## Stack
Writen in C#
Using Mongo DB has Data Base
all dockenize

## Proyect structure
+-- src
| +-- UserPoints                core librery with interfaces and models
| +-- UserPoints.API            project with the enpoints 
| +-- UserPoints.DataAccess     contains the data access code
+-- test
| +-- UserPoints.API.UnitTest   unit tests

## How to run

Run the command:
'docker-compose -f docker-compose.yml up' in the root of the proyect this will build and start the service