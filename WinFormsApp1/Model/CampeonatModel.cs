using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model
{
    public class CampeonatModel
    {
        private int year;
        private string city;
        private string country;
        private List<DJ> Disjockeys = new List<DJ>();

        public int Year { get => year; set => year = value; }
        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public void addDJ(DJ dJ)
        {
            Disjockeys.Add(dJ);
        }
    }
}
