using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Command;
using Core.Domain.Command.Handlers;
using Core.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.MVC.Controllers
{
    public class EmployeeController : Controller
    {
     
        private readonly EmployeeCommandHandler _handler;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;

        public EmployeeController(EmployeeCommandHandler handler, IEmployeeRepository employeeRepository, IRoleRepository roleRepository) 
        {
            _handler = handler;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }


        public IActionResult Index() =>
            View(_employeeRepository.GetAll());


        [HttpPost]
        public string CreateOrUpdate([FromBody]EmployeeCreateOrUpdateCommand model)
        {

            _handler.Handle(model);

            return JsonConvert.SerializeObject(new
            {
                sucess = model.Failures.Any(),
                erros = model.Failures
            });

        }

        public IActionResult Create()
        {
            ViewBag.Role = new SelectList(_roleRepository.GetAll(), "Id", "Name");
            return View("CreateOrUpdate");
        }


        public IActionResult Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var employee = _employeeRepository.GetById(id);
            if (employee == null)
                return NotFound();


            ViewBag.Role = new SelectList(_roleRepository.GetAll(), "Id", "Name", employee.RoleId);

            return View("CreateOrUpdate", employee);
        }


        public IActionResult Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var employee = _employeeRepository.GetById(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(EmployeeRemoveCommand command)
        {
            _handler.Handle(command);
            return RedirectToAction(nameof(Index));
        }

    }
}