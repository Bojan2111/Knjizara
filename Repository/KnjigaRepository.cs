using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Knjizara.Models;
using Knjizara.Repository.Interfaces;

namespace Knjizara.Repository
{
    public class KnjigaRepository : IKnjigaRepository
    {
        public IConfiguration Configuration;
        public KnjigaRepository(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public void Create(KnjigaModel knjiga)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "insert into knjige(naziv_knjige, cena, zanr_id) values (@Naziv, @Cena, @ZanrId)";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Naziv", knjiga.Naziv);
            command.Parameters.AddWithValue("Cena", knjiga.Cena);
            command.Parameters.AddWithValue("ZanrId", knjiga.Zanr.Id);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public bool CheckIfKnjigaExists(string nazivKnjige)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "IF EXISTS ( SELECT 1 FROM knjige WHERE naziv_knjige = @nazivKnjige) BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nazivKnjige", nazivKnjige);
                    int result = (int)command.ExecuteScalar();
                    return result == 1;
                }
            }
        }

        public void Archive(int id)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);

            // Virtualno brisanje preko polja 'izbrisana' u bazi. Podatak ostaje snimljen, ali se ne prikazuje
            string query = "update knjige set izbrisana=1 where id_knjige=@Id";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", id);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete(int id)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);

            // Stvarno brisanje iz baze podataka
            string query = "delete from knjige where id_knjige = @Id";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", id);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<KnjigaModel> GetAll()
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "select * from knjige join zanrovi on knjige.zanr_id = zanrovi.id_zanra where knjige.izbrisana=0;";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            DataTable dt = new DataTable("knjige");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            List<KnjigaModel> knjige = new List<KnjigaModel>();
            foreach (DataRow dr in dt.Rows)
            {
                KnjigaModel k = new KnjigaModel();
                k.Id = int.Parse(dr["id_knjige"].ToString());
                k.Naziv = dr["naziv_knjige"].ToString();
                k.Cena = double.Parse(dr["cena"].ToString());
                k.Izbrisana = bool.Parse(dr["izbrisana"].ToString());
                k.Zanr = new ZanrModel();
                k.Zanr.Id = int.Parse(dr["id_zanra"].ToString());
                k.Zanr.NazivZanra = dr["naziv_zanra"].ToString();
                knjige.Add(k);
            }

            connection.Close();

            return knjige;
        }

        public List<KnjigaModel> GetAllDeleted()
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "select * from knjige join zanrovi on knjige.zanr_id = zanrovi.id_zanra where knjige.izbrisana = 1;";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            DataTable dt = new DataTable("knjige");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            List<KnjigaModel> knjige = new List<KnjigaModel>();
            foreach (DataRow dr in dt.Rows)
            {
                KnjigaModel k = new KnjigaModel();
                k.Id = int.Parse(dr["id_knjige"].ToString());
                k.Naziv = dr["naziv_knjige"].ToString();
                k.Cena = double.Parse(dr["cena"].ToString());
                k.Izbrisana = bool.Parse(dr["izbrisana"].ToString());
                k.Zanr = new ZanrModel();
                k.Zanr.Id = int.Parse(dr["id_zanra"].ToString());
                k.Zanr.NazivZanra = dr["naziv_zanra"].ToString();
                knjige.Add(k);
            }

            connection.Close();

            return knjige;
        }

        public KnjigaModel GetOne(int id)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "select * from knjige join zanrovi on knjige.zanr_id = zanrovi.id_zanra where knjige.id_knjige = @Id;";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", id);

            DataTable dt = new DataTable("knjizara");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            KnjigaModel k = new KnjigaModel();
            k.Zanr = new ZanrModel();
            foreach (DataRow dr in dt.Rows)
            {
                k.Id = int.Parse(dr["id_knjige"].ToString());
                k.Naziv = dr["naziv_knjige"].ToString();
                k.Cena = double.Parse(dr["cena"].ToString());
                k.Izbrisana = bool.Parse(dr["izbrisana"].ToString());
                k.Zanr.Id = int.Parse(dr["id_zanra"].ToString());
                k.Zanr.NazivZanra = dr["naziv_zanra"].ToString();
            }

            connection.Close();

            return k;
        }

        public List<KnjigaModel> Sort(int deleted, string field, string sorting)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);

            string query = $"select * from knjige join zanrovi on knjige.zanr_id = zanrovi.id_zanra where knjige.izbrisana={deleted} order by knjige.{field} {sorting};";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            DataTable dt = new DataTable("knjige");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            List<KnjigaModel> knjige = new List<KnjigaModel>();
            foreach (DataRow dr in dt.Rows)
            {
                KnjigaModel k = new KnjigaModel();
                k.Id = int.Parse(dr["id_knjige"].ToString());
                k.Naziv = dr["naziv_knjige"].ToString();
                k.Cena = double.Parse(dr["cena"].ToString());
                k.Izbrisana = bool.Parse(dr["izbrisana"].ToString());
                k.Zanr = new ZanrModel();
                k.Zanr.Id = int.Parse(dr["id_zanra"].ToString());
                k.Zanr.NazivZanra = dr["naziv_zanra"].ToString();
                knjige.Add(k);
            }

            connection.Close();

            return knjige;
        }

        public void Update(KnjigaModel knjiga)
        {
            string connectionString = this.Configuration.GetConnectionString("Knjizara");
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "update knjige set naziv_knjige=@Naziv, cena=@Cena, izbrisana=@Izbrisana, zanr_id=@ZanrId where knjige.id_knjige=@Id;";

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", knjiga.Id);
            command.Parameters.AddWithValue("Naziv", knjiga.Naziv);
            command.Parameters.AddWithValue("Cena", knjiga.Cena);
            command.Parameters.AddWithValue("Izbrisana", knjiga.Izbrisana);
            command.Parameters.AddWithValue("ZanrId", knjiga.Zanr.Id);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
