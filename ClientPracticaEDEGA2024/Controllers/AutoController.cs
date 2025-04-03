using ClientPracticaEDEGA2024.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;

namespace ClientPracticaEDEGA2024.Controllers
{
    public class AutoController : Controller
    {
        public ActionResult GetAll()
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
        public ActionResult Formulario(int? au_id)
        {
            AutoModel auto = new AutoModel();

            try
            {
                if (au_id != null)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44383/api/Auto/");

                        var responseTask = client.GetAsync($"GetById?au_id={au_id}");
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

        [HttpPost]
        public ActionResult Formulario(AutoModel auto)
        {


            auto.Autos = new List<object>();
            auto.su_nombre = "";
            auto.mo_nombre = "";
            auto.ma_nombre = "";
            auto.au_estado = "";
            auto.au_id = 0;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44383/api/Auto/");
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                    var responseTask = client.PostAsJsonAsync("Add", auto);
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
                        ViewBag.Error = "No se encontro información" + result.StatusCode;
                    }
                }


            }

            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un error " + ex.Message;
            }
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Delete(int au_id)
        {
            
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44383/api/Auto/");

                    var responseTask = client.DeleteAsync($"Delete/Delete?au_id={au_id}");
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
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

            return RedirectToAction("GetAll");

        }

    }
}
