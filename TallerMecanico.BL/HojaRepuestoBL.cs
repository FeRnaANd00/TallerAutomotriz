using System.Collections.Generic;
using TallerMecanico.EN;
using TallerMecanico.DAL;

namespace TallerMecanico.BL
{
    public static class HojaRepuestoBL
    {
        public static int AgregarRepuestoAHoja(HojaRepuesto hr)
        {
            return HojaRepuestoDAL.AgregarRepuestoAHoja(hr);
        }

        public static int EliminarRepuestoDeHoja(int idHojaRepuesto)
        {
            return HojaRepuestoDAL.EliminarRepuestoDeHoja(idHojaRepuesto);
        }

        public static List<HojaRepuesto> ObtenerRepuestosPorHoja(int idHoja)
        {
            return HojaRepuestoDAL.ObtenerRepuestosPorHoja(idHoja);
        }
    }
}