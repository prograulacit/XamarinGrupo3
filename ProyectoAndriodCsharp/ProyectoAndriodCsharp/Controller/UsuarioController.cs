using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Models;

namespace ProyectoAndriodCsharp.Controller
{
    public class UsuarioController
    {
        private SQLiteConnection sqliteConn;
        public string Estado = ""; // Estado al realizar alguna consulta.

        public UsuarioController(string ConnectionPath)
        {
            sqliteConn = new SQLiteConnection(ConnectionPath);
            sqliteConn.CreateTable<Model.Usuario>(); // Crea la tabla si no existe.
        }

        // -------------------------------------------------------------------
        // Funciones CRUD.

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            try
            {
                return sqliteConn.Table<Model.Usuario>();
            }
            catch (Exception e)
            {
                this.Estado = e.Message;
            }
            return Enumerable.Empty<Model.Usuario>();
        }

        public Usuario GetUsuario(int id)
        {
            try
            {
                return sqliteConn.Table<Model.Usuario>()
                .Where(i => i.UsuarioId == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                this.Estado = e.Message;
            }
            return null;
        }

        public int InsertarUsuario(Usuario Usuario)
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
        

        public int ActualizarUsuario(Usuario Usuario)
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

        public static Usuario GetUsuarioByUsername(string username)
        {
            using (SQLiteConnection sqliteConn = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                return sqliteConn.Table<Model.Usuario>().Where(i => i.NombreUsuario == username).FirstOrDefault();
            }
        }
        public static bool ValidarUsuario(LoginRequest loginRequest) {
            bool result = false;
            Usuario usuario=GetUsuarioByUsername(loginRequest.Username);
            if (usuario.NombreUsuario.Equals(loginRequest.Username) && usuario.Contrasenia.Equals(loginRequest.Password) && usuario.US_ROL.Equals("Usuario")) {
                result = true;
            }
            return result;
        }
        public static bool ValidarAdministrador(LoginRequest loginRequest)
        {
            bool result = false;
            Usuario usuario = GetUsuarioByUsername(loginRequest.Username);
            if (usuario.NombreUsuario.Equals(loginRequest.Username) && usuario.Contrasenia.Equals(loginRequest.Password) && usuario.US_ROL.Equals("Administrador"))
            {
                result = true;
            }
            return result;
        }

        public int EliminarUsuario(Usuario Usuario)
        {
            int FilasAfectadas = 0;
            try
            {
                FilasAfectadas = sqliteConn.Delete<Usuario>(Usuario);
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
