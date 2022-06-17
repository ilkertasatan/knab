## Knab Engineer Recruitment Test

### What the API does
This assignment has been developed for Knab. It consist of one api in order to convert from given crypto currency to the following;

* USD
* EUR
* BRL
* GBP
* AUD

### Technologies
For this assignment, the tools is being used;

* .NET 6
* Web Api
* Api Versioning
* Polly
* PowerShell script
* Newtonsoft
* Moq
* FluentAssertion

### Architecture
Clean Architecture which has been introduced by Robert C. Martin (Uncle Bob) is being used in the project and consisting of the parts below;

* Api
* Application
* Infrastructure
* Domain

[Click here for more information about clean architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

#### Api
This layer is entrance the application and is responsible to send the request to application layer by converting the request as application input and getting application output and convert it to response and show it.

#### Application
This layer is responsible to execute use-cases (application logic) and make an output.

#### Infrastructure
This layer is responsible to communicate external services such as external apis or database.

#### Domain
This layer is the heart of our application and doesn't hold any reference from the other layers. It is responsible to execute domain logic.

### How to run
If you would like to run the application locally, you would need an ide in order to open source codes. After opening, the project can simply be run. As a second option is Docker, the project has Docker support and it can easily be run inside docker container by running the command below which has in PowerShell script on the root folder;

`./build-all.ps1`

In case you might get a security error when running the PowerShell script, you should run the following;

`Set-ExecutionPolicy RemoteSigned`

### How to test
If you start project locally, there is Swagger documentation that you can test the application on its UI, but Swagger is only for development environment. If you start project inside docker container, you won't have swagger since it's being started as production configuration. For this scenario; you can use any rest-client tool to test.

## Conclusion
I'd like to thank you Knab to send this assignment to me and consider me as a candidate. I hope that you like the solution while reviewing.
