using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    public class Temporal
    {

        private static Temporal Instancia = null;

        public static Temporal ConseguirInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new Temporal();
            }
            return Instancia;


        }

        private Temporal()
        {

        }

        string conclusionTemporal;
        public List<string> premisasTemporal = new List<string>();

        public string ConclusionTemporal
        {
            get { return conclusionTemporal; }
            set { conclusionTemporal = value; }
        }
       
    }
}
