using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model
{
    internal class Concessionari
    {
            public int Id { get; set; }
            public string Nom { get; set; }
            public string Carrer { get; set; }
            public string Ciutat { get; set; }
            public string CodiPostal { get; set; }
            public string Telefon { get; set; }
            public string Dilluns { get; set; }
            public string Dimarts { get; set; }
            public string Dimecres { get; set; }
            public string Dijous { get; set; }
            public string Divendres { get; set; }
            public string Dissabte { get; set; }
            public string Diumenge { get; set; }
        public List<CaracteristicaModel> Caracteristiques { get; set; }

    }
}
