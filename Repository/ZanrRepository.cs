using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Knjizara.Models;
using Knjizara.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Knjizara.Repository
{
    public class ZanrRepository : IZanrRepository
    {
        public IConfiguration Configuration;
        public ZanrRepository(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void Create(ZanrModel zanr)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "insert into zanrovi(naziv_zanra) values (@Naziv)";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Naziv", zanr.NazivZanra);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ZanrModel> GetAll()
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM zanrovi;";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            DataTable dt = new DataTable("zanrovi");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            List<ZanrModel> Zanrovi = new List<ZanrModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ZanrModel z = new ZanrModel();
                z.Id = int.Parse(dr["id_zanra"].ToString());
                z.NazivZanra = dr["naziv_zanra"].ToString();
                Zanrovi.Add(z);
            }

            connection.Close();

            return Zanrovi;
        }

        public ZanrModel GetOne(int Id)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "select * from zanrovi where zanrovi.id_zanra = @Id;";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", Id);

            DataTable dt = new DataTable("knjizara");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            ZanrModel z = new ZanrModel();
            foreach (DataRow dr in dt.Rows)
            {
                z.Id = int.Parse(dr["id_zanra"].ToString());
                z.NazivZanra = dr["naziv_zanra"].ToString();
            }

            connection.Close();

            return z;
        }

        public void Update(ZanrModel zanr)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "update zanrovi set naziv_zanra=@Naziv where zanrovi.id_zanra=@Id;";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", zanr.Id);
            command.Parameters.AddWithValue("Naziv", zanr.NazivZanra);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
