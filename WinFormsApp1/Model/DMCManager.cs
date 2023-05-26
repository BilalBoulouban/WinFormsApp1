using System;
using System.Collections.Generic;
using System.Xml;

namespace WinFormsApp1.Model
{
    public static class DMCManager
    {
       
            private static List<CotxesModel> Campeonat = new List<CotxesModel>();

            public static bool CarregarModel(string filePath)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filePath);
                    XmlNodeList ElementsCotxes = doc.SelectNodes("//cotxe");
                    foreach (XmlNode cotxe in ElementsCotxes)
                    {
                        CotxesModel Championship = new CotxesModel();
                        Championship.Any = int.Parse(cotxe.Attributes["any"].Value);
                        Championship.Marca = cotxe.Attributes["marca"].Value;
                        Championship.Model = cotxe.Attributes["model"].Value;

                        XmlNodeList ElementsCaracteristiques = cotxe.SelectNodes("caracteristica");
                        foreach (XmlNode caracteristica in ElementsCaracteristiques)
                        {
                            string tipus = caracteristica.Attributes["tipus"].Value;
                            string valor = caracteristica.InnerText;
                            Championship.Caracteristiques.Add(new CaracteristicaModel { Tipus = tipus, Valor = valor });
                        }

                        Campeonat.Add(Championship);
                    }

                    EnviarDades();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cargar el modelo: " + ex.Message);
                    return false;
                }
            }

            public static bool EnviarDades()
            {
                return BD.EnviarDatosBDD(Campeonat);
            }
        }
    }
