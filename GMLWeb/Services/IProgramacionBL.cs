﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLWeb.Services
{
    interface IProgramacionBL
    {

        void registrar(int codigo, int equipo, int tecnico, DateTime fecprogramada);

    }
}
