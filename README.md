# Thesaurus
Thesaurus ASP.NET Core (3.1) Web App

Web: http://webthesaurus-dev.us-east-1.elasticbeanstalk.com/

Description:
Monolith solution which consists of eight projects:
- Web (Responsability: handling HTTP requests using controllers and returning Razor views with data)
- Service (Responsability: transforming model's data into data transfer objects and vice versa, can also handle validations)
- Business (Responsability:  business domain logic and interaction with data access layer)
- DataAccess (Responsability:  managing database context and domain entities)
- Infrastructure (Responsability:  handling/providing information to Web project related to depencency injection management)
- Common (Responsability: manage common data for multiple projects - ex. data transfer objects, interfaces) 
- Logger (Responsability: application's main logger configuration)
- Test (Responsability: running application tests)

Main project depencencies:
DataAccess < Business < Service < Web

Each project can have their own depencency to Common project, but Common project can not have any depencencies to any other project.

Infrastructure project can have depencencies to all other projects. It serves Web project to gether dependency injection information.
