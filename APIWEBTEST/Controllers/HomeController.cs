using APIWEBTEST.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using System.Net.Http.Headers;

namespace APIWEBTEST.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subir_Archivo(string descripcion, IFormFile documento)
        {
            var client = new HttpClient();//declarar un cliente 
            using (var multipartFormContent = new MultipartFormDataContent())//
            {

                //Add other fields
                multipartFormContent.Add(new StringContent(descripcion), name: "Descripcion");

                //Add the file // System.Net.Http.Headers;
                var fileStreamContent = new StreamContent(documento.OpenReadStream()); //se recibe el documento
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(documento.ContentType); //dice que tipo de documento
                multipartFormContent.Add(fileStreamContent, name: "Archivo", fileName: documento.FileName);

                //Send it
                var response = await client.PostAsync("http://localhost:5121/api/Documento/Subir", multipartFormContent);
                var test = await response.Content.ReadAsStringAsync();
            }

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}