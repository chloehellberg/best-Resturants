using Microsoft.AspNetCore.Mvc;
using Yelp.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yelp.Controllers
{
  public class ReviewsController : Controller
  {
    private readonly YelpContext _db;

    public ReviewsController(YelpContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Review> model = _db.Reviews.Include(review => review.Restaurant).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Description");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Review review)
    {
      _db.Reviews.Add(review);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Review thisReview = _db.Reviews.FirstOrDefault(reviews => reviews.ReviewId == id);
      return View(thisReview);
    }

    public ActionResult Edit(int id)
    {
      var thisReview = _db.Reviews.FirstOrDefault(reviews => reviews.ReviewId == id);
      ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Description");
      return View(thisReview);
    }

    [HttpPost]
    public ActionResult Edit(Review review)
    {
      _db.Entry(review).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisReview = _db.Reviews.FirstOrDefault(reviews => reviews.ReviewId == id);
      return View(thisReview);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisReview = _db.Reviews.FirstOrDefault(reviews => reviews.ReviewId == id);
      _db.Reviews.Remove(thisReview);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}