using BussinesEntities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GraficoTestManager
    {

        public List<GraficoTest> DatosGraficoTest()
        {
            using (SitcomEntities db = new SitcomEntities())
            {

                return db.Database.SqlQuery<GraficoTest>("EXEC [dbo].[Armar_Grafico]").ToList();

            }
        }
    }
}
