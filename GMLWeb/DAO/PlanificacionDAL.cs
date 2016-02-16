using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMLWeb.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;

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

        public int totalCronogramaActividades(int anio)
        {
            string sql = "select count(*) as total " +
                "from cronogramadetalle d " +
                "inner join cronograma c on c.codigo = d.codigo_cronograma and c.anio = @anio";

            int total = db.Database.SqlQuery<int>(sql, new SqlParameter("anio", anio)).SingleOrDefault();

            return total;
        }

        public List<Tecnico> listarTecnicos(int anio, string tipo)
        {
            string sql = "select codigo, dni, nombres, apellidos, especialidad, tipo from tecnico t " +
                "where exists( " +
                "   select c.anio, d.semana " +
                "   from cronogramadetalle d " +
                "   inner join cronograma c on c.codigo = d.codigo_cronograma and c.anio = @anio " +
                "   except " +
                "   select d.anio, d.semana " +
                "   from disponibilidad d " +
                "   where d.anio = @anio and d.codigo_tecnico = t.codigo " +
                ") ";

            if(tipo != String.Empty)
            {
                sql += "and tipo = @tipo";
            }

            List<Tecnico> tecnicos = db.Database.SqlQuery<Tecnico>(sql, new SqlParameter("anio", anio), new SqlParameter("tipo", tipo)).ToList();

            return tecnicos;
        }

        public void generar(int vanio, int vlocal, List<int> vtecnicos)
        {
            System.Diagnostics.Debug.WriteLine("generar:" + vanio + " - " + vlocal + " - " + vtecnicos);

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

                    // Listar las actividades de cada cronograma del año-local

                    /*
                    select d.codigo, d.semana 
                    from CronogramaDetalle d
                    inner join Cronograma c on c.codigo = d.codigo_cronograma and c.anio = 2015
                    inner join Equipo e on e.codigo = c.codigo_equipo and e.codigo_local = 1
                    */

                    List<CronogramaDetalle> detalles = db.CronogramaDetalle.Include(x => x.Cronograma).Include(x => x.Cronograma.Equipo)
                        .Where(x => x.Cronograma.anio == vanio && x.Cronograma.Equipo.codigo_local == vlocal).ToList();

                    int i = 0;
                    foreach (CronogramaDetalle detalle in detalles)
                    {

                        System.Diagnostics.Debug.WriteLine("CronogramaDetalle:" + detalle);

                        // Seleccionando Técnico disponible
                        do
                        {
                            int vtecnico = vtecnicos.ElementAt(i++);
                            List<Disponibilidad> disponibilidades = db.Disponibilidad.Where(x => x.codigo_tecnico == vtecnico && x.anio == detalle.Cronograma.anio && x.semana == detalle.semana).ToList();
                            System.Diagnostics.Debug.WriteLine("Disponibilidad: TECNICO: " + vtecnico + " ANIO:" + detalle.Cronograma.anio + " SEMANA:" + detalle.semana);
                            System.Diagnostics.Debug.WriteLine("Disponible?:" + disponibilidades.Count);

                            if (disponibilidades.Count == 0)
                            {

                                // Técnico encontrado, registrando disponibilidad
                                System.Diagnostics.Debug.WriteLine("Técnico asignado:" + vtecnico);

                                Disponibilidad disponibilidad = new Disponibilidad
                                {
                                    codigo_tecnico = vtecnico,
                                    anio = detalle.Cronograma.anio,
                                    semana = detalle.semana
                                };
                                db.Disponibilidad.Add(disponibilidad);

                                db.SaveChanges();

                                // Registrando  Detalle del PanMantenimiento

                                PlanMantenimientoDetalle plandetalle = new PlanMantenimientoDetalle
                                {
                                    codigo_planmantenimiento = plan.codigo,
                                    codigo_cronogramadetalle = detalle.codigo,
                                    codigo_disponibilidad = disponibilidad.codigo
                                };
                                db.PlanMantenimientoDetalle.Add(plandetalle);

                                db.SaveChanges();

                                break;
                            }

                            if(i == vtecnicos.Count)
                            {
                                throw new Exception("No existe técnicos disponibles para la semana " + detalle.semana + " en el año " + detalle.Cronograma.anio);
                            }

                        } while (true);
                        
                    }
                    
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