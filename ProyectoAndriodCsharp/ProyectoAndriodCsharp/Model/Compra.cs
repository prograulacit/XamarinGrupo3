using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Model
{
    
    public class Compra
    {
        [PrimaryKey,AutoIncrement]
        public int COM_ID { get; set; }

        [Indexed]
        public int US_ID { get; set; }

        public DateTime COM_FECHA_COMPRA { get; set; }
        public string COM_ESTADO { get; set; }
        public decimal COM_PRECIO_IVA { get; set; }
        public decimal COM_PRECIO_TOTAL { get; set; }
    }
}
