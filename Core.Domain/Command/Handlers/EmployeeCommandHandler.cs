using Core.Domain.Entities;
using Core.Domain.Repository;
using Core.Domain.UwO;
using Core.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Domain.Command.Handlers
{
    public class EmployeeCommandHandler
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDependentRepository _dependentRepository;
        private readonly IUnitOfWork _unitOfWork;


        public EmployeeCommandHandler(IEmployeeRepository employeeRepository, IDependentRepository dependentRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _dependentRepository = dependentRepository;
        }


        public async Task Handle(EmployeeCreateOrUpdateCommand model)
        {
            var isValid = await model.IsValid();

            if (!isValid)
                return;

            if (!string.IsNullOrWhiteSpace(model.Email))
                if (await _employeeRepository.GetByEmail(model.Email, model.Id))
                {
                    model.Failures.Add("Já existe um Email cadastrado");
                    return;
                }



            var email = new Email(model.Email);

            if (model.Id == null)
            {
                #region Insert

                var employee = new Employee(Guid.NewGuid(), model.Name, email, model.Genre, model.Birth, model.Role);
                
                foreach (var item in model.Dependent)
                {
                    employee.AddDependent(new Dependent(item.Name, employee.Id));
                }

                await _employeeRepository.Add(employee);

                #endregion
            }
            else
            {
                #region Update                
                var employee = new Employee((Guid)model.Id, model.Name, email, model.Genre, model.Birth, model.Role);

                _employeeRepository.Update(employee);

                var dependents = await _dependentRepository.GetAllByEmployee((Guid)model.Id);

                if(dependents.Any())
                    _dependentRepository.RemoveAll(dependents);

                if (model.Dependent.Any())
                    foreach (var item in model.Dependent)
                      await _dependentRepository.Add(new Dependent(item.Name, employee.Id));

                #endregion
            }


           await _unitOfWork.Commit();

        }


        public async Task Handle(EmployeeRemoveCommand command)
        {
            var employee = await _employeeRepository.GetById(command.Id);

            if (employee.Dependent.Any())
                _dependentRepository.RemoveAll(employee.Dependent);

            _employeeRepository.Remove(employee);

            await _unitOfWork.Commit();
        }

    }
}
