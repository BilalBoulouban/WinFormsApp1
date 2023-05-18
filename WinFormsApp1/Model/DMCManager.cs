using System;
using System.Collections.Generic;
using System.Xml;

namespace WinFormsApp1.Model
{
    public static class DMCManager
    {
        private static List<CampeonatModel> Campeonat = new List<CampeonatModel>();
        private static string message = "";
        public static bool CarregarModel(string filePath)
        {
            bool bres = false;
            try
            {
                // if (DTDValidator.Validate(filepath))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filePath);
                    XmlNodeList ElementsCamionat = doc.SelectNodes("//championship");
                    foreach (XmlNode campionat in ElementsCamionat)
                    {
                        CampeonatModel Championship = new CampeonatModel();
                        Championship.Year = int.Parse(campionat.Attributes["Year"].InnerText);
                        Championship.Country = campionat.Attributes["Country"].Value;
                        Championship.City = campionat.Attributes["City"].Value;

                        XmlNodeList ElementsDJ = campionat.SelectNodes("//dj");
                        foreach (XmlNode discjockey in ElementsDJ)
                        {
                            DJ DJ = new DJ();
                            DJ.local = discjockey.Attributes["local"].Value;
                            DJ.nom = discjockey.Attributes["name"].InnerText;
                            DJ.pos = int.Parse(discjockey.SelectSingleNode("pos").InnerText);
                            Championship.addDJ(DJ);
                        }
                        Campeonat.Add(Championship);
                    }
                    bres = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return bres;
        }
    }
}