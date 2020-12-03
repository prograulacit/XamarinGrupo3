using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using ProyectoAndriodCsharp.Model;
using ProyectoAndriodCsharp.Objects;

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

        public static bool IngresarUsuario(Usuario usuario) {
            bool result = false;
            Console.WriteLine("********************* NO CREADO: " + usuario.UsuarioId);
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(DataConnection.GetConnectionPath()))
            {
                int filasAfectadas = sqliteConnection.Insert(usuario);
                if (filasAfectadas > 0) {
                    Console.WriteLine("********************* CREADO: " + usuario.UsuarioId);
                    result = true; }
            }
            return result;
        }

        public int InsertarUsuario(Usuario usuario)
        {
            int FilasAfectadas = 0;
            try
            {
                Console.WriteLine("********************* NO CREADO: " + usuario.UsuarioId);
                FilasAfectadas = sqliteConn.Insert(usuario);
                Console.WriteLine("********************* CREADO: " + usuario.UsuarioId);
                this.Estado = string.Format("Cantidad de filas afectadas: {0}", FilasAfectadas);
            }
            catch (Exception e)
            {
                this.Estado = e.Message;
            }
            return FilasAfectadas;
        }

        public static bool ExisteUsuarioByUsername(string username) {
            bool result = false;
            SQLiteConnection sQLiteConnection = new SQLiteConnection(DataConnection.GetConnectionPath());
            Usuario user=sQLiteConnection.Table<Usuario>().Where(i => i.NombreUsuario == username).FirstOrDefault();
            if (user!=null) { result = true; }
            return result;
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
                return sqliteConn.Table<Usuario>()
                    .Where(i => i.NombreUsuario == username).FirstOrDefault();
            }
        }

        public static bool ValidarUsuario(LoginRequest loginRequest)
        {
            bool result = false;
            Usuario usuario = GetUsuarioByUsername(loginRequest.Username);
            if (usuario != null)
            {
                if (usuario.NombreUsuario.Equals(loginRequest.Username) &&
                usuario.Contrasenia.Equals(loginRequest.Password) &&
                usuario.US_ROL.Equals("Usuario"))
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool ValidarAdministrador(LoginRequest loginRequest)
        {
            bool result = false;
            Usuario usuario = GetUsuarioByUsername(loginRequest.Username);

            if (usuario != null)
            {
                if (usuario.NombreUsuario.Equals(loginRequest.Username) &&
                    usuario.Contrasenia.Equals(loginRequest.Password) &&
                    usuario.US_ROL.Equals("Administrador"))
                {
                    result = true;
                }
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

        public bool NombreUsuarioUnico(string NombreUsuario)
        {
            Usuario registro = GetUsuarioByUsername(NombreUsuario);
            if (registro != null)
            {
                if (registro.UsuarioId == Memoria.UsuarioActual.UsuarioId)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
    }
}
