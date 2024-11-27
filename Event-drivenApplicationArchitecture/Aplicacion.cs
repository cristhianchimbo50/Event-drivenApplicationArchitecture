using System;
using System.Windows.Forms;

namespace Event_drivenApplicationArchitecture
{
    public partial class Aplicacion : Form
    {
        private GestorTareas gestorTareas;

        public Aplicacion()
        {
            InitializeComponent();
            gestorTareas = new GestorTareas();
            gestorTareas.TareasActualizadas += GestorTareas_TareasActualizadas;
            ActualizarListaTareas();
        }

        private void GestorTareas_TareasActualizadas(object sender, EventArgs e)
        {
            ActualizarListaTareas();
        }

        private void ActualizarListaTareas()
        {
            listBoxTareas.DataSource = null;
            listBoxTareas.DataSource = gestorTareas.Tareas;
            listBoxTareas.DisplayMember = "Descripcion";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxDescripcion.Text))
            {
                    try
                    {
                        int nextId = gestorTareas.Tareas.Count > 0 ? gestorTareas.Tareas[gestorTareas.Tareas.Count - 1].Id + 1 : 1;
                        var nuevaTarea = new Tarea
                        {
                            Id = nextId,
                            Descripcion = textBoxDescripcion.Text,
                            Completada = false
                        };
                        gestorTareas.AgregarTarea(nuevaTarea);
                        textBoxDescripcion.Clear();
                        MessageBox.Show("Tarea agregada exitosamente.", "Tarea Agregada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al agregar tarea: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            else
            {
                MessageBox.Show("Por favor ingrese una descripción para la tarea.", "Descripción Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listBoxTareas.SelectedItem is Tarea tareaSeleccionada)
            {
                if (MessageBox.Show("¿Está seguro de que desea eliminar esta tarea?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        gestorTareas.EliminarTarea(tareaSeleccionada.Id);
                        MessageBox.Show("Tarea eliminada exitosamente.", "Tarea Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar tarea: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una tarea para eliminar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
