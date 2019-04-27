# aspnet.core microservices ![GitHub release](https://img.shields.io/github/release/ajeetx/microservice-swagger-ocelot.svg?style=for-the-badge) ![Maintenance](https://img.shields.io/maintenance/yes/2019.svg?style=for-the-badge)

### dotnet2-microservices-docker-swagger

[![.Net Framework](https://img.shields.io/badge/DotNet-2.1_Framework-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1)  | ![GitHub language count](https://img.shields.io/github/languages/count/ajeetx/microservice-swagger-ocelot.svg) | ![GitHub top language](https://img.shields.io/github/languages/top/ajeetx/microservice-swagger-ocelot.svg) |![GitHub repo size in bytes](https://img.shields.io/github/repo-size/ajeetx/microservice-swagger-ocelot.svg) | [![.Net Framework](https://img.shields.io/badge/docker-install-blue.svg)](https://www.docker.com/get-started)
| --- | ---          | ---        | ---      | --- |

---------------------------------------

 #### Please see the demo below

 <img width="100%" src="Record-Point-Demo.gif" />


## Repository codebase
 
The repository consists of projects as below:


| # |Project Name | Project detail | comments | 
| ---| ---  | ---           | ---          | 
| 1 | TicketGateway.Api | Asp.Net Core2 WebApi as gateway  |  gateway to the microservices api |
| 2 | Identity.Api | Asp.Net Core2 WebApi as microservice  |  create, read, delete customer | 
| 3 | Identity.Api.Test | Unit Test for Identity.Api |  unit test for Identity.Api project | 
| 4 | HelpDesk.Api | Asp.Net Core2 WebApi as microservice  |  create, read, delete helpdesk tickets |
| 5 | HelpDesk.Api.Test | Unit Test for HelpDesk.Api |  unit test for HelpDesk.Api project |  


### Summary

The overall objective of the applications :
```
>	A customer can Register
>	Then the customer can Login 
>	Authentication token is used
>	Once logged-in, user can do "CRUD" operation on Helpdesk tickets
```

### Setup detail
 
> If docker is not installed please install `docker` on your machine.

> Please download / clone the repository.

> Open the solution file named `TicketGateway.Api.sln` through VS2017.

> Within VS2017, in solution exploere right click in the `solution file` , select properties. selected startup project as "Multiple startup Projects" and choose action "Start" for each project from the dropdown.

> Run the solution by pressing they key `F5`
 
### Support or Contact

Having any trouble? Please read out this [documentation](https://github.com/AJEETX/microservice-swagger-ocelot/blob/master/README.md) or [contact](mailto:ajeetx@email.com) and to sort it out.

```
keep coding ;)
```

<a href="https://info.flagcounter.com/VOMj">
<img src="https://s01.flagcounter.com/count/VOMj/bg_FFFFFF/txt_000000/border_CCCCCC/columns_8/maxflags_12/viewers_0/labels_1/pageviews_1/flags_0/percent_0/" alt="Flag Counter" border="0">
</a>