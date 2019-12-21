using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Recarga.Models.ViewModels
{
    public class DashboardViewModel
    {
        public virtual ICollection<Posto> Top5PostosMesCorrente { get; set; }
        public virtual ICollection<Posto> Top5PostosAnoCorrente { get; set; }
        public virtual ICollection<Distrito> Top5Distritos { get; set; }
        public virtual ICollection<Concelho> Top5Concelhos { get; set; }

        public int ReservasDiaCorrente { get; set; }
        public int ReservasMesCorrente { get; set; }
        public int ReservasAnoCorrente { get; set; }


    }



}