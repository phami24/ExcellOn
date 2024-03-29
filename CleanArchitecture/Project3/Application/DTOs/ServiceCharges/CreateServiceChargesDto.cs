﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ServiceCharges
{
    public class CreateServiceChargesDto
    {
        [Required]

        public string ServiceChargesName { get; set; }
        [Required]

        public double Price { get; set; }
        [Required]

        public string ServiceChargesDescription { get; set; }
        [Required]

        public int ServiceId { get; set; }
    }
}
