﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
  public  class ConjuntoDisponibildiadEntity
    {


        //public List<DisponibilidadEntity> ListaDisponibilidad { get; set; }
        public string mensaje { get; set; }
        public string codigo { get; set; } 
        public virtual List<DisponibilidadEntity> ListaDisponibilidad { get; set; }

    }
}
