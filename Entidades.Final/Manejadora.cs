using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entidades.Final
{
    public static class Manejadora
    {
        public static bool DeserializarJSON(string path, out List<Usuario> users) 
        {
            try
            {
                string json = File.ReadAllText(path);
                users = JsonSerializer.Deserialize<List<Usuario>>(json);
                return true;
            }
            catch (Exception ex)
            {
                users = new List<Usuario>();
                return false;
            }
        }
        public static bool EscribirArchivo(List<Usuario> users) 
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "usuarios.log");
            DateTime dateTime = DateTime.Now;
            string fecha = dateTime.ToString("dd/MMMM/yyyy HH:mm:ss");
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"fecha y hora: {fecha}");
                    sw.WriteLine($"apellido: {users[0].Apellido}");
                    sw.WriteLine($"correos");
                    foreach (Usuario usuario in users)
                    {
                        sw.WriteLine(usuario.Correo);
                    }

                    sw.WriteLine($"* * ** * ** ****");
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool SerializarJSON(List<Usuario> users, string path) 
        {
            try
            {
                string json = JsonSerializer.Serialize(users);
                File.WriteAllText(path, json);
                return true;
            }
            catch { return false; }
        }
    }
}
