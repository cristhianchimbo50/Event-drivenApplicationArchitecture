using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace Event_drivenApplicationArchitecture
{
    internal class GestorXML
    {
        private string archivoPath = @"C:\Users\chimb\source\repos\Event-drivenApplicationArchitecture\Event-drivenApplicationArchitecture\tareas.xml";

        public List<Tarea> CargarTareas()
        {
            XDocument documento;
            if (File.Exists(archivoPath))
            {
                documento = XDocument.Load(archivoPath);
            }
            else
            {
                documento = new XDocument(new XElement("Tareas"));
                documento.Save(archivoPath);
            }

            return documento.Descendants("Tarea")
                .Select(x => new Tarea
                {
                    Id = (int)x.Element("Id"),
                    Descripcion = (string)x.Element("Descripcion"),
                    Completada = (bool)x.Element("Completada")
                }).ToList();
        }

        public void GuardarTareas(List<Tarea> tareas)
        {
            var documento = new XDocument(
                new XElement("Tareas",
                    tareas.Select(t => new XElement("Tarea",
                        new XElement("Id", t.Id),
                        new XElement("Descripcion", t.Descripcion),
                        new XElement("Completada", t.Completada)
                    ))
                )
            );
            documento.Save(archivoPath);
        }
    }
}
