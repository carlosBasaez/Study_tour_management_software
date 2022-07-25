using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Representante: Usuario
    {
        public override bool Delete(string ERut)
        {
            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("DELETE FROM REPRESENTANTE WHERE RUT=:RUT",ora);
                    command.Parameters.Add(":RUT", OracleDbType.Varchar2).Value = "11111";
                    command.ExecuteNonQuery();
                }
                catch(Exception ignacio)
                {
                    Console.WriteLine("error-------------" + ignacio);
                }
                ora.Close();
            }
            return true;
        }

        public override bool Insert(string NRut, string NEmail, string NPass, string NNombre, string NApellido)
        {
            using(OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("INSERT INTO REPRESENTANTE VALUES(:RUT,:EMAIL,:PASS,:NOMBRE,:APELLIDO)", ora);
                    command.Parameters.Add(":RUT", OracleDbType.Varchar2).Value = NRut;
                    command.Parameters.Add(":EMAIL", OracleDbType.Varchar2).Value = NEmail;
                    command.Parameters.Add(":PASS", OracleDbType.Varchar2).Value = NPass;
                    command.Parameters.Add(":NOMBRE", OracleDbType.Varchar2).Value = NNombre;
                    command.Parameters.Add(":APELLIDO", OracleDbType.Varchar2).Value = NApellido;
                    command.ExecuteNonQuery();
                }
                catch(Exception ignacio)
                {
                    Console.WriteLine("error----------" + ignacio);
                }
                ora.Close();
            }
            return true;
        }

        public override Usuario Select()
        {
            Representante repre = null;
            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM REPRESENTANTE", ora);
                    OracleDataReader lector = command.ExecuteReader();
                    if (lector.Read())
                    {
                        repre = new Representante();
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error Sergio" + Ex);
                }
                ora.Close();
            }
            return repre;
        }

        public Representante SelectRepresentante()
        {
            Representante repre = null;
            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM REPRESENTANTE", ora);
                    OracleDataReader lector = command.ExecuteReader();
                    if (lector.Read())
                    {
                        repre = new Representante();
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error Sergio" + Ex);
                }
                ora.Close();
            }
            return repre;
        }

        public override bool SelectInicioSesion()
        {
            bool indicador = false;
            using (OracleConnection ora = new OracleConnection(connection))
            {
                try
                {
                    ora.Open();
                    OracleCommand command = new OracleCommand("SELECT * FROM REPRESENTANTE WHERE RUTREPRESENTANTE=:Rut", ora);
                    command.Parameters.Add(":Rut", OracleDbType.Varchar2).Value = RutCurrent;
                    OracleDataReader lector = command.ExecuteReader();
                    if (lector.Read())
                    {
                        indicador = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                ora.Close();
            }
            return indicador;
        }

        public override Usuario SelectWhere(string Rut)
        {
            throw new NotImplementedException();
        }

        public override bool Update(string ARut, string AEmail, string APass, string ANombre, string AApellido)
        {
            throw new NotImplementedException();
        }
    }
}

