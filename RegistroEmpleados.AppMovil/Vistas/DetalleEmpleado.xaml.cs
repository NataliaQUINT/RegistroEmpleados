using RegistroEmpleados.Modelos.Modelos;

namespace RegistroEmpleados.AppMovil.Vistas;

public partial class DetalleEmpleado : ContentPage
{
    public DetalleEmpleado(Empleado empleado)
    {
        InitializeComponent();
        BindingContext = empleado;
    }
}