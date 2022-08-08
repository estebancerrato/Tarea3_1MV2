using CRUD_MVVM.Models;
using CRUD_MVVM.Services;
using CRUD_MVVM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CRUD_MVVM.ViewModels
{
    public class ListarAlumnosViewModel : BaseViewModels
    {

        private List<Persona> _ListaPersonas;
        PersonaServices personaServices;

        public List<Persona> ListaPersonas
        {
            get { return _ListaPersonas; }
            set { 
                _ListaPersonas = value; 
                OnPropertyChanged(); 
            }
        }

        public ListarAlumnosViewModel() {
            personaServices = new PersonaServices();

            EditarPersonaCommand = new Command<Persona>(async (Persona) => await EditarPersona(Persona));
            EliminarPersonaCommand = new Command<Persona>(async (Persona) => await EliminarPersona(Persona));
        }

        private async Task EliminarPersona(Persona persona)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Advertencia", "¿Esta seguro de eliminar a " + persona.Nombre + "?", "Si", "No");

            if (confirm)
            {
                bool response = await personaServices.DeletePerson(persona.Key);
                if (response)
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Persona Eliminada Correctamente", "Ok");
                    CargarDatos();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Se produjo un error al eliminar la persona", "Ok");
                }
            }
        }

        private async Task EditarPersona(Persona persona)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AgregarAlumnoView("Editar", persona));
        }

        public async void CargarDatos()
        {
            ListaPersonas = await personaServices.ListarPersonas();
            if (ListaPersonas.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "No hay personas registradas", "Ok");
            }
        }

        public ICommand EditarPersonaCommand { get; set; }
        public ICommand EliminarPersonaCommand { get; set; }
    }
}
