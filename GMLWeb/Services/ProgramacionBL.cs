using GMLWeb.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMLWeb.Services
{
    public class ProgramacionBL : IProgramacionBL
    {

        private IProgramacionDAL programacionDAL = new ProgramacionDAL();

        public void registrar(int codigo, int equipo, int tecnico, DateTime fecprogramada)
        {
            programacionDAL.registrar(codigo, equipo, tecnico, fecprogramada);
        }

    }
}