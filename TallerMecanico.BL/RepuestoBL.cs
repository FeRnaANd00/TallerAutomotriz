using System.Collections.Generic;
using TallerMecanico.EN;
using TallerMecanico.DAL;

namespace TallerMecanico.BL
{
    public static class RepuestoBL
    {
        public static int AgregarRepuesto(Repuesto repuesto)
        {
            return RepuestoDAL.AgregarRepuesto(repuesto);
        }

        public static int ModificarRepuesto(Repuesto repuesto)
        {
            return RepuestoDAL.ModificarRepuesto(repuesto);
        }

        public static int EliminarRepuesto(int id)
        {
            return RepuestoDAL.EliminarRepuesto(id);
        }

        public static List<Repuesto> MostrarRepuestos()
        {
            return RepuestoDAL.MostrarRepuestos();
        }

        public static Repuesto ObtenerRepuestoPorId(int id)
        {
            return RepuestoDAL.ObtenerRepuestoPorId(id);
        }

        public static List<Repuesto> BuscarRepuesto(string texto)
        {
            return RepuestoDAL.BuscarRepuesto(texto);
        }
    }
}