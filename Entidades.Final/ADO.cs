using Entidades.Final;
using System.Data.SqlClient;

namespace WinFormsApp
{
    public delegate void ApellidoUsuarioExistenteDelegado(object sender, EventArgs e);
    public class ADO
    {
        private static string conexion = "Server=DESKTOP-8A3M14H; Database=laboratorio2; Trusted_Connection=True; TrustServerCertificate=True;";

        public event ApellidoUsuarioExistenteDelegado? ApellidoUsuarioExistente;
        public bool Agregar(Usuario user)
        {
            List<Usuario> users = ADO.ObtenerTodos(user.Apellido);
            if (users.Count > 0)
            {
                users.Add(user);
                Utils_EventArg e = new Utils_EventArg(users);
                ApellidoUsuarioExistente?.Invoke(this, e);
            }
            try
            {
                using (SqlConnection connect = new SqlConnection(conexion))
                {
                    connect.Open();
                    string query = "INSERT INTO usuarios (nombre, apellido, dni, correo, clave) VALUES (@nombre, @apellido, @dni, @correo, @clave)";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("nombre", user.Nombre);
                    cmd.Parameters.AddWithValue("apellido", user.Apellido);
                    cmd.Parameters.AddWithValue("dni", user.Dni);
                    cmd.Parameters.AddWithValue("correo", user.Correo);
                    cmd.Parameters.AddWithValue("clave", user.Clave);
                    int filasAfectada = cmd.ExecuteNonQuery();
                    return filasAfectada > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar(Usuario user)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conexion))
                {
                    connect.Open();
                    string query = "DELETE FROM usuarios WHERE dni = @dni";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("dni", user.Dni);
                    int filasAfectada = cmd.ExecuteNonQuery();
                    return filasAfectada > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Modificar(Usuario user)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conexion))
                {
                    connect.Open();
                    string query = "UPDATE usuarios SET nombre = @nombre, apellido = @apellido, correo = @correo, clave = @clave WHERE dni = @dni";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("nombre", user.Nombre);
                    cmd.Parameters.AddWithValue("apellido", user.Apellido);
                    cmd.Parameters.AddWithValue("dni", user.Dni);
                    cmd.Parameters.AddWithValue("correo", user.Correo);
                    cmd.Parameters.AddWithValue("clave", user.Clave);
                    int filasAfectada = cmd.ExecuteNonQuery();
                    return filasAfectada > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<Usuario> ObtenerTodos() 
        {
            List<Usuario> user = new List<Usuario>();
            try
            {
                using (SqlConnection connect = new SqlConnection(conexion))
                {
                    connect.Open();
                    string query = "SELECT * FROM usuarios";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.Add(new Usuario(reader.GetString(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetString(3)));
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        public static List<Usuario> ObtenerTodos(string apellidoUsuario)
        {
            List<Usuario> user = new List<Usuario>();
            try
            {
                using (SqlConnection connect = new SqlConnection(conexion))
                {
                    connect.Open();
                    string query = "SELECT * FROM usuarios WHERE apellido = @apellidoUsuario";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("apellidoUsuario", apellidoUsuario);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.Add(new Usuario(reader.GetString(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetString(3)));
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                return user;
            }
        }
    }
}