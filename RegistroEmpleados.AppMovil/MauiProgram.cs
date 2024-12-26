using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;
using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif
            ActualizarCargo();
            ActualizarEmpleado();
            return builder.Build();
        }

        public static async Task ActualizarCargo()
        {
            FirebaseClient client = new FirebaseClient("https://registroempleados1-default-rtdb.firebaseio.com/");

            var cargos = await client.Child("Cargos").OnceAsync<Cargo>(); // recuperar todos los cargos despues de peticion
            //cede cargos
            if (cargos.Count == 0)// va a la bd,si no hay cargos en la base de datos se crean los cargos por defecto 
            {
                await client.Child("Cargos").PostAsync(new Cargo { Nombre = "Administrador", Estado = true });
                await client.Child("Cargos").PostAsync(new Cargo { Nombre = "Supervisor", Estado = true });
                await client.Child("Cargos").PostAsync(new Cargo { Nombre = "Usuario", Estado = true });
            }
            else //si no se cumple condicion anterior (if) se actualizan los cargos
            {
                foreach (var cargo in cargos)
                {
                    if (cargo.Object.Estado == null)//si el estado del cargo es nulo se actualiza a true
                    {
                        var cargoActualizado = cargo.Object;
                        cargoActualizado.Estado = true;

                        await client
                            .Child("Cargos")
                            .Child(cargo.Key)
                            .PutAsync(cargoActualizado);
                    }
                }
            }
        }
        public static async Task ActualizarEmpleado()
        {
            FirebaseClient client = new FirebaseClient("https://registroempleados1-default-rtdb.firebaseio.com/");

            var empleados = await client.Child("Empleados").OnceAsync<Empleado>();

            foreach (var empleado in empleados)
            {
                if (empleado.Object.Estado == null)
                {
                    var empleadoActualizado = empleado.Object;
                    empleadoActualizado.Estado = true;

                    await client
                        .Child("Empleados")
                        .Child(empleado.Key)
                        .PutAsync(empleadoActualizado);
                }
            }
        }
    }
}
