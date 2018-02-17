using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{

    public class ReportesDataTableEntitiesFechaValor
    {

        public decimal valor { get; set; }
        public string fecha { get; set; }

    }

    public class ReportesCampoFechaValor
    {

        public string valor { get; set; }
        public string campo { get; set; }
        public string fecha { get; set; }

    }

    public class ReportesCampoCantidadValor
    {

        public string valor { get; set; }
        public string campo { get; set; }
        public string cantidad { get; set; }

    }

    public class ReportesCampoValor
    {

        public string Valor { get; set; }
        public string Campo { get; set; }
       

    }

    public class ReportesFechaValor
    {

        public string valor { get; set; }
        public string campo { get; set; }
        public string fecha { get; set; }

    }

 public class ReportesCampoValorValor
    {

        public string Valor { get; set; }
        public string Campo { get; set; }
        public string Valor_2 { get; set; }

    }


    public class ReportesCampoValorDinamico_old
 {
     public string Campo { get; set; }
     public string Valor { get; set; }
     public string Valor_2 { get; set; }
     public string Valor_3 { get; set; }
 }

 public class ReportesCampoValorDinamico
 {
     public string Campo { get; set; }
     public string Valor { get; set; }
     public string Valor_2 { get; set; }
     public string Valor_3 { get; set; }

     public string Etiqueta_1 { get; set; }

     public string Etiqueta_2 { get; set; }

     public string Etiqueta_3 { get; set; }
 }

    public class ReportesEncuesta
    {       
        List<ReportesCampoValor> preguntas { get; set; }
    }

    public class ReportesFechaValorValor
    {


        public string Fecha { get; set; }
        public string Valor { get; set; }
        public string Valor_2 { get; set; }
        

    }


    //public class ReportesGraficoPieEntities
    //{

    //    public string label { get; set; }
    //    public int valor { get; set; }


    //}

    //public class ReportesGraficoBarEntities
    //{

    //    public string label { get; set; }
    //    public int valor { get; set; }

    //}


}
