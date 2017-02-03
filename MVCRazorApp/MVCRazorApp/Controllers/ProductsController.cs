﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Net;

using MVCRazorApp.Models;
using System.IO;

namespace MVCRazorApp.Controllers
{
	public class ProductsController : Controller
	{

		private ProductDBContext db = new ProductDBContext();

		// GET: /Products/ 
		public ActionResult Index()
		{
			return View(db.Products.ToList());
		}

		// GET: /Movies/Details/5 
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// GET: /Products/Create 
		public ActionResult Create()
		{
			return View();
		}

		// POST: /Product/Create 
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for  
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include="Name,Type,Description,Price")] Product product, HttpPostedFileBase Img)
		{
			if (ModelState.IsValid)
			{
				byte[] imgByte = null;

				if (Img != null)
				{
					using (var binaryReader = new BinaryReader(Img.InputStream))
					{
						imgByte = binaryReader.ReadBytes(Img.ContentLength);
					}
					product.ProductImg = imgByte;
					       
				}



				db.Products.Add(product);

				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(product);
		}

		// GET: /Product/Edit/5 
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: /Product/Edit/5 
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for  
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int ID, string Name, string Type, string Description, decimal Price, HttpPostedFileBase Img)
		{
			Product product = db.Products.Find(ID);



			if (ModelState.IsValid)
			{
				product.Name = Name;
				product.Type = Type;
				product.Description = Description;
				product.Price = Price;

				if (Img != null)
				{
					byte[] imgByte;
					using (var binaryReader = new BinaryReader(Img.InputStream))
					{
						imgByte = binaryReader.ReadBytes(Img.ContentLength);
					}
					product.ProductImg = imgByte;

				}

				db.Entry(product).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(product);
		}

		// GET: /Movies/Delete/5 
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: /Product/Delete/5 
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Product product = db.Products.Find(id);
			db.Products.Remove(product);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

	}
}
