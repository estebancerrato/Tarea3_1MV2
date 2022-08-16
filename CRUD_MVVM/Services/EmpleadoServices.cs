using Tarea3_1MV2.Firebase;
using Firebase.Database.Query;
using Tarea3_1MV2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Firebase.Database;
using System.Diagnostics;
using System.Linq;

namespace Tarea3_1MV2.Services
{
    public class EmpleadoServices
    {
        public async Task<bool> GuardarEmpleado(Empleado empleado)
        {
            bool response = false;
            try
            {
                await Conexion.firebase
                .Child("Empleado")
                .PostAsync(new Empleado()
                {
                    Nombre = empleado.Nombre,
                    Apellidos = empleado.Apellidos,
                    Edad = empleado.Edad,
                    Direccion = empleado.Direccion,
                    Puesto = empleado.Puesto,
                    Foto = empleado.Foto
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

        public async Task<List<Empleado>> ListarEmpleados()
        {
            try
            {
                var data = (await Conexion.firebase
                            .Child("Empleado")
                            .OnceAsync<Empleado>()).Select(item => new Empleado
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

        public async Task<bool> EliminarEmpleado(string key)
        {
            bool response = false;
            try
            {
                await Conexion.firebase.Child("Empleado").Child(key).DeleteAsync();
                response = true;
            }
            catch (Exception ex)
            {
                response = false;
                Debug.WriteLine(ex.Message);
            }
            return response;
        }

        public async Task<bool> ActualizarEmpleado(Empleado empleado, string id)
        {
            bool response = false;
            try
            {
                await Conexion.firebase
                              .Child("Empleado")
                              .Child(id)
                              .PutAsync(empleado);
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
