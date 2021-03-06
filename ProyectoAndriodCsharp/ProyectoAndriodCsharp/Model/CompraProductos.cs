﻿using SQLite;

namespace ProyectoAndriodCsharp.Model
{
    [Table("CompraProductos")]
    public class CompraProductos
    {
        public CompraProductos(int COM_ID, int PRO_ID, int COMP_CANTIDAD)
        {
            this.COM_ID = COM_ID;
            this.PRO_ID = PRO_ID;
            this.COMP_CANTIDAD = COMP_CANTIDAD;
        }

        public CompraProductos() { }

        [PrimaryKey, AutoIncrement]
        public int COMP_ID { get; set; }
        [Indexed]
        public int COM_ID { get; set; }
        [Indexed]
        public int PRO_ID { get; set; }
        public int COMP_CANTIDAD { get; set; }
    }
}
