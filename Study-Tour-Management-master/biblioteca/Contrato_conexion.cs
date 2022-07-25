using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Contrato_conexion
    {
        [Obsolete]
        public static int insertar_contrato(Contrato newContrato)
        {
            Int32 rowsAffected = 0;
            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {

            }
                return rowsAffected;
        }

        public static bool CreareContrato(Contrato contrato, Curso curso, Representante representante, List<Apoderado> list_apoderados, List<Alumno> list_alumnos)
        {
            bool allCorrect = false;

            using (OracleConnection ora = new OracleConnection(InicioSesionBD.connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand(null, ora);
                    OracleTransaction transaccion = ora.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    command.Transaction = transaccion;
                    try
                    {

                        //Agregar Representante
                        try
                        {
                            //usuario
                            command.CommandText = $"SELECT rut FROM usuario WHERE rut = '{representante.Rut}'";
                            var reader_rep_user = command.ExecuteReader();
                            if (!reader_rep_user.Read())
                            {
                                command.CommandText = $"INSERT INTO usuario VALUES ( " +
                                    $" '{representante.Rut}' , " +
                                    $" '{representante.Email}' , " +
                                    $" '{representante.Pass}' , " +
                                    $" '{representante.Nombre}' , " +
                                    $" '{representante.Apellido}'" +
                                    $")";
                                command.ExecuteNonQuery();
                            }
                            //representante
                            command.CommandText = $"SELECT rutRepresentante FROM representante WHERE rutRepresentante = '{representante.Rut}'";
                            var reader_rep = command.ExecuteReader();
                            if (!reader_rep.Read())
                            {
                                command.CommandText = $"INSERT INTO representante VALUES ( " +
                                    $" '{representante.Rut}' " +
                                    $")";
                                command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Excepcion en crear nuevo representante. " + ex.Message);
                            throw ex;
                        }

                        //Agregar Curso
                        try
                        {
                            command.CommandText = $"INSERT INTO curso VALUES (seq_cursoID.nextval," +
                                $" '{curso.nombre}' , " +
                                $" '{representante.Rut}' ," +
                                $" {curso.id_colegio}" +
                                $")";
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Excepcion en crear nuevo curso. " + ex.Message);
                            throw ex;
                        }

                        //Agregar Contrato
                        try
                        {
                            command.CommandText = $"INSERT INTO contrato VALUES (seq_contratoID.nextval, " +
                                $" seq_cursoID.currval , " +
                                $" {contrato.monto_objetivo} ," +
                                $" to_date('{Utilities.DateTimeToString(DateTime.Now)}', 'yyyy-mm-dd') ," +
                                $" to_date('{Utilities.DateTimeToString(contrato.fecha_viaje)}', 'yyyy-mm-dd') , " +
                                $" '{contrato.observaciones}' , " +
                                $" {contrato.id_plan_viaje}" +
                                $")";
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Excepcion en insertar nuevo contrato. " + ex.Message);
                            throw ex;
                        }


                        int cantidad_inserciones = (list_alumnos.Count <= list_apoderados.Count ? list_alumnos.Count : list_apoderados.Count);

                        for (int i = 0; i < cantidad_inserciones; i++)
                        {
                            //Agregar Usuario
                            try
                            {
                                command.CommandText = $"SELECT rut FROM usuario WHERE rut = '{list_apoderados[i].Rut}'";
                                var reader_apod_user = command.ExecuteReader();
                                if (!reader_apod_user.Read())
                                {
                                    command.CommandText = $"INSERT INTO usuario VALUES ( " +
                                        $" '{list_apoderados[i].Rut}' , " +
                                        $" '{list_apoderados[i].Email}' , " +
                                        $" '{list_apoderados[i].Pass}' , " +
                                        $" '{list_apoderados[i].Nombre}' , " +
                                        $" '{list_apoderados[i].Apellido}'" +
                                        $")";
                                    command.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error en agregar usuario_apoderado. list numero: " + i + "\n" + ex.Message);
                                throw ex;
                            }
                            //Agregar apoderado
                            try
                            {
                                command.CommandText = $"SELECT rutApoderado FROM apoderado WHERE rutApoderado = '{list_apoderados[i].Rut}'";
                                var reader_apod = command.ExecuteReader();
                                if (!reader_apod.Read())
                                {
                                    command.CommandText = $"INSERT INTO apoderado VALUES ( " +
                                        $" '{list_apoderados[i].Rut}' " +
                                        $")";
                                    command.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error en agregar apoderado. list numero: " + i + "\n" + ex.Message);
                                throw ex;
                            }

                            //Agregar alumno
                            try
                            {
                                command.CommandText = $"SELECT * FROM alumno WHERE rutAlumno = '{list_alumnos[i].rut_alumno}' AND rutApoderado = '{list_apoderados[i].Rut}'";
                                var reader_alumno = command.ExecuteReader();
                                if (!reader_alumno.Read())
                                {
                                    command.CommandText = $"INSERT INTO alumno VALUES ( " +
                                        $" '{list_alumnos[i].rut_alumno}' , " +
                                        $" '{list_apoderados[i].Rut}' , " +
                                        $" '{list_alumnos[i].nombre}' , " +
                                        $" '{list_alumnos[i].apellido}' " +
                                        $")";
                                    command.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error en agregar alumno. list numero: " + i + "\n" + ex.Message);
                                throw ex;
                            }

                            //Agregar Cuenta
                            try
                            {
                                command.CommandText = $"SELECT seq_cursoid.currval FROM dual";
                                Int32 idCurso = Int32.Parse(command.ExecuteScalar().ToString());

                                command.CommandText = $"SELECT * FROM cuenta WHERE rutAlumno = '{list_alumnos[i].rut_alumno}' AND rutApoderado = '{list_apoderados[i].Rut}' AND idCurso = {idCurso}";
                                var reader_cuenta = command.ExecuteReader();
                                if (!reader_cuenta.Read())
                                {
                                    command.CommandText = $"INSERT INTO cuenta VALUES (seq_cuentaID.nextval, " +
                                        $" '{list_alumnos[i].rut_alumno}' , " +
                                        $" '{list_apoderados[i].Rut}' , " +
                                        $" {idCurso} , " +
                                        $" 0 " +
                                        $")";
                                    command.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error en agregar cuenta. list numero: " + i + "\n" + ex.Message);
                                throw ex;
                            }

                        }

                        //Commit
                        try
                        {
                            transaccion.Commit();
                        }
                        catch (Exception ex_commit)
                        {
                            Console.WriteLine("Error en commit de contrato.\n" + ex_commit.Message);
                            throw ex_commit;
                        }
                        allCorrect = true;
                    }
                    catch (Exception ex)
                    {
                        allCorrect = false;
                        Console.WriteLine("Error en crear contrato.\n" + ex.Message);

                        //RollBack
                        try
                        {
                            transaccion.Rollback();
                            Console.WriteLine("RollBack de la Base de Datos");
                        }
                        catch (Exception ex_rollback)
                        {
                            Console.WriteLine("Error en rollback de contrato.\n" + ex_rollback.Message);
                        }
                    }
                    ora.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en conectar contrato.\n" + ex.Message);
                }
            }
            return allCorrect;
        }


    }
}
