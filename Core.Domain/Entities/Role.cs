using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Role
    {
        public Int16 Id { get; private set; }

        public string Name { get; private set; }

        public virtual ICollection<Employee> Employees { get; set; }


        protected Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}
