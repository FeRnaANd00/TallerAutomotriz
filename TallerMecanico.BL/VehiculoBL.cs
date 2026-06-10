using System.Collections.Generic;
using TallerMecanico.DAL;
using TallerMecanico.EN;

namespace TallerMecanico.BL
{
    public static class VehiculoBL
    {
        public static int AgregarVehiculo(Vehiculo vehiculo)
        {
            return VehiculoDAL.AgregarVehiculo(vehiculo);
        }

        public static int ModificarVehiculo(Vehiculo vehiculo)
        {
            return VehiculoDAL.ModificarVehiculo(vehiculo);
        }

        public static int EliminarVehiculo(int id)
        {
            return VehiculoDAL.EliminarVehiculo(id);
        }

        public static List<Vehiculo> MostrarVehiculos()
        {
            return VehiculoDAL.MostrarVehiculos();
        }

        public static Vehiculo ObtenerVehiculoPorId(int id)
        {
            return VehiculoDAL.ObtenerVehiculoPorId(id);
        }

        public static List<Vehiculo> BuscarVehiculo(string texto)
        {
            return VehiculoDAL.BuscarVehiculo(texto);
        }
    }
}