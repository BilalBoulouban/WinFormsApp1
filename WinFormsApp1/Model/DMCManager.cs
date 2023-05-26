using System;
using System.Collections.Generic;
using System.Xml;
using WinFormsApp1.DAO;

namespace WinFormsApp1.Model
{
    public static class DMCManager
    {
        public static bool CarregarModel(string filePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                foreach (XmlNode concessionariNode in doc.SelectNodes("//concessionari"))
                {
                    ConcessionariModel concessionari = new ConcessionariModel();
                    concessionari.Nom = concessionariNode.SelectSingleNode("nom")?.InnerText;
                    concessionari.Carrer = concessionariNode.SelectSingleNode("adreça/carrer")?.InnerText;
                    concessionari.Ciutat = concessionariNode.SelectSingleNode("adreça/ciutat")?.InnerText;
                    concessionari.CodiPostal = concessionariNode.SelectSingleNode("adreça/codiPostal")?.InnerText;
                    concessionari.Telefon = concessionariNode.SelectSingleNode("telefon")?.InnerText;
                    concessionari.Dilluns = concessionariNode.SelectSingleNode("horari/dilluns")?.InnerText;
                    concessionari.Dimarts = concessionariNode.SelectSingleNode("horari/dimarts")?.InnerText;
                    concessionari.Dimecres = concessionariNode.SelectSingleNode("horari/dimecres")?.InnerText;
                    concessionari.Dijous = concessionariNode.SelectSingleNode("horari/dijous")?.InnerText;
                    concessionari.Divendres = concessionariNode.SelectSingleNode("horari/divendres")?.InnerText;
                    concessionari.Dissabte = concessionariNode.SelectSingleNode("horari/dissabte")?.InnerText;
                    concessionari.Diumenge = concessionariNode.SelectSingleNode("horari/diumenge")?.InnerText;

                    XmlNode cotxesNode = concessionariNode.SelectSingleNode("cotxes");
                    if (cotxesNode != null)
                    {
                        foreach (XmlNode cotxeNode in cotxesNode.SelectNodes("cotxe"))
                        {
                            CotxesModel cotxe = new CotxesModel();
                            cotxe.Marca = cotxeNode.Attributes["marca"]?.Value;
                            cotxe.Model = cotxeNode.Attributes["model"]?.Value;
                            cotxe.Any = int.Parse(cotxeNode.Attributes["any"]?.Value ?? "0");

                            foreach (XmlNode caracteristicaNode in cotxeNode.SelectNodes("caracteristica"))
                            {
                                string tipus = caracteristicaNode.Attributes["tipus"]?.Value;
                                string valor = caracteristicaNode.InnerText;
                                cotxe.Caracteristiques.Add(new CaracteristicaModel { Tipus = tipus, Valor = valor });
                            }

                            int idCotxe = BD.InsertarCotxe(cotxe);
                            if (idCotxe != -1)
                            {
                                foreach (var caracteristica in cotxe.Caracteristiques)
                                {
                                    BD.InsertarCaracteristica(caracteristica, idCotxe);
                                }
                            }
                        }
                    }

                    BD.InsertarConcessionari(concessionari);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
