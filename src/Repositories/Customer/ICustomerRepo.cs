using Chinook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repositories.Customers
{
    internal interface ICustomer : ICrudService<Customer, int>
    {
        
        Customers GetByName(string name);
        void AddSubject(int subjectId, int professorId);
    }
}