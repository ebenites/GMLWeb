using GMLWeb.DAO;
using GMLWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMLWeb.Services
{
    public class PlanificacionBL : IPlanificacionBL
    {

        private IPlanificacionDAL planificacionDAL = new PlanificacionDAL();

        public List<int> listarAnios()
        {
            return planificacionDAL.listarAnios();
        }

        public int totalCronogramaActividades(int anio)
        {
            return planificacionDAL.totalCronogramaActividades(anio);
        }

        public List<PlanLocal> listarLocales(int anio, int estado)
        {
            return planificacionDAL.listarLocales(anio, estado);
        }

        public List<Tecnico> listarTecnicos(int anio, string tipo)
        {
            return planificacionDAL.listarTecnicos(anio, tipo);
        }

        public void generar(int vanio, int vlocal, List<int> vtecnicos)
        {
            planificacionDAL.generar(vanio, vlocal, vtecnicos);
        }
    }
}