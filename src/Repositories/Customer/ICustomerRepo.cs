using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chinook.Src.Model;

namespace Chinook.Src.Repositories.CustomerRepo
{
    internal interface ICustomer : ICrudService<Customer, int>
    {
        
        Customer GetByName(string name);

    }
}