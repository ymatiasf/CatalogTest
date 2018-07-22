using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAuthor.CORE;
using BookAuthor.DL;
using BookAuthor.Repositories;

namespace BookAuthor.MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IAuthorRepository authorRepository,IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
       
    }
}