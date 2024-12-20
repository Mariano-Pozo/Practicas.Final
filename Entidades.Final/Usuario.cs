using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Final
{
    public class Usuario
    {
        private string apellido;
        private string clave;
        private string correo;
        private int dni;
        private string nombre;

        public string Apellido { get => apellido; set => apellido = value; }
        public string Clave { get => clave; }
        public string Correo { get => correo; set => correo = value; }
        public int Dni { get => dni; set => dni = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public Usuario() { }

        public Usuario(string nombre, string apellido, int dni, string correo)
        {
            this.apellido = apellido;
            this.correo = correo;
            this.dni = dni;
            this.nombre = nombre;
        }
        public Usuario(string nombre, string apellido, int dni, string correo, string clave) : this(nombre, apellido, dni, correo)
        {
            this.clave = clave;
        }

        public override string ToString()
        {
            return $"{nombre} {apellido} {correo} {dni}";
        }
    }
}
