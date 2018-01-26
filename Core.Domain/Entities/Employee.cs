using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Employee
    {
        protected Employee() { }


        public Employee(Guid id, string name, Email email, bool genre, DateTime? birth, Int16 role)
        {
            Id = id;
            Name = name;
            Email = email;
            Genre = genre;
            Birth = birth;
            RoleId = role;

            Dependent = new List<Dependent>();

        }



        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Email Email { get; private set; }

        public bool Genre { get; private set; }

        public DateTime? Birth { get; private set; }

        public Int16 RoleId { get; private set; }

        public virtual Role Role { get; private set; }

        public IList<Dependent> Dependent { get; private set; }


        public void AddDependent(Dependent item)
        {
            Dependent.Add(item);
        }


    }
}
