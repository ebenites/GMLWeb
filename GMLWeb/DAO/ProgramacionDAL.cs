using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GMLWeb.Models;

namespace GMLWeb.DAO
{
    public class ProgramacionDAL : IProgramacionDAL
    {

        private GMLConnectionString db = new GMLConnectionString();

        public List<Solicitud> listar()
        {

            return db.Solicitud.ToList();
        }
        
        public void registrar(int codigo, int equipo, int tecnico, DateTime fecprogramada)
        {
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    // Registrando la OS

                    OrdenServicio orden = new OrdenServicio
                    {
                        numero = "OS",
                        fecha = fecprogramada,
                        estado = "P",
                        codigo_equipo = equipo,
                        codigo_tecnico = tecnico,
                        codigo_solicitud = codigo
                    };
                    db.OrdenServicio.Add(orden);

                    // Cambiando el estado de la solicitud

                    var solicitud = db.Solicitud.Include(s => s.Equipo).Include(s => s.Equipo.Local).Single(x => x.codigo == codigo);
                    solicitud.estado = "G";

                    // Grabando cambios

                    db.SaveChanges();

                    // Actualizando el numero de OS

                    orden.numero = "OS-" + DateTime.Now.ToString("yyyy-MM-") + orden.codigo.ToString("D4");

                    // Grabando cambios

                    db.SaveChanges();

                    tx.Commit();

                }
                catch (Exception)
                {
                    tx.Rollback();
                }
            }
        }


    }
}