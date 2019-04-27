using Ocelot.JwtAuthorize;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Model
{
    /// <summary>
    /// LoginModel
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// UserName
        /// </summary>        
        public string UserName { get; set; }
    }

    public class Login
    {
        /// <summary>
        /// UserName
        /// </summary>      
        [Required]
        public string UserName { get; set; }
    }
    /// <summary>
    /// CustomerModel
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// UserName
        /// </summary>
        [Required]
        public string UserName { get; set; }
    }

    public class Customer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        [Required]
        public string UserName { get; set; }

    }
    /// <summary>
    /// Response
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// result
        /// </summary>
        public bool Result { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public TokenBuilder.Token Data { get; set; }
    }
}
