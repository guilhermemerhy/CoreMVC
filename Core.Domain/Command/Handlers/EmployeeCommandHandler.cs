using Core.Domain.Entities;
using Core.Domain.Repository;
using Core.Domain.UwO;
using System.Linq;

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
  

        public void Handle(EmployeeCreateOrUpdateCommand model)
        {
            if (!model.IsValid())
                return;

            if (!string.IsNullOrWhiteSpace(model.Email))
                if (_employeeRepository.GetByEmail(model.Email, model.Id))
                {
                    model.Failures.Add("Já existe um Email cadastrado");
                    return;
                }



            if (model.Id == null)
            {
                var employee = new Employee(model.Name, model.Email, model.Genre, model.Birth, model.Role);
                _employeeRepository.Add(employee);

                foreach (var item in model.Dependent)
                {
                    employee.AddDependent(new Dependent(item.Name, employee.Id));
                }

                _employeeRepository.Add(employee);
            }
            else
            {
                var employee = _employeeRepository.GetById(model.Id);

            

                employee.Alterar(model.Name, model.Email, model.Genre, model.Birth, model.Role);

                _employeeRepository.Update(employee);

                if (model.Dependent.Any())
                {
                    foreach (var item in employee.Dependent)
                        _dependentRepository.Remove(item);

                    foreach (var item in model.Dependent)
                    {
                        _dependentRepository.Add(new Dependent(item.Name, employee.Id));
                    }
                }
            }

            _unitOfWork.Commit();

            return;

        }


        public void Handle(EmployeeRemoveCommand command)
        {
            var employee = _employeeRepository.GetById(command.Id);

            if (employee.Dependent.Any())
                foreach (var item in employee.Dependent)
                    _dependentRepository.Remove(item);

            _employeeRepository.Remove(employee);

            _unitOfWork.Commit();
        }

    }
}
