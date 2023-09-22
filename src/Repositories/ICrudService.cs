using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Src.Repositories
{
    internal interface ICrudService <T, TKey>
    {
        ICollection<T> GetAll();
        T GetById(TKey id);
        T Save(T entity);
        T Update(T entity);
        void Delete(TKey id);
    }
}