﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using SHomies.Utilitario;
using System.Data;

namespace SHomies.Core.Planilla
{
    public class Trabajador : Persona.PersonaNatural
    {
        public Cargo Cargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        public bool Estado { get; set; }

        public Trabajador()
        {

        }
        public Trabajador(IConexion iConexion)
            : base(iConexion)
        {

        }

        public List<Trabajador> ListaPorCargo(Cargo iCargo)
        {
            List<Trabajador> trabajadores = new List<Trabajador>();
            try
            {
                DataSet datos = new DataSet();

                this.Conexion.NombrePaquete = "administra_trabajador";
                this.Conexion.QuerySQL = "lista_por_cargo";
                this.Conexion.SetValorParametroInput("i_cargo", iCargo.Id);

                this.Conexion.ExecuteProcedure(out datos);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(datos))
                    if (!Funcion.IsDataTableEmpty(datos.Tables[0]))
                    {
                        datos.Tables[0].Columns["idcargo"].ColumnName = "Cargo.Id";
                        datos.Tables[0].Columns["descripcioncargo"].ColumnName = "Cargo.Descripcion";

                        trabajadores = Funcion.ConvertToList<Trabajador>(datos.Tables[0]);
                    }
            }
            catch (Exception)
            {

                throw;
            }

            return trabajadores;
        }

        public override void Nuevo()
        {
            try
            {
                base.Nuevo();

                this.Conexion.NombrePaquete = "administra_trabajador";
                this.Conexion.QuerySQL = "nuevo";

                this.Conexion.SetValorParametroInput("i_id", this.Id);
                this.Conexion.SetValorParametroInput("i_fechaingreso", this.FechaIngreso.ToShortDateString());
                this.Conexion.SetValorParametroInput("i_idcargo", this.Cargo.Id);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ChangeEstado()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_trabajador";
                this.Conexion.QuerySQL = "changeestado";

                this.Conexion.SetValorParametroInput("i_id", this.Id);
                this.Conexion.SetValorParametroInput("i_estado", this.Estado);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
