using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Final
{
    public class Utils_EventArg : EventArgs
    {
        public List<Usuario> usuariosRepetidos; //{ get; set; }
        public Utils_EventArg(List<Usuario> UsuariosRepetidos)
        {
            this.usuariosRepetidos = UsuariosRepetidos;
        }
    }
}
