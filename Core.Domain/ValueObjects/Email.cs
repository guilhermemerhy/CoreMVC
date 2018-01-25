using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ValueObjects
{
    public class Email 
    {
        protected Email() { }
        public Email(string address)
        {
            Address = address;
        }

        public string Address { get; private set; }

    }
}
