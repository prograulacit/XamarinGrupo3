using SQLite;

namespace ProyectoAndriodCsharp.Model
{
    [Table("Producto")]
    public class Producto
    {
        [PrimaryKey, AutoIncrement]
        public int PRO_ID { get; set; }
        public string PRO_NOMBRE { get; set; }
        public string PRO_DESCRIPCION { get; set; }
        public decimal PRO_PRECIO { get; set; }
        public string PRO_ESTADO{get;set;}
    }
}
