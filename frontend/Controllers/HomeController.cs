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
		public async Task<IActionResult> Tree()
		{
			var response = await _httpClient.GetAsync("Empleados");
			if (response.IsSuccessStatusCode)
			{
				try
				{
					var json = await response.Content.ReadAsStringAsync();
					var empleadosAux = JsonSerializer.Deserialize<List<Empleado>>(json, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true 
					});
					if (empleadosAux == null)
					{
						empleadosAux = new List<Empleado>();
					}
					return View(empleadosAux);
				}
				catch (JsonException ex)
				{
					return View(new List<Empleado>());
				}
			}
			else
			{
				return View(new List<Empleado>());
			}
		}
		public async Task<IActionResult> Delete()
		{
			var response = await _httpClient.GetAsync("Empleados");
			if (response.IsSuccessStatusCode)
			{
				try
				{
					var json = await response.Content.ReadAsStringAsync();
					var empleados = JsonSerializer.Deserialize<List<Empleado>>(json, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});
					return View(empleados ?? new List<Empleado>());
				}
				catch (JsonException ex)
				{
					return View(new List<Empleado>());
				}
			}
			return View(new List<Empleado>());
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int codigo)
		{
			var response = await _httpClient.DeleteAsync($"Empleados/{codigo}");
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Delete");
			}
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
			empleado.CodigoJefe = empleado.CodigoJefe == 0? null: empleado.CodigoJefe;
			var json = JsonSerializer.Serialize(empleado);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("Empleados", content);

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