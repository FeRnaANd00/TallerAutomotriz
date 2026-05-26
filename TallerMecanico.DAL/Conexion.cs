using System;
using System.Collections.Generic;
using System.Text;

using MySqlConnector;

namespace TallerMecanico.DAL
{
    public class Conexion
    {
        private static string cad = @"datasource=localhost;port=3306;username=root;password=2365;database=taller_mecanico";

        public static MySqlConnection conectar()
        {
            return new MySqlConnection(cad);
        }
    }
}
