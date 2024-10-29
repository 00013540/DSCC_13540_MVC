using DSCC_13540_MVC.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DSCC_13540_MVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly string BaseUrl = "http://localhost:7137/";

        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            List<AuthorDto> authors = new List<AuthorDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/author/GetAllAuthors");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    authors = JsonConvert.DeserializeObject<List<AuthorDto>>(responseContent);
                }
            }
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            AuthorDto author = new AuthorDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/author/GetAuthorById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<AuthorDto>(responseContent);
                }
            }
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthorCreateDto author)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(author);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/author/CreateAuthor", contentString);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(author);
        }

        // GET: AuthorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            AuthorDto author = new AuthorDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/author/GetAuthorById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<AuthorDto>(responseContent);
                }
            }
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AuthorCreateDto authorUpdateDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(authorUpdateDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/author/UpdateAuthor/{id}", contentString);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(authorUpdateDto);
        }

        // GET: AuthorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            AuthorDto author = new AuthorDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/author/GetAuthorById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<AuthorDto>(responseContent);
                }
            }
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync($"api/author/DeleteAuthor/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}
