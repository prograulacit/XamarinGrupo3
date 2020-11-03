﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoAndriodCsharp.Model
{
    public class CompraProductosModel
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
