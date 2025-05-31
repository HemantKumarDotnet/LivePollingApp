using Microsoft.Data.SqlClient;
using System.Data;

namespace LivePollingApp.Models
{
    public class Polling
    {
        private readonly string _connectionString;

        public Polling()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public DataTable SubmitPollingDate(PollingDto model)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("prc_submitPollingDate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PollingDate",Validator.ConvertToDateOnly(model.PollingDate));
                    cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
    public class PollingDto
    {
        public string PollingDate  { get; set; }
        public bool IsActive { get; set; }
    }
}
