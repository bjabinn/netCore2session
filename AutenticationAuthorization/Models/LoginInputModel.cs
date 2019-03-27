using System;

namespace AutenticationAuthorization.Models
{
    public class LoginInputModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RequestPath { get; set; }
    }
}