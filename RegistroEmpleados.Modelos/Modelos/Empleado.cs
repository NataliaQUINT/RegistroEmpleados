using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpleados.Modelos.Modelos
{
    public class Empleado
    {
        public string? Id { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? CorreoElectronico { get; set; }
        public int Sueldo { get; set; }
        public DateTime FechaInicio { get; set; }
        public Cargo? Cargo { get; set; }
        public bool? Estado { get; set; }
        public string NombreCompleto => $"{PrimerNombre} {PrimerApellido}";
        public string EstadoTexto => Estado.HasValue? (Estado.Value ? "Activo" : "Inactivo"): ("Estado Desconocido");//operador ternario(HasValue)(if y else) en una sola linea)
                

    }
}       
        
        