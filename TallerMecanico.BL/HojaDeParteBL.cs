using System.Collections.Generic;
using TallerMecanico.EN;
using TallerMecanico.DAL;

namespace TallerMecanico.BL
{
    public static class HojaDeParteBL
    {
        public static int AgregarHojaDeParte(HojaDeParte hoja)
        {
            return HojaDeParteDAL.AgregarHojaDeParte(hoja);
        }

        public static int ModificarHojaDeParte(HojaDeParte hoja)
        {
            return HojaDeParteDAL.ModificarHojaDeParte(hoja);
        }

        public static int EliminarHojaDeParte(int id)
        {
            return HojaDeParteDAL.EliminarHojaDeParte(id);
        }

        public static List<HojaDeParte> MostrarHojasDeParte()
        {
            return HojaDeParteDAL.MostrarHojasDeParte();
        }

        public static HojaDeParte ObtenerHojaPorId(int id)
        {
            return HojaDeParteDAL.ObtenerHojaPorId(id);
        }

        public static List<HojaDeParte> BuscarHojaDeParte(string texto)
        {
            return HojaDeParteDAL.BuscarHojaDeParte(texto);
        }
    }
}