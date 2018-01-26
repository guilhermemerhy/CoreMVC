using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Core.MVC.Models
{
    public class EmployeeViewModel
    {
        public Guid? Id { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        public Email Email { get; set; }

        [DisplayName("Sexo")]
        public bool Genre { get; set; }

        [DisplayName("Data de Nascimento")]
        public DateTime? Birth { get; set; }

        [DisplayName("Cargo")]
        public Role Role { get; set; }

        [DisplayName("Dependente")]
        public ICollection<Dependent> Dependent { get; set; }
    }
}
