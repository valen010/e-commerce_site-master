﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceWebSite.Classes
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string SecurityQuestion { get; set; }
        public string SqAnswer { get; set; }

    }
}