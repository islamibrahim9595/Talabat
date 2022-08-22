using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications
{
    public class EmployeeWithDepartmentSpecification : BaseSpecification<Employee>
    {
        public EmployeeWithDepartmentSpecification(string sort)
        {
            AddIncludes(E => E.Department);

            AddOrderBy(e => e.Name);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "Asc":
                        AddOrderBy(e => e.Department.Id);
                        break;
                    case "Desc":
                        AddOrderByDescending(e => e.Department.Id);
                        break;
                    default:
                        AddOrderBy(e => e.Name);
                        break;
                }
            }
        }

        public EmployeeWithDepartmentSpecification(int id) : base(E => E.Id == id)
        {
            AddIncludes(E => E.Department);

        }
    }
}
