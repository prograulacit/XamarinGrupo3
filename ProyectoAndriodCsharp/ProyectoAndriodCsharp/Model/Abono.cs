 using SQLite;

namespace ProyectoAndriodCsharp.Model
{
    [Table("Abono")]
    public class Abono
    {
        [PrimaryKey,AutoIncrement]
        public int ABO_ID { get; set; }
        [Indexed]
        public int COM_ID { get; set; }
        public decimal ABO_CANTIDAD_MENSUAL { get; set; }
        public int ABO_CANTIDAD_DE_MESES { get; set; }
        public int ABO_RESTANTE { get; set; }
    }
}
