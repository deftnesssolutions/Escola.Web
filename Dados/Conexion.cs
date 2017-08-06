using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace Dados
{
    public class Conexion : Iconnection
    {
        private NpgsqlConnection _conexionString;

        public Conexion()
        {
            _conexionString = new NpgsqlConnection("User Id=postgres;Password=postgres;Host=localhost;Database=escola");
        }

        public NpgsqlConnection Open()
        {
            if (_conexionString.State == ConnectionState.Closed)
            {
                _conexionString.Open();
            }
            return _conexionString;
        }

        public NpgsqlConnection Get()
        {
            return this.Open();
        }

        public void Close()
        {
            if (_conexionString.State == ConnectionState.Open)
            {
                _conexionString.Close();
            }
        }

        public void Dispose()
        {
            this.Close();
            GC.SuppressFinalize(this);
        }
        
    }
}
