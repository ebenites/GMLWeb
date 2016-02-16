using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMLWeb.Models;
using System.Data.SqlClient;

namespace GMLWeb.DAO
{
    public class PlanificacionDAL : IPlanificacionDAL
    {

        private GMLConnectionString db = new GMLConnectionString();

        public List<int> listarAnios() {
            int anio_max = DateTime.Now.Year + 1;
            int anio_min = db.Database.SqlQuery<int>("SELECT min(anio) FROM PlanMantenimiento").Single();
            List<int> anios = new List<int>();
            for (int anio = anio_max; anio >= anio_min; anio--) {
                anios.Add(anio);
            }
            return anios;
        }

        public List<PlanLocal> listarLocales(int anio, int estado)
        {

            string sql = "select codigo, nombre, " +
                "(select count(*) from equipo where codigo_local = l.codigo) as nequipos, " +
                "(select count(*) from planmantenimiento where codigo_local = l.codigo and anio = @anio) estado " +
                "from local l ";

            if (estado != -1)
            {
                sql += "where(select count(*) from planmantenimiento where codigo_local = l.codigo and anio = @anio) = @estado";
            }

            List<PlanLocal> planlocales = db.Database.SqlQuery<PlanLocal>(sql, new SqlParameter("anio", anio), new SqlParameter("estado", estado)).ToList();
            
            return planlocales;
        }

        public List<Tecnico> listarTecnicos(int anio, string tipo)
        {
            string sql = "select codigo, dni, nombres, apellidos, especialidad, tipo from tecnico t " +
                "where not exists( " +
                "   select * from disponibilidad d " +
                "   inner join planmantenimiento p on p.codigo = d.codigo_plan and p.anio = @anio " +
                "   where codigo_tecnico = t.codigo " +
                ") ";

            if(tipo != String.Empty)
            {
                sql += "and tipo = @tipo";
            }

            List<Tecnico> tecnicos = db.Database.SqlQuery<Tecnico>(sql, new SqlParameter("anio", anio), new SqlParameter("tipo", tipo)).ToList();

            return tecnicos;
        }

        public int generar(int vanio, int vlocal, List<int> vtecnicos)
        {
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {

                    // Registrando el PlanMantenimiento

                    PlanMantenimiento plan = new PlanMantenimiento
                    {
                        anio = vanio,
                        codigo_local = vlocal
                    };
                    db.PlanMantenimiento.Add(plan);

                    db.SaveChanges();

                    // Registrando cambio de disponibilidad por cada técnico

                    foreach(int vtecnico in vtecnicos)
                    {
                        Disponibilidad disponibilidad = new Disponibilidad
                        {
                            codigo_plan = plan.codigo,
                            codigo_tecnico = vtecnico
                        };
                        db.Disponibilidad.Add(disponibilidad);
                    }
                    
                    // Consultar cronogrma de cada equipo por local

                    // Generar orden de servicio por cada actividad

                    /*db.Database.ExecuteSqlCommand(
                        @"UPDATE Blogs SET Rating = 5" +
                            " WHERE Name LIKE '%Entity Framework%'"
                        );

                    var query = db.Posts.Where(p => p.Blog.Rating >= 5);
                    foreach (var post in query)
                    {
                        post.Title += "[Cool Blog]";
                    }*/

                    db.SaveChanges();

                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                }
            }
            return 0;
        }

    }
}