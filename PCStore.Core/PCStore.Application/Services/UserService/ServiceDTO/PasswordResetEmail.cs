﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.UserService.ServiceDTO
{
    public class PasswordResetEmail
    {
        public string Url = null!;
        public required string Email { get; set; }
    }
}
