# NetCoreSampleArchitecture

A sample architecture to run a .Net Core application as a service which includes api methods. This solution includes base architecture to
run a complex service application.

It includes the packages and libraries below

- .Net Core 3.1.3
- Entity Framework Core 3.1.3
- Serilog 3.2.0
- Automapper
- Swagger
- SignalR
- OpenXml
- Jwt Authentication

N-Tier architecture and repository unit of work pattern is used in this solution. .Net Core dependency injection 
is used without using any third party dependency injection library. It can be runned as a service by installing
install.bat file. Data migration is available thanks to entity framework core. Sql Server must be installed in your computer to run
this application or you can change database settings to work with the other databases. It has transaction management, publishing events, 
model validation, paging, password hashing, memory cache management properties and base codes. IHostedService is used to run background 
jobs and OpenXml is used to exporting excel files. It also includes features to log entity audits. SignalR base codes are available for 
real time communication with client side. User, Role, Permission and some other base entity, mapping files and api methods for crud 
operations are written.  
