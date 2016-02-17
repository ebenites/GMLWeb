using GMLWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLWeb.Services
{
    interface IPlanificacionBL
    {

        List<int> listarAnios();

        int totalCronogramaActividades(int anio);

        List<PlanLocal> listarLocales(int anio, int estado);

        List<Tecnico> listarTecnicos(int anio, string tipo);

        void generar(int vanio, int vlocal, List<int> vtecnicos);

    }
}
