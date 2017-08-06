using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Dados
{
    public interface Iconnection:IDisposable
    {
        NpgsqlConnection Open();
        NpgsqlConnection Get();
        void Close();
    }
}
