using System.Collections.Generic;
using TallerMecanico.EN;
using TallerMecanico.DAL;

namespace TallerMecanico.BL
{
    public static class ServicioBL
    {
        public static int AgregarServicio(Servicio servicio)
        {
            return ServicioDAL.AgregarServicio(servicio);
        }

        public static int ModificarServicio(Servicio servicio)
        {
            return ServicioDAL.ModificarServicio(servicio);
        }

        public static int EliminarServicio(int id)
        {
            return ServicioDAL.EliminarServicio(id);
        }

        public static List<Servicio> MostrarServicios()
        {
            return ServicioDAL.MostrarServicios();
        }

        public static Servicio ObtenerServicioPorId(int id)
        {
            return ServicioDAL.ObtenerServicioPorId(id);
        }

        public static List<Servicio> BuscarServicio(string texto)
        {
            return ServicioDAL.BuscarServicio(texto);
        }
    }
}