using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public interface IDao<T>:IDisposable
        where T: class, new()
    {
        T Insert(T model);
        void Update(T model);
        bool Delete(T model);
        T FindOrDefault(params object[] keys);
        IEnumerable<T> All();
    }
}
