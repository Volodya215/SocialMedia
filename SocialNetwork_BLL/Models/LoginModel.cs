﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    /// <summary>
    /// Contains data for login: username and password
    /// </summary>
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
