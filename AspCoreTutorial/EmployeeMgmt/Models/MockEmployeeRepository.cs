using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            {
                new Employee{ Id = 1, Name = "John", Department = Dept.HR, Email = "John@gmail.com" },
                new Employee{ Id = 2, Name = "Vishal", Department = Dept.IT, Email = "vishal@gmail.com" },
                new Employee{ Id = 3, Name = "Raj", Department = Dept.IT, Email = "Raj@gmail.com" },
                new Employee{ Id = 4, Name = "Peter", Department = Dept.Payroll, Email = "peter@gmail.com" },
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id =  _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee emp = _employeeList.FirstOrDefault(e => e.Id == id);
            if(emp != null)
            {
                _employeeList.Remove(emp);
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return this._employeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee changedEmp)
        {
            Employee emp = _employeeList.FirstOrDefault(e => e.Id == changedEmp.Id);
            if(emp != null)
            {
                changedEmp.Name = emp.Name;
                changedEmp.Email = emp.Email;
                changedEmp.Department = emp.Department;
            }
            return emp;
        }
    }
}
