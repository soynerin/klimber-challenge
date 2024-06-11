using DevelopmentChallenge.Data.Enums;
using DevelopmentChallenge.Data.Interfaces;
using System.Collections.Generic;

namespace DevelopmentChallenge.Data.Services.Interfaces
{
    public interface IReportService
    {
        string Imprimir(List<IFormaGeometrica> formas, IdiomaEnum idioma);
    }
}
