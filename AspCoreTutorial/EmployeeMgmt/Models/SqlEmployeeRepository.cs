using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.Models
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext _context;

        public SqlEmployeeRepository(AppDBContext context)
        {
            _context = context;
        }

        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee emp = _context.Employees.Find(id);
            if(emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee Update(Employee changedEmp)
        {
            var emp = _context.Employees.Attach(changedEmp);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return changedEmp;
        }
    }
}
