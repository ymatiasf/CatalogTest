using System.Collections.Generic;
using System.Web.Http;
using BookAuthor.Repositories;
using Newtonsoft.Json.Linq;

namespace BookAuthor.MVCApp.Controllers
{
    public class SearchController : ApiController
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public SearchController(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        // GET: api/Search
        [System.Web.Http.HttpGet]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var data = new List<object>();

            foreach (var item in _bookRepository.GetAll())
            {
                data.Add(new { Id = item.Id, Title = item.Title,Edition = item.EditionDate , Authors = item.Authors.Count});
            }

            return Ok(data);
        }
        [HttpPost]
        [Route("api/Search/List")]
        // GET api/<controller>
        public IHttpActionResult Get(JObject book)
        {
            dynamic jsonData = book;
            var bookData = jsonData.book ;

            List<object> data = new List<object>();
            string title = bookData.title.ToString();
            var repo= _bookRepository.GetMany(p => p.Title.Contains(title));
            foreach (var item in repo)
            {
                data.Add(new { Id = item.Id, Title = item.Title, Edition = item.EditionDate, Authors = item.Authors.Count });
            }

            return Ok(data);
        }

        // GET: api/Search/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Search
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Search/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Search/5
        public void Delete(int id)
        {
        }
    }
}
