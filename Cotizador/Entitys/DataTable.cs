using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador.Entitys
{
    public class DataTable
    { //Clase Entidad Que Contiene Todas Los Parametros Que El Plugin datatable Necesita.
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public Searchs Search { get; set; }
        public Order[] Order { get; set; }
        public Columns[] Columns { get; set; }
    }
    public class Columns
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Searchs search { get; set; }

    }
    public class Order
    {
        public string dir { get; set; }
        public int column { get; set; }
    }
   public class Searchs
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

}