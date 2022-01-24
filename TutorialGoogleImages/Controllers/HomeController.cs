using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.CustomSearchAPI.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TutorialGoogleImages.Models;

namespace TutorialGoogleImages.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AjaxQueryImages(string query)
		{
			const string apiKey = "AIzaSyDupTANwg9Bb9685d5gJCUtz4oCEoW4ukQ";
			const string seId = "5754089c9fca34455";

			var csa = new CustomSearchAPIService(new Google.Apis.Services.BaseClientService.Initializer()
			{
				ApiKey = apiKey,
			});

			var request = csa.Cse.List();
			request.Cx = seId;
			request.Q = query;
			request.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
			var result = request.Execute().Items.ToList();

			return JsonData(result.Select(r => new
			{
				r.Link,
				r.Image.ThumbnailLink,
			}));
		}

		public ActionResult JsonData(object data) => Content(JsonConvert.SerializeObject(data), "application/json");
	}
}
