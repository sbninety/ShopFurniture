﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int IsAdmin { get; set; }
        public int IsCustomer { get; set; }
    }
}
