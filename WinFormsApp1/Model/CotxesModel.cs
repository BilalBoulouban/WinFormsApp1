using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model
{
    public class CotxesModel
    {
           public string Marca { get; set; }
            public string Model { get; set; }
            public int Any { get; set; }
            public List<CaracteristicaModel> Caracteristiques { get; set; }

            public CotxesModel()
            {
                Caracteristiques = new List<CaracteristicaModel>();
            }
        }
    }