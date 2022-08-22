using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
