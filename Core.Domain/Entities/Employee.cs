using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Core.Domain.Entities
{
    public class Employee
    {
        protected Employee() { }


        public Employee(string name, string email, bool genre, DateTime? birth, Int16 role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Genre = genre;
            Birth = birth;
            RoleId = role;

            Dependent = new List<Dependent>();

        }



        public Guid Id { get; private set; }

        [DisplayName("Nome")]
        public string Name { get; private set; }

        public string Email { get; private set; }

        [DisplayName("Sexo")]
        public bool Genre { get; private set; }

        [DisplayName("Data de Nascimento")]
        public DateTime? Birth { get; private set; }


        public Int16 RoleId { get; private set; }

        [DisplayName("Cargo")]
        public virtual Role Role { get; private set; }

        [DisplayName("Dependente")]
        public IList<Dependent> Dependent { get; private set; }



        public void Alterar(string name, string email, bool genre, DateTime? birth, Int16 role)
        {
            Name = name;
            Email = email;
            Genre = genre;
            Birth = birth;
            RoleId = role;
        }


        public void AddDependent(Dependent item)
        {
            Dependent.Add(item);
        }


    }
}
