using CRUD_MVVM.Firebase;
using Firebase.Database.Query;
using CRUD_MVVM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Firebase.Database;
using System.Diagnostics;
using System.Linq;

namespace CRUD_MVVM.Services
{
    public class PersonaServices
    {
        public async Task<bool> InsertarPersona(Persona persona)
        {
            bool response = false;
            try
            {
                await Conexion.firebase
                .Child("Persona")
                .PostAsync(new Persona()
                {
                    Nombre = persona.Nombre,
                    Apellidos = persona.Apellidos,
                    Edad = persona.Edad,
                    Direccion = persona.Direccion,
                    Puesto = persona.Puesto,
                    Foto = persona.Foto
                });
                response = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response = false;
            }
            return response;
        }

        public async Task<List<Persona>> ListarPersonas()
        {
            try
            {
                var data = (await Conexion.firebase
                            .Child("Persona")
                            .OnceAsync<Persona>()).Select(item => new Persona
                            {
                                Key = item.Key, // This is the ID
                                Nombre = item.Object.Nombre,
                                Apellidos = item.Object.Apellidos,
                                Direccion = item.Object.Direccion,
                                Edad = item.Object.Edad,
                                Puesto = item.Object.Puesto,
                                Foto = item.Object.Foto,  
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<bool> DeletePerson(string key)
        {
            bool response = false;
            try
            {
                await Conexion.firebase.Child("Persona").Child(key).DeleteAsync();
                response = true;
            }
            catch (Exception ex)
            {
                response = false;
                Debug.WriteLine(ex.Message);
            }
            return response;
        }

        public async Task<bool> UpdatePerson(Persona persona, string key)
        {
            bool response = false;
            try
            {
                await Conexion.firebase
                              .Child("Persona")
                              .Child(key)
                              .PutAsync(persona);
                response = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response = false;
            }
            return response;
        }



    }
}
