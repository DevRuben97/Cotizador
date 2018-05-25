using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador.Entitys
{
    public class DataTable
    { //Clase Entidad Que Contiene Todas las variables contenedoras del datatable.
        public string[] search { get; set; }
        public string draw { get; set; }
        public string[][] order { get; set; }
        public string[][] orderdir { get; set; }
        public int starRec { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public int recordsFiltered { get; set; }
    }
}