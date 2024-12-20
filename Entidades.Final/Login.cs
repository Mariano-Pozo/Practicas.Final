using System.Data.SqlClient;
namespace Entidades.Final
{
    public class Login
    {
        private string email;
        private string pass;

        protected string Pass { get => pass; }
        protected string Email { get => email; }

        public Login(string email, string pass)
        {
            this.email = email;
            this.pass = pass;
        }

        public bool Loguear()
        {
            string conexion = "Server=DESKTOP-8A3M14H; Database=laboratorio2; Trusted_Connection=True; TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connect = new SqlConnection(conexion))
                {
                    connect.Open();
                    string query = "SELECT * FROM usuarios WHERE correo = @email AND clave = @pass";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("pass", pass);
                    SqlDataReader sqlData = cmd.ExecuteReader();
                    if (sqlData.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
