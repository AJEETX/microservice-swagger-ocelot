using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Api.Model
{
    /// <summary>
    /// Model
    /// </summary>
    public class TicketModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Describe
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// IsSure
        /// </summary>
        public bool Active { get; set; }
    }
    /// <summary>
    /// Ticket
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Describe
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// IsSure
        /// </summary>
        public bool Active { get; set; }
    }
}
