﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string userId { get; set; }
        [ForeignKey(nameof(userId))]
        public ApplicationUser user { get; set; }


        public List<OrderItem> OrderItems { get; set; }
    }
}
