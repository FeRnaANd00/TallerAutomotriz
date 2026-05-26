using System.Collections.Generic;
using TallerMecanico.EN;
using TallerMecanico.DAL;

namespace TallerMecanico.BL
{
    public static class ClienteBL
    {
        public static int AgregarCliente(Cliente cliente)
        {
            return ClienteDAL.AgregarCliente(cliente);
        }

        public static int ModificarCliente(Cliente cliente)
        {
            return ClienteDAL.ModificarCliente(cliente);
        }

        public static int EliminarCliente(int id)
        {
            return ClienteDAL.EliminarCliente(id);
        }

        public static List<Cliente> MostrarClientes()
        {
            return ClienteDAL.MostrarClientes();
        }

        public static Cliente ObtenerClientePorId(int id)
        {
            return ClienteDAL.ObtenerClientePorId(id);
        }

        public static List<Cliente> BuscarCliente(string texto)
        {
            return ClienteDAL.BuscarCliente(texto);
        }
    }
}