using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using BookAuthor.DL;
using BookAuthor.Repositories;

namespace BookAuthor.MVCApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        
        public JsonResult GetAuthors()
        {
            var data = new List<object>();

            foreach (var item in _authorRepository.GetAll())
            {
                data.Add(new { Id = item.Id, Name = item.Name });
            }
         
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}