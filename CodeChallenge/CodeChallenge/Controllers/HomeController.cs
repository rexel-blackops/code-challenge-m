using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Controllers
{
    public class HomeController : Controller
    {
        private TerminalService _terminalService;
        private readonly string _currentPageCookieName = "codechallenge_currentpage";

        public HomeController(TerminalService terminalService)
        {
            _terminalService = terminalService;
        }

        /// <summary>
        /// The page will attempt to skip to the "current page" unless the user explicitly set h querystring to true
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public IActionResult Index(bool? h)
        {
            // Note from RexGex: These are logic for book marking the task page, so that you don't have to keep clicking everytime you reload the site.
            // **DO NOT modify any code in Index controller action.**
            var currentPage = HttpContext.Request.Cookies[_currentPageCookieName];

            if (h.HasValue && h == true)
            {
                HttpContext.Response.Cookies.Append(_currentPageCookieName, "home");
                return View();
            }

            switch (currentPage)
            {
                case "1":
                    return RedirectToAction("Task1", "Home");
                case "2":
                    return RedirectToAction("Task2", "Home");
                case "3":
                    return RedirectToAction("Task3", "Home");
                case "4":
                    return RedirectToAction("Task4", "Home");
                case "complete":
                    return RedirectToAction("MissionComplete", "Home");
                default:
                    return View();
            }
        }

        public async Task<IActionResult> Task1()
        {
            HttpContext.Response.Cookies.Append(_currentPageCookieName, "1");

            // #TODO: Assign your secret token provided by the original email on the variable below
            var secretToken = "";

            // You must assign a valid secretToken above!
            if (string.IsNullOrEmpty(secretToken))
            {
                return View();
            }

            // Retrieve the API key from the mole in form of JSON string.
            var jsonResponse = await _terminalService.RetrieveApiKey(secretToken);

            // #TODO: The API Key from the response seems to return empty value. You will need to debug and find out why.
            var viewModel = JsonConvert.DeserializeObject<Task1GetApiKeyResponse>(jsonResponse);

            return View("Task1", viewModel.ApiKey);
        }

        public async Task<IActionResult> Task2()
        {
            HttpContext.Response.Cookies.Append(_currentPageCookieName, "2");

            // #TODO: Assign the API key obtain from task #1 here.
            var apiKey = "";

            if (string.IsNullOrEmpty(apiKey))
            {
                return View();
            }

            var assetList = await _terminalService.RetrieveAssetList(apiKey);

            // #TODO: Redo the view model property values assignment below, so that the lists actually match the criteria
            var viewModel = new Task2ViewModel
            {
                Top4MostQuantityAssets = assetList,
                Top4MostValuableAssets = assetList,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Task3()
        {
            HttpContext.Response.Cookies.Append(_currentPageCookieName, "3");

            // Note from RexGex: for now, I'm just gonna put a blanket try-catch statement for this task, so the page will at least render.
            // #TODO: Remove the try-catch statement once the task is completed.
            try
            {
                var apiKey = "";

                // #TODO: Retrieve the list of asset (use the same service call form Task 2)

                // #TODO: Find an asset where both Value and Quantity are NULL

                // #TODO: Assign the asset Id here
                long? assetIdWithNulls = null;

                // Retrieve the history log. Note that the log entries may not be sorted.
                var response = await _terminalService.RetrieveAssetHistoryLog(apiKey, assetIdWithNulls);
                var historyLog = JsonConvert.DeserializeObject<IReadOnlyList<Task3GetAssetHistoryLogResponse>>(response);

                // #TODO: Find the last user that made changes to this asset based on history log

                // #TODO: Assign UserId, first name, last name of that user onto the view model below.
                return View(new Task3ViewModel
                {
                    UserId = null,
                    UserFirstName = "",
                    UserLastName = ""
                });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Task4()
        {
            HttpContext.Response.Cookies.Append(_currentPageCookieName, "4");

            // Note from RexGex: for now, I'm just gonna put a blanket try-catch statement for this task, so the page will at least render.
            // #TODO: Remove the try-catch statement once the task is completed.
            try
            {
                // #TODO: Put an access code provided by the terminal's task 3 validator here
                var acccessCode = "";

                // #TODO: Assign the perpetrator key in format of {userId}-{userFirstName}-{userLastName} here (without curly braces)
                var userKey = "";

                var response = await _terminalService.RetrieveSuspectInfo(acccessCode, userKey);

                return View(new Task4ViewModel
                {
                    HiredDate = response.HiredDate,
                    LastUserLocationLat = response.LastUserLocationLat,
                    LastUserLocationLon = response.LastUserLocationLon,
                    UserDepartmentCode = response.DepartmentCode,
                    UserFirstName = response.FirstName,
                    UserLastName = response.LastName,
                    UserId = response.UserId,
                    UserImageUrl = response.ImageUrl,
                    UserJobTitle = response.JobTitle
                });
            }
            catch
            {
                return View();
            }
        }

        public IActionResult MissionComplete()
        {
            HttpContext.Response.Cookies.Append(_currentPageCookieName, "complete");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
