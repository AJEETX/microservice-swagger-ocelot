using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Api.Model;
using Identity.Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocelot.JwtAuthorize;

namespace Identity.Api.Controllers
{
    [Route("identity/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        readonly ILogger<CustomersController> _logger;
        readonly ITokenBuilder _tokenBuilder;
        ICustomerService _customerService;
        IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenBuilder"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="customerService"></param>
        public CustomersController(ITokenBuilder tokenBuilder, ILogger<CustomersController> logger, ICustomerService customerService, IMapper mapper)
        {
            _logger = logger;
            _tokenBuilder = tokenBuilder;
            _customerService = customerService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>returns all customers</returns>
        [HttpGet(Name = "get-customers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> Get()
        {
            string message = string.Empty;
            try
            {
                _logger.LogInformation($"getting customers ...");
                var customers = await _customerService.GetCustomers();
                if (customers == null) NotFound();
                var customersModel = _mapper.Map<IEnumerable<CustomerModel>>(customers);
                return Ok(new { Customers = customersModel });
            }
            catch (Exception)
            {
                message = $"Errored .... shout";
                _logger.LogInformation(message);
                return StatusCode(500);
            }

        }
        /// <summary>
        /// Customer login
        /// </summary>
        /// <param name="loginModel">Model</param>
        /// <returns>login customer</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginModel loginModel)
        {
            // No model state validation code here in dotnet ore 2.1, hooray!
            string message = string.Empty;
            try
            {
                var login = _mapper.Map<Login>(loginModel);
                var userIsValid = await _customerService.Login(login);
                message = $"{loginModel.UserName} login！";
                _logger.LogInformation(message);
                if (userIsValid)
                {
                    var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(ClaimTypes.Role, "admin"),

                };
                    var token = _tokenBuilder.BuildJwtToken(claims);
                    message = $"{loginModel.UserName} login success，and generate token return";
                    _logger.LogInformation(message);
                    return Ok(token);
                }
                else
                {
                    message = $"{loginModel.UserName} login fails";
                    _logger.LogInformation(message);
                    return BadRequest(message);
                }
            }
            catch (AggregateException)
            {
                message = $"Errored .... shout";
                _logger.LogInformation(message);
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Customer register 
        /// </summary>
        /// <param name="customerModel">Model</param>
        /// <returns>return regiter message</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public async Task<ActionResult> Register([FromBody]CustomerModel customerModel)
        {
            // No model state validation code here in dotnet core 2.1, hooray!
            try
            {
                _logger.LogInformation($"{customerModel.UserName} register！");
                var customer = _mapper.Map<Customer>(customerModel);

                var createdCustomer = await _customerService.AddCustomer(customer);
                if (createdCustomer != null)
                {
                    _logger.LogInformation($"{customer.UserName} register success");
                    return CreatedAtAction("login", new { id = createdCustomer.ID }, createdCustomer);
                }
                else
                {
                    string message = $"{customer.UserName} register failes";
                    _logger.LogInformation(message);
                    return BadRequest(message);
                }
            }
            catch (AggregateException)
            {
                string message = $" Error .....shout";
                _logger.LogInformation(message);
                return StatusCode(500);
            }

        }
        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="userName"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{userName}")]
        public async Task<ActionResult> Delete(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return BadRequest();

            try
            {
                var customer =await _customerService.DeleteCustomer(userName);
                if (customer == null) return NotFound();

                _logger.LogInformation($"deleted customer with userName : {userName}！");

                return Ok($"{userName} deleted !!!");
            }
            catch (AggregateException)
            {
                string message = $" Error .....shout";
                _logger.LogInformation(message);
                return StatusCode(500);
            }
        }
    }
}
