﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class UserDto
    {
        [Key]
        public string Id { get; set; } 
        public string Email { get; set; } 
    }

}
