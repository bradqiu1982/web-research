using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
       /* public ActionResult Index()
        {
                return View(db.Movies.ToList());
        }*/
        
       public ActionResult Index(string movieGenre, string searchstring)
        {
            string sql = "Select Id,Title,ReleaseDate,Genre,Price,Rate from Movies";
            var movielist = DataBaseUtility.GetMovies(sql);

            var GenreLst = new List<string>();
            foreach (var item in movielist)
            {
                GenreLst.Add(item.Genre);
            }
            ViewBag.movieGenre = new SelectList(GenreLst);

            bool addgenre = false;

            var querymovie = new Movie();
            
            if (!string.IsNullOrEmpty(movieGenre))
            {
                querymovie.Genre = movieGenre;
                sql = sql + " where Genre = '"+ querymovie.dbGenre + "'";
                addgenre = true;
            }
            if(!string.IsNullOrEmpty(searchstring))
            {
                querymovie.Title = searchstring;
                if (addgenre)
                {
                    sql = sql + " and Title like '%"+ querymovie.dbTitle + "%'";
                }
                else
                {
                    sql = sql + " where Title like '%" + querymovie.dbTitle + "%'";
                }
            }

            movielist = DataBaseUtility.GetMovies(sql);
            return View(movielist);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string sql = "Select Id,Title,ReleaseDate,Genre,Price,Rate from Movies where Id = " + id.ToString();
            var movielist = DataBaseUtility.GetMovies(sql);
            if (movielist.Count == 0)
            {
                return HttpNotFound();
            }
            return View(movielist[0]);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rate")] Movie movie)
        {
            var ret = DataBaseUtility.AddMovie(movie.dbTitle, movie.ReleaseDate.ToString(), movie.dbGenre, movie.Price, movie.dbRate);
            if (ret)
            {
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string sql = "Select Id,Title,ReleaseDate,Genre,Price,Rate from Movies where Id = " + id.ToString();
            var movielist = DataBaseUtility.GetMovies(sql);

            if (movielist.Count == 0)
            {
                return HttpNotFound();
            }

            return View(movielist[0]);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rate")] Movie movie)
        {
            var ret = DataBaseUtility.UpdateMovie(movie.ID, movie.dbTitle, movie.ReleaseDate.ToString(), movie.dbGenre, movie.Price, movie.dbRate);
            if (ret)
            {
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string sql = "Select Id,Title,ReleaseDate,Genre,Price,Rate from Movies where Id = " + id.ToString();
            var movielist = DataBaseUtility.GetMovies(sql);

            if (movielist.Count == 0)
            {
                return HttpNotFound();
            }

            return View(movielist[0]);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DataBaseUtility.DeleteMovie(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
