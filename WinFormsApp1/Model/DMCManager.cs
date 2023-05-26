using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WinFormsApp1.DAO;

namespace WinFormsApp1.Model
{
    public static class DMCManager
    {
        public static bool CarregarModel(string filePath)
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);

                foreach (XElement concessionariElement in doc.Descendants("concessionari"))
                {
                    XElement cotxesElement = concessionariElement.Parent.Element("cotxes");
                    foreach (XElement cotxeElement in cotxesElement.Elements("cotxe"))
                    {
                        CotxesModel cotxe = new CotxesModel();
                        cotxe.Marca = cotxeElement.Attribute("marca").Value;
                        cotxe.Model = cotxeElement.Attribute("model").Value;
                        cotxe.Any = int.Parse(cotxeElement.Attribute("any").Value);

                        foreach (XElement caracteristicaElement in cotxeElement.Elements("caracteristica"))
                        {
                            string tipus = caracteristicaElement.Attribute("tipus").Value;
                            string valor = caracteristicaElement.Value;
                            cotxe.Caracteristiques.Add(new CaracteristicaModel { Tipus = tipus, Valor = valor });
                        }

                        int idCotxe = BD.InsertarCotxe(cotxe);
                        if (idCotxe != -1)
                        {
                            foreach (var caracteristica in cotxe.Caracteristiques)
                            {
                                BD.InsertarCaracteristica(caracteristica, idCotxe);
                            }

                            if (cotxe.Concessionari != null)
                            {
                                BD.InsertarConcessionari(cotxe.Concessionari);
                            }
                        }
                    }
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
