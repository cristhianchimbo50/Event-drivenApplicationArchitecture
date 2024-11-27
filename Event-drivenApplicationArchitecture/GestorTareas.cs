using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_drivenApplicationArchitecture
{
    internal class GestorTareas
    {
        public event EventHandler TareasActualizadas;

        public List<Tarea> Tareas { get; private set; }

        private GestorXML gestorXML;

        public GestorTareas()
        {
            gestorXML = new GestorXML();
            Tareas = gestorXML.CargarTareas();
        }

        public void AgregarTarea(Tarea nuevaTarea)
        {
            Tareas.Add(nuevaTarea);
            gestorXML.GuardarTareas(Tareas);
            TareasActualizadas?.Invoke(this, EventArgs.Empty);
        }

        public void EliminarTarea(int id)
        {
            Tareas.RemoveAll(t => t.Id == id);
            gestorXML.GuardarTareas(Tareas);
            TareasActualizadas?.Invoke(this, EventArgs.Empty);
        }
    }
}
