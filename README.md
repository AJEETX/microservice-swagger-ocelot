# aspnet.core microservices ![GitHub release](https://img.shields.io/github/release/ajeetx/microservice-swagger-ocelot.svg?style=for-the-badge) ![Maintenance](https://img.shields.io/maintenance/yes/2019.svg?style=for-the-badge)

### dotnet-core2.1-microservices-swagger

[![.Net Framework](https://img.shields.io/badge/DotNet-Core_2.1-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1)  | ![GitHub language count](https://img.shields.io/github/languages/count/ajeetx/microservice-swagger-ocelot.svg) | ![GitHub top language](https://img.shields.io/github/languages/top/ajeetx/microservice-swagger-ocelot.svg) |![GitHub repo size in bytes](https://img.shields.io/github/repo-size/ajeetx/microservice-swagger-ocelot.svg) 
| --- | ---          | ---        | ---      | 

---------------------------------------

 #### Please see the demo below

 <img width="100%" src="demo.gif" />


## Repository codebase
 
The repository consists of projects as below:


| # |Project Name |  Project Type | Project detail | comments | 
| ---| ---  | ---           | ---       |    --- | 
| 1 | TicketGateway.Api | gateway api | Asp.Net Core2 WebApi  |  gateway to the microservices api |
| 2 | Identity.Api | microservice | Asp.Net Core2 WebApi  |  create, read, delete customer | 
| 3 | Identity.Api.Test |Unit Test | Test Identity.Api |  unit test for Identity.Api project | 
| 4 | HelpDesk.Api | microservice | Asp.Net Core2 WebApi  |  create, read, delete helpdesk tickets |
| 5 | HelpDesk.Api.Test | Unit Test | Test  HelpDesk.Api |  unit test for HelpDesk.Api project |  


### Summary

The overall objective of the applications :
```
>	A customer can Register
>	Then the customer can Login 
>	Authentication token is used
>	Once logged-in, user can do "CRUD" operation on Helpdesk tickets
```

### Setup detail
 
> Please download / clone the repository.

> Open the solution file named `TicketGateway.Api.sln` through VS2017.

> Within VS2017, in solution exploere right click in the `solution file` , select properties. selected startup project as "Multiple startup Projects" and choose action "Start" for each `non-test` projects from the dropdown.

> Run the solution by pressing they key `F5`
 
### To-do

> Run in docker

### Support or Contact

Having any trouble? Please read out this [documentation](https://github.com/AJEETX/microservice-swagger-ocelot/blob/master/README.md) or [contact](mailto:ajeetx@email.com) and to sort it out.

```
keep coding ;)
```

<a href="https://info.flagcounter.com/kaHw"><img src="https://s01.flagcounter.com/count2/kaHw/bg_FFFFFF/txt_000000/border_CCCCCC/columns_8/maxflags_12/viewers_0/labels_0/pageviews_0/flags_0/percent_0/" alt="Flag Counter" border="0"></a>