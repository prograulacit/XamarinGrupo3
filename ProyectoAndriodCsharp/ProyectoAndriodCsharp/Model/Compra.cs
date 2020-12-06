using SQLite;
using System;

namespace ProyectoAndriodCsharp.Model
{
    [Table("Compra")]
    public class Compra
    {
        [PrimaryKey, AutoIncrement]
        public int COM_ID { get; set; }

        [Indexed]
        public int US_ID { get; set; }

        public DateTime COM_FECHA_COMPRA { get; set; }
        public DateTime COM_SIGUIENTE_PAGO { get; set; }
        public string COM_ESTADO { get; set; } // Propiedad obsoleta.
        public decimal COM_INTERES { get; set; }
        public decimal COM_PRECIO_TOTAL { get; set; }
    }
}
