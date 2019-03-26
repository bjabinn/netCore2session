using System;
using System.Collections.Generic;

namespace EF_FromSql.Models
{
    public partial class Articulos
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double? Precio { get; set; }
    }
}
