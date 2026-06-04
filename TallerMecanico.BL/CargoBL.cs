using System.Collections.Generic;
using TallerMecanico.EN;
using TallerMecanico.DAL;

namespace TallerMecanico.BL
{
    public static class CargoBL
    {
        public static int AgregarCargo(Cargo cargo)
        {
            return CargoDAL.AgregarCargo(cargo);
        }

        public static int ModificarCargo(Cargo cargo)
        {
            return CargoDAL.ModificarCargo(cargo);
        }

        public static int EliminarCargo(int id)
        {
            return CargoDAL.EliminarCargo(id);
        }

        public static List<Cargo> MostrarCargos()
        {
            return CargoDAL.MostrarCargos();
        }

        public static Cargo ObtenerCargoPorId(int id)
        {
            return CargoDAL.ObtenerCargoPorId(id);
        }

        public static List<Cargo> BuscarCargo(string texto)
        {
            return CargoDAL.BuscarCargo(texto);
        }
    }
}