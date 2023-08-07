using Dapper;
using Microsoft.AspNetCore.Mvc;
using RegistroDeSinistro.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RegistroDeSinistro.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<Sinistro> model = ListarSinistros();
            return View(model);
        }

        private List<Sinistro> ListarSinistros()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings").GetSection("MainConnection").Value;
            List<Sinistro> sinistros = new List<Sinistro>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Sinistro";
                    sinistros = con.Query<Sinistro>(query).ToList();
                }
                catch (Exception ex)
                {
#pragma warning disable CA2200 // Gerar novamente para preservar detalhes da pilha
#pragma warning restore CA2200 // Gerar novamente para preservar detalhes da pilha
                }
                finally
                {
                    con.Close();
                }
            }
            return sinistros;
        }

        public IActionResult AddSinistro()
        {
            Sinistro model = new Sinistro();
            return View(model);
        }
        public IActionResult EditSinistro(int id)
        {
            Sinistro model = ListarSinistro(id);
            return View(model);
        }
        private Sinistro ListarSinistro(int id)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings").GetSection("MainConnection").Value;
            Sinistro sinistros = new Sinistro();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Sinistro WHERE Id = " + id;
                    sinistros = con.Query<Sinistro>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return sinistros;
        }
        [HttpPost]
        public IActionResult RegistrarSinistro(Sinistro model)
        { 
            RegistraSinistro(model);
            return RedirectToAction("Index");
        }
        private void RegistraSinistro(Sinistro model)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings").GetSection("MainConnection").Value;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO Sinistro (Data, Tipo, Numero, Rua, Bairro, Cidade, Estado, Pais) VALUES(@Data, @Tipo, @Numero, @Rua, @Bairro, @Cidade, @Estado, @Pais)";
                    con.Execute(query,model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        [HttpPost]
        public IActionResult EditarSinistro(Sinistro model)
        {
            EditaSinistro(model);
            return RedirectToAction("Index");
        }
        private void EditaSinistro(Sinistro model)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings").GetSection("MainConnection").Value;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "UPDATE Sinistro SET Data=@Data, Tipo=@Tipo, Numero=@Numero, Rua=@Rua, Bairro=@Bairro, Cidade=@Cidade, Estado=@Estado, Pais=@Pais WHERE Id ="+model.Id;
                    con.Execute(query, model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public IActionResult DeleteSinistro(int id)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings").GetSection("MainConnection").Value;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM Sinistro WHERE Id = " + id;
                    con.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}