using System.Collections.Generic;
using TallerMecanico.DAL;
using TallerMecanico.EN;

namespace TallerMecanico.BL
{
    public class MecanicoBL
    {
        // AGREGAR
        public static int AgregarMecanico(Mecanico mecanico)
        {
            return MecanicoDAL.AgregarMecanico(mecanico);
        }

        // MOSTRAR
        public static List<Mecanico> MostrarMecanicos()
        {
            return MecanicoDAL.MostrarMecanicos();
        }

        // MODIFICAR
        public static int ModificarMecanico(Mecanico mecanico)
        {
            return MecanicoDAL.ModificarMecanico(mecanico);
        }

        // ELIMINAR
        public static int EliminarMecanico(int id)
        {
            return MecanicoDAL.EliminarMecanico(id);
        }

        // OBTENER POR ID
        public static Mecanico ObtenerMecanicoPorId(int id)
        {
            return MecanicoDAL.ObtenerMecanicoPorId(id);
        }

        // BUSCAR
        public static List<Mecanico> BuscarMecanico(string texto)
        {
            return MecanicoDAL.BuscarMecanico(texto);
        }
    }
}
