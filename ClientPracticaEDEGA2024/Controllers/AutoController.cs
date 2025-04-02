using ClientPracticaEDEGA2024.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientPracticaEDEGA2024.Controllers
{
    public class AutoController : Controller
    {
        public IActionResult GetAll()
        {
            AutoModel auto = new AutoModel();
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44383/api/Auto/");

                    var responseTask = client.GetAsync("GetAll");
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<AutoModel>>();
                        readTask.Wait();
                        var listAutos = readTask.Result;

                        auto.Autos = new List<object>();
                        foreach (var a in listAutos)
                        {
                            auto.Autos.Add(a);
                        }
                    }
                    else
                    {
                        ViewBag.Error = "No se encontro información";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un error " + ex.Message;
            }

            return View(auto);

        }

        [HttpGet]
        public ActionResult Formulario(int? IdAuto)
        {
            AutoModel auto = new AutoModel();

            try
            {

                if (IdAuto != null)
                {

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44383/api/Auto/");

                        var responseTask = client.GetAsync($"GetById/{IdAuto}");
                        responseTask.Wait();
                        var result = responseTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<AutoModel>();
                            readTask.Wait();
                            var listAutos = readTask.Result;

                            auto = listAutos;
                        }
                        else
                        {
                            ViewBag.Error = "No se encontro información";
                        }
                    }
                }
                
            }

            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un error " + ex.Message;
            }
            return View(auto);
        }

    }
}
