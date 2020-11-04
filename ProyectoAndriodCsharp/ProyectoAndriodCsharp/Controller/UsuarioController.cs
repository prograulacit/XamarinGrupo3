using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using ProyectoAndriodCsharp.Model;

namespace ProyectoAndriodCsharp.Controller
{
    public class UsuarioController
    {
        private SQLiteConnection sqliteConn;
        public string Estado = ""; // Estado al realizar alguna consulta.

        public UsuarioController(string ConnectionPath)
        {
            sqliteConn = new SQLiteConnection(ConnectionPath);
            sqliteConn.CreateTable<UsuarioModel>(); // Crea la tabla si no existe.
        }

        // -------------------------------------------------------------------
        // Funciones CRUD.

        public IEnumerable<UsuarioModel> GetAllUsuarios()
        {
            try
            {
                return sqliteConn.Table<UsuarioModel>();
            }
            catch (Exception e)
            {
                this.Estado = e.Message;
            }
            return Enumerable.Empty<UsuarioModel>();
        }

        public UsuarioModel GetUsuario(int id)
        {
            try
            {
                return sqliteConn.Table<UsuarioModel>()
                .Where(i => i.UsuarioId == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                this.Estado = e.Message;
            }
            return null;
        }

        public int InsertarUsuario(UsuarioModel Usuario)
        {
            int FilasAfectadas = 0;
            try
            {
                FilasAfectadas = sqliteConn.Insert(Usuario);
                this.Estado = string.Format("Cantidad de filas afectadas: {0}", FilasAfectadas);
            }
            catch (Exception e)
            {
                this.Estado = e.Message;
            }
            return FilasAfectadas;
        }

        public int ActualizarUsuario(UsuarioModel Usuario)
        {
            int FilasAfectadas = 0;
            try
            {
                FilasAfectadas = sqliteConn.Update(Usuario);
                this.Estado = string.Format("Cantidad de filas afectadas: {0}", FilasAfectadas);
            }
            catch (Exception e)
            {
                this.Estado = e.Message;
            }
            return FilasAfectadas;
        }

        public int EliminarUsuario(UsuarioModel Usuario)
        {
            int FilasAfectadas = 0;
            try
            {
                FilasAfectadas = sqliteConn.Delete<UsuarioModel>(Usuario);
                this.Estado = string.Format("Cantidad de filas afectadas: {0}", FilasAfectadas);
            }
            catch (Exception e)
            {
                this.Estado = e.Message;
            }
            return FilasAfectadas;
        }

    }
}
