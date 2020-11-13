using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Model
{
    [Table("CompraProductos")]
    public class CompraProductos
    {
        [PrimaryKey,AutoIncrement]
        public int COMP_ID { get; set; }
        [Indexed]
        public int COM_ID { get; set; }
        [Indexed]
        public int PRO_ID { get; set; }
        public int COMP_CANTIDAD { get; set; }
    }
}
