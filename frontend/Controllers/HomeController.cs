using System.Diagnostics;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace frontend.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			_logger = logger;
			_httpClient = httpClientFactory.CreateClient();
			_configuration = configuration;
			_httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]); 
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Empleado empleado)
		{
			if (!ModelState.IsValid)
			{
				return View(empleado);
			}

			empleado.EstaBorrado = false; 

			var json = JsonSerializer.Serialize(empleado);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("empleados", content);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View(empleado);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}