using System;

namespace Core.Domain.Entities
{
    public class Dependent
    {
        public Int16 Id { get; private set; }

        public string Name { get; private set; }

        public Guid EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }



        protected Dependent() { }

        public Dependent(string name, Guid employeeId)
        {
            Name = name;
            EmployeeId = employeeId;
        }

    }
}
