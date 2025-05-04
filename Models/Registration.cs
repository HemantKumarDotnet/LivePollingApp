using Microsoft.Data.SqlClient;
using System.Data;

namespace LivePollingApp.Models
{
    public class Registration
    {
        private readonly string _connectionString;
        
        public Registration()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public DataTable UserRegistration(RegisterDto model)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("prc_userRegistration", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
                    cmd.Parameters.AddWithValue("@Role", model.Role);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", Validator.EncryptPassword(model.Password));
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
    public class RegisterDto
    {
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}
