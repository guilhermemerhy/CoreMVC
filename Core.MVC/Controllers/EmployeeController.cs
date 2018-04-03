using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.Command;
using Core.Domain.Command.Handlers;
using Core.Domain.Repository;
using Core.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Core.MVC.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly EmployeeCommandHandler _handler;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;

        public EmployeeController(EmployeeCommandHandler handler, IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IMapper mapper) 
        {
            _handler = handler;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index() => View(_mapper.Map<IEnumerable<EmployeeViewModel>>(await _employeeRepository.GetAll()));

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<string> CreateOrUpdate([FromBody]EmployeeCreateOrUpdateCommand model)
        {

           await _handler.Handle(model);

            return JsonConvert.SerializeObject(new
            {
                sucess = !model.Failures.Any(),
                erros = model.Failures
            });

        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Role = new SelectList(await _roleRepository.GetAll(), "Id", "Name");
            return View("CreateOrUpdate");
        }


        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _employeeRepository.GetById(id);
            if (employee == null)
                return NotFound();


            ViewBag.Role = new SelectList(await _roleRepository.GetAll(), "Id", "Name", employee.RoleId);

            return View("CreateOrUpdate", _mapper.Map<EmployeeViewModel>(employee));
        }


        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _employeeRepository.GetById(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirmed(EmployeeRemoveCommand command)
        {
            await _handler.Handle(command);
            return RedirectToAction(nameof(Index));
        }

    }
}
