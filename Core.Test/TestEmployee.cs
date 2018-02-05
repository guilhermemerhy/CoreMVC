using Core.Domain.Command.Handlers;
using Core.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Test
{
    [TestClass]
    public class TestEmployee
    {
        [TestMethod]
        public void EmployeeCreateIsValid()
        {
            var command = new EmployeeCreateOrUpdateCommand
            {
                Birth = DateTime.Now,
                Email = null,
                Genre = true,
                Id = null,
                Name = "Testando mais",
                Role = 1,
                Dependent = null               
            };
            

            Assert.AreEqual(true, command.IsValid());
        }
    }
}
