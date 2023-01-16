using automationPractice.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automationPractice.Tests.CreateAccount
{
    internal class SignUpRepository
    {
        public string[] InvalidEmails { get; set; }
        public string[] InvalidPasswords { get; set; }
        public User ValidUser { get; set; }
    }
}
