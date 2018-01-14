using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Command
{
    public class EmployeeRemoveCommand
    {
        public Guid Id { get; set; }
    }
}
