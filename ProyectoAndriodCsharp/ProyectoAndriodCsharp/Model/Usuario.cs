using SQLite;

namespace ProyectoAndriodCsharp.Model
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int UsuarioId { get; set; }
        [Unique]
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public string US_ROL { get; set; }
        public string Email { get; set; }
    }
}
