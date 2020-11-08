using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Model
{
    [Table("AbonoPorMes")]
    public class AbonoPorMes
    {
        [PrimaryKey,AutoIncrement]
        public int ABEM_ID { get; set; }
        [Indexed]
        public int ABO_ID { get; set; }
        public DateTime ABEM_FECHA_DEPOSITADA { get; set; }
        public decimal ABEM_CANTIDAD_DEPOSITADA { get; set; }
    }
}
