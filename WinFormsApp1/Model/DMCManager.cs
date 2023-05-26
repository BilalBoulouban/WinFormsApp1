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

                XmlNodeList concessionaris = doc.SelectNodes("//concessionari");
                foreach (XmlNode concessionari in concessionaris)
                {
                    XmlNodeList cotxes = concessionari.SelectNodes("cotxes/cotxe");
                    foreach (XmlNode cotxeNode in cotxes)
                    {
                        CotxesModel cotxe = new CotxesModel();
                        cotxe.Marca = cotxeNode.Attributes["marca"].Value;
                        cotxe.Model = cotxeNode.Attributes["model"].Value;
                        cotxe.Any = int.Parse(cotxeNode.Attributes["any"].Value);

                        XmlNodeList caracteristiques = cotxeNode.SelectNodes("caracteristica");
                        foreach (XmlNode caracteristicaNode in caracteristiques)
                        {
                            string tipus = caracteristicaNode.Attributes["tipus"].Value;
                            string valor = caracteristicaNode.InnerText;
                            cotxe.Caracteristiques.Add(new CaracteristicaModel { Tipus = tipus, Valor = valor });
                        }

                        BD.InsertarCotxe(cotxe);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el modelo: " + ex.Message);
                return false;
            }
        }
    }
}
