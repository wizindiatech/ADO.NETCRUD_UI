using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ADO.NET_Crud.DAL;
using ADO.NET_Crud.Models;

namespace ADO.NET_Crud.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL productDAL = new Product_DAL();
        // GET: Product
        public ActionResult Index()
        {
            var productList = productDAL.GetAllProducts();

            if(productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently no data available." ;
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var product = productDAL.GetProductById(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product Not Availablr With Id " + id.ToString();
                    return RedirectToAction("Index");
                }

                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                // TODO: Add insert logic here
                bool IsInserted = false;

                if (ModelState.IsValid)
                {
                    IsInserted = productDAL.InsertProduct(product);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product Details Saved Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable To Insert The ProductDetails.";
                    }
                    
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = productDAL.GetProductById(id).FirstOrDefault();
            if(products == null)
            {
                TempData["InfoMessage"] = "Product not available with Id " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = productDAL.UpdateProduct(product);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product Details Updated Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable To Update The ProductDetails.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = productDAL.GetProductById(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product Is Not Available with id " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = productDAL.DeleteProduct(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
