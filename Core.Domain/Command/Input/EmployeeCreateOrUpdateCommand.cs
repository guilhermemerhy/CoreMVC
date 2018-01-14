using Core.Domain.Entities;
using Core.Domain.Validator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Command.Handlers
{
   public class EmployeeCreateOrUpdateCommand
    {
        private readonly IList<string> _items;

        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Genre { get; set; }

        public DateTime? Birth { get; set; }

        public Int16 Role { get; set; }

        public ICollection<Dependent> Dependent { get; set; }

        public ICollection<string> Failures => _items;

        public EmployeeCreateOrUpdateCommand()
        {
            _items = new List<string>();
        }

        public bool IsValid()
        {
            var result = new EmployeeCreateOrUpdateCommandValidator().Validate(this);

            foreach (var error in result.Errors)
                _items.Add(error.ErrorMessage);

            return result.IsValid;
        }
    }
}

