using DevelopmentChallenge.Data.Enums;
using System;
using System.Globalization;
using System.Resources;

namespace DevelopmentChallenge.Data.Utils
{
    public static class TraductorFormas
    {
        private static ResourceManager _manager = new ResourceManager(@"DevelopmentChallenge.Data.Resources.Res", typeof(TraductorFormas).Assembly);

        public static string Traducir(string forma, int cantidad, IdiomaEnum idioma)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            switch (idioma)
            {
                case IdiomaEnum.Castellano:
                    culture = new CultureInfo("es");
                    break;
                case IdiomaEnum.Ingles:
                    culture = new CultureInfo("en");
                    break;
                case IdiomaEnum.Italiano:
                    culture = new CultureInfo("it");
                    break;
            }

            string clave = cantidad == 1 ? forma : forma + "Plural";
            string traduccion = _manager.GetString(clave, culture);
            if (traduccion == null)
            {
                throw new ArgumentOutOfRangeException(nameof(forma), @"Forma no reconocida o idioma no soportado.");
            }

            return traduccion;
        }
    }
}
