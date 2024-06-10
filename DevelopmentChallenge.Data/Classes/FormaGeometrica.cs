/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos.
 * Implementar la forma Trapecio/Rectangulo. 
 * Agregar el idioma Italiano (o el deseado) al reporte.
 * Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 */

using DevelopmentChallenge.Data.Enums;
using DevelopmentChallenge.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopmentChallenge.Data.Classes
{
    public class FormaGeometrica
    {
        #region Formas

        public const int Cuadrado = 1;
        public const int TrianguloEquilatero = 2;
        public const int Circulo = 3;
        public const int Trapecio = 4;

        #endregion

        public static string Imprimir(List<IFormaGeometrica> formas, IdiomaEnum idioma)
        {
            var sb = new StringBuilder();
            if (!formas.Any())
            {
                sb.Append(idioma == IdiomaEnum.Castellano ? "<h1>Lista vacía de formas!</h1>" : idioma == IdiomaEnum.Ingles ? "<h1>Empty list of shapes!</h1>" : "<h1>Elenco vuoto di forme!</h1>");
            }
            else
            {
                // Encabezado del reporte
                sb.Append(idioma == IdiomaEnum.Castellano ? "<h1>Reporte de Formas</h1>" : idioma == IdiomaEnum.Ingles ? "<h1>Shapes report</h1>" : "<h1>Rapporto sulle forme</h1>");

                // Agrupo formas por tipo
                var gruposFormas = formas.GroupBy(f => f.GetType().Name);

                // Calculo de área y perímetro total por tipo
                foreach (var grupo in gruposFormas)
                {
                    var areaTotal = grupo.Sum(forma => forma.CalcularArea());
                    var perimetroTotal = grupo.Sum(forma => forma.CalcularPerimetro());
                    sb.Append(ObtenerLinea(grupo.Count(), areaTotal, perimetroTotal, grupo.Key, idioma));
                }

                // Pie de página del reporte
                sb.Append("TOTAL:<br/>");
                sb.Append(formas.Count + " " + (idioma == IdiomaEnum.Castellano ? "formas" : idioma == IdiomaEnum.Ingles ? "shapes" : "forme") + " ");
                sb.Append((idioma == IdiomaEnum.Castellano ? "Perimetro " : idioma == IdiomaEnum.Ingles ? "Perimeter " : "Perimetro ") + formas.Sum(f => f.CalcularPerimetro()).ToString("#.##") + " ");
                sb.Append("Area " + formas.Sum(f => f.CalcularArea()).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, string tipo, IdiomaEnum idioma)
        {
            if (cantidad > 0)
            {
                if (idioma == IdiomaEnum.Castellano)
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>";
                if (idioma == IdiomaEnum.Italiano)
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | La zona {area:#.##} | Perimetro {perimetro:#.##} <br/>";

                return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";
            }

            return string.Empty;
        }

        private static string TraducirForma(string tipo, int cantidad, IdiomaEnum idioma)
        {
            switch (tipo)
            {
                case "Cuadrado":
                    if (idioma == IdiomaEnum.Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
                    else if (idioma == IdiomaEnum.Ingles) return cantidad == 1 ? "Square" : "Squares";
                    else return cantidad == 1 ? "Piazza" : "Piazze";
                case "Circulo":
                    if (idioma == IdiomaEnum.Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
                    else if (idioma == IdiomaEnum.Ingles) return cantidad == 1 ? "Circle" : "Circles";
                    else return cantidad == 1 ? "Cerchio" : "Cerchi";
                case "TrianguloEquilatero":
                    if (idioma == IdiomaEnum.Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
                    else if (idioma == IdiomaEnum.Ingles) return cantidad == 1 ? "Triangle" : "Triangles";
                    else return cantidad == 1 ? "Triangolo" : "Triangoli";
                case "Trapecio":
                    if (idioma == IdiomaEnum.Castellano) return cantidad == 1 ? "Trapecio" : "Trapecios";
                    else if (idioma == IdiomaEnum.Ingles) return cantidad == 1 ? "Trapeze" : "Trapezoids";
                    else return cantidad == 1 ? "Trapezio" : "Trapezi";
            }

            return string.Empty;
        }
    }
}
