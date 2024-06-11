using DevelopmentChallenge.Data.Classes;
using DevelopmentChallenge.Data.Enums;
using DevelopmentChallenge.Data.Interfaces;
using DevelopmentChallenge.Data.Services;
using DevelopmentChallenge.Data.Services.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace DevelopmentChallenge.Data.Tests
{
    [TestFixture]
    public class ReportDataTests
    {
        private IReportService _reporte;

        [SetUp]
        public void SetUp()
        {
            _reporte = new ReportService();
        }


        [TestCase]
        public void TestResumenListaVacia()
        {
            Assert.AreEqual("<h1>Lista vacía de formas!</h1>",
                _reporte.Imprimir(new List<IFormaGeometrica>(), IdiomaEnum.Castellano));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnIngles()
        {
            Assert.AreEqual("<h1>Empty list of shapes!</h1>",
                _reporte.Imprimir(new List<IFormaGeometrica>(), IdiomaEnum.Ingles));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnItaliano()
        {
            Assert.AreEqual("<h1>Elenco vuoto di forme!</h1>",
                _reporte.Imprimir(new List<IFormaGeometrica>(), IdiomaEnum.Italiano));
        }

        [TestCase]
        public void TestResumenListaConUnCuadradoEnCastellano()
        {
            var cuadrado = new List<IFormaGeometrica> { new Cuadrado(5) };
            var resumen = _reporte.Imprimir(cuadrado, IdiomaEnum.Castellano);

            Assert.AreEqual("<h1>Reporte de Formas</h1>1 Cuadrado | Area 25 | Perímetro 20 <br/>TOTAL:<br/>1 formas Perimetro 20 Area 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConUnCuadradoEnIngles()
        {
            var cuadrado = new List<IFormaGeometrica> { new Cuadrado(5) };
            var resumen = _reporte.Imprimir(cuadrado, IdiomaEnum.Ingles);

            Assert.AreEqual("<h1>Shapes report</h1>1 Square | Area 25 | Perimeter 20 <br/>TOTAL:<br/>1 shapes Perimeter 20 Area 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConUnTrapecioEnItaliano()
        {
            var trapecio = new List<IFormaGeometrica> { new Trapecio(9.5m, 3.5m, 4) };
            var resumen = _reporte.Imprimir(trapecio, IdiomaEnum.Italiano);

            Assert.AreEqual("<h1>Rapporto sulle forme</h1>1 Trapezio | La zona 26 | Perimetro 17 <br/>TOTALE:<br/>1 forme Perimetro 17 La zona 26", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasCuadrados()
        {
            var cuadrados = new List<IFormaGeometrica>
            {
                new Cuadrado(5),
                new Cuadrado(1),
                new Cuadrado(3)
            };

            var resumen = _reporte.Imprimir(cuadrados, IdiomaEnum.Ingles);

            Assert.AreEqual("<h1>Shapes report</h1>3 Squares | Area 35 | Perimeter 36 <br/>TOTAL:<br/>3 shapes Perimeter 36 Area 35", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnIngles()
        {
            var formas = new List<IFormaGeometrica>
            {
                new Cuadrado(5),
                new Circulo(3),
                new TrianguloEquilatero(4),
                new Cuadrado(2),
                new TrianguloEquilatero(9),
                new Circulo(2.75m),
                new TrianguloEquilatero(4.2m)
            };

            var resumen = _reporte.Imprimir(formas, IdiomaEnum.Ingles);

            Assert.AreEqual(
                "<h1>Shapes report</h1>2 Squares | Area 29 | Perimeter 28 <br/>2 Circles | Area 52.03 | Perimeter 36.13 <br/>3 Triangles | Area 49.64 | Perimeter 51.6 <br/>TOTAL:<br/>7 shapes Perimeter 115,73 Area 130,67",
                resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnCastellano()
        {
            var formas = new List<IFormaGeometrica>
            {
                new Cuadrado(5),
                new Circulo(3),
                new TrianguloEquilatero(4),
                new Cuadrado(2),
                new TrianguloEquilatero(9),
                new Circulo(2.75m),
                new TrianguloEquilatero(4.2m)
            };

            var resumen = _reporte.Imprimir(formas, IdiomaEnum.Castellano);

            Assert.AreEqual(
                "<h1>Reporte de Formas</h1>2 Cuadrados | Area 29 | Perímetro 28 <br/>2 Círculos | Area 52,03 | Perímetro 36,13 <br/>3 Triángulos | Area 49,64 | Perímetro 51,6 <br/>TOTAL:<br/>7 formas Perimetro 115,73 Area 130,67",
                resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnItaliano()
        {
            var formas = new List<IFormaGeometrica>
            {
                new Cuadrado(5),
                new Circulo(3),
                new TrianguloEquilatero(4),
                new Trapecio(18.5m, 6.5m, 8),

                new Cuadrado(2),
                new Circulo(4.75m),
                new TrianguloEquilatero(9),
                new Trapecio(9.5m, 3.5m, 4)
            };

            var resumen = _reporte.Imprimir(formas, IdiomaEnum.Italiano);

            Assert.AreEqual(
                "<h1>Rapporto sulle forme</h1>2 Piazze | La zona 29 | Perimetro 28 <br/>2 Cerchi | La zona 99,16 | Perimetro 48,69 <br/>2 Triangoli | La zona 42 | Perimetro 39 <br/>2 Trapezi | La zona 126 | Perimetro 50 <br/>TOTALE:<br/>8 forme Perimetro 165,69 La zona 296,16",
                resumen);
        }
    }
}
