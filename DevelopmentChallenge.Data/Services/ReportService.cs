using DevelopmentChallenge.Data.Classes;
using DevelopmentChallenge.Data.Enums;
using DevelopmentChallenge.Data.Interfaces;
using DevelopmentChallenge.Data.Services.Interfaces;
using DevelopmentChallenge.Data.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;

namespace DevelopmentChallenge.Data.Services
{
    public class ReportService : IReportService
    {
        private static ResourceManager _resourceManager = new ResourceManager("DevelopmentChallenge.Data.Resources.Res", typeof(FormaGeometrica).Assembly);

        StringBuilder _cadenaReporte;

        public string Imprimir(List<IFormaGeometrica> formas, IdiomaEnum idioma)
        {
            _cadenaReporte = new StringBuilder();
            if (!formas.Any())
            {
                _cadenaReporte.Append("<h1>" + GetResourceValue("ListaVacia", idioma) + "</h1>");
            }
            else
            {
                GenerarEncabezado(idioma);
                GenerarCuerpo(formas, idioma);
                GenerarPiePagina(formas, idioma);
            }

            return _cadenaReporte.ToString();
        }

        private void GenerarEncabezado(IdiomaEnum idioma)
        {
            _cadenaReporte.Append("<h1>" + GetResourceValue("EncabezadoReporte", idioma) + "</h1>");
        }

        private void GenerarCuerpo(List<IFormaGeometrica> formas, IdiomaEnum idioma)
        {
            var gruposFormas = formas.GroupBy(f => f.GetType().Name);

            // Calculo de área y perímetro total por tipo
            foreach (var grupo in gruposFormas)
            {
                var areaTotal = grupo.Sum(forma => forma.CalcularArea());
                var perimetroTotal = grupo.Sum(forma => forma.CalcularPerimetro());
                _cadenaReporte.Append(ObtenerLinea(grupo.Count(), areaTotal, perimetroTotal, grupo.Key, idioma));
            }
        }

        private void GenerarPiePagina(List<IFormaGeometrica> formas, IdiomaEnum idioma)
        {
            StringBuilder piePagina = new StringBuilder();

            string total = GetResourceValue("Total", idioma);
            string formasTexto = GetResourceValue("Formas", idioma);
            string perimetroTexto = GetResourceValue("Perimetro", idioma);
            string areaTexto = GetResourceValue("Area", idioma);

            piePagina.Append($"{total}:<br/>");
            piePagina.Append($"{formas.Count} {formasTexto} ");
            piePagina.Append($"{perimetroTexto} {formas.Sum(f => f.CalcularPerimetro()).ToString("#.##")} ");
            piePagina.Append($"{areaTexto} {formas.Sum(f => f.CalcularArea()).ToString("#.##")}");

            _cadenaReporte.Append(piePagina.ToString());
        }

        public static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, string tipo, IdiomaEnum idioma)
        {
            var nombreForma = TraductorFormas.Traducir(tipo, cantidad, idioma);
            var linea = new StringBuilder();

            string formatoLinea = GetResourceValue("FormatoLinea", idioma);
            if (formatoLinea == null)
            {
                throw new InvalidOperationException("Formato de línea no encontrado para el idioma seleccionado.");
            }

            linea.AppendFormat(GetCulture(idioma), formatoLinea, cantidad, nombreForma, area, perimetro);

            return linea.ToString();
        }

        private static string GetResourceValue(string key, IdiomaEnum idioma)
        {
            CultureInfo culture = GetCulture(idioma);
            return _resourceManager.GetString(key, culture) ?? "Resource not found";
        }

        private static CultureInfo GetCulture(IdiomaEnum idioma)
        {
            switch (idioma)
            {
                case IdiomaEnum.Castellano:
                    return new CultureInfo("es-ES");
                case IdiomaEnum.Ingles:
                    return new CultureInfo("en-US");
                case IdiomaEnum.Italiano:
                    return new CultureInfo("it-IT");
                default:
                    return CultureInfo.InvariantCulture;
            }
        }
    }
}
