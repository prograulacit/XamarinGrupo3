using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Model
{
    public class ProductoModel
    {
        [PrimaryKey,AutoIncrement]
        public int PRO_ID { get; set; }
        public string PRO_NOMBRE { get; set; }
        public string PRO_DESCRIPCION { get; set; }
        public decimal PRO_PRECIO { get; set; }
    }
}
