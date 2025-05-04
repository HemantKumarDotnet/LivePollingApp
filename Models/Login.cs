using Microsoft.Data.SqlClient;
using System.Data;

namespace LivePollingApp.Models
{
    public class Login
    {
        private readonly string _connectionString;

        public Login()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DataTable userLogin(Login model)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("prc_userLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Password", Validator.EncryptPassword(model.Password));
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
