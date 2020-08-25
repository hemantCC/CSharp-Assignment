using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Assignment.MVC.CustomFilter;
using Assignment.MVC.Models;
using Assignment.MVC.Models.BusinessEntities;
using Assignment.MVC.Models.DataEntities;
using AutoMapper;

namespace Assignment.MVC.Controllers
{
    [Authorize]
    [CustomExceptionHandle]
    public class ProductController : Controller
    {

        /// <summary>
        /// shows the view for List of all Products
        /// </summary>
        /// <returns>View</returns>

        [OutputCache(CacheProfile = "CacheProfileClient", Location = OutputCacheLocation.Client, NoStore = true)]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (var _context = new ApplicationDbContext())
            {
                List<Product> ProductsDomain = await _context.Products.ToListAsync();
                List<ProductVM> productVMs = Mapper.Map<List<Product>, List<ProductVM>>(ProductsDomain);
                return View("Index", productVMs);
            }
        }


        /**
         This Method shows View of Both Add Product 
            and Edit product based on Condition
         */
        [HttpGet]
        public async Task<ActionResult> AddOrEditProduct(int? id = 0)
        {
            if (id == 0)
            {
                //Returns view for Add product
                ViewBag.Heading = "Add Product";
                return View(new ProductVM());
            }
            else
            {
                //Returns View for Edit Existing Product
                ViewBag.Heading = "Edit Product";
                using (var _context = new ApplicationDbContext())
                {
                    Product ProductDomain = await _context.Products.FindAsync(id);
                    if (ProductDomain == null)
                    {
                        return HttpNotFound();
                    }
                    ProductVM ProductDTO = Mapper.Map<Product, ProductVM>(ProductDomain);
                    return View(ProductDTO);
                }
            }
        }


        /// <summary>
        /// Adds new Product or Edits Existing Product
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrEditProduct(ProductVM productVM)
        {
            using (var _context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    //Mapping DTO model to Domain Model
                    Product ProductDomain = Mapper.Map<ProductVM, Product>(productVM);
                    bool ProductExist = await _context.Products.AnyAsync(x => x.Id == productVM.Id); //Checks if product exists


                    //Add new Product Section
                    if (!ProductExist)
                    {
                        if (productVM.ImageFile == null)
                        {
                            ModelState.AddModelError("", "Please Insert Image");
                            return View(productVM);
                        }

                        //Insert Image 
                        string FileName = Path.GetFileNameWithoutExtension(productVM.ImageFile.FileName);
                        string Extension = Path.GetExtension(productVM.ImageFile.FileName);
                        FileName = FileName + DateTime.Now.ToString("yymmssfff") + Extension;
                        ProductDomain.Image = productVM.Image = "~/Images/" + FileName;
                        FileName = Path.Combine(Server.MapPath("~/Images/"), FileName);
                        productVM.ImageFile.SaveAs(FileName);

                        _context.Products.Add(ProductDomain);
                        await _context.SaveChangesAsync();
                    }


                    /** 
                     Edit Existing Product Section
                     */
                    else
                    {
                        if (productVM.ImageFile != null)
                        {
                            //Insert Image 
                            string FileName = Path.GetFileNameWithoutExtension(productVM.ImageFile.FileName);
                            string Extension = Path.GetExtension(productVM.ImageFile.FileName);
                            FileName = FileName + DateTime.Now.ToString("yymmssfff") + Extension;
                            ProductDomain.Image = "~/Images/" + FileName;
                            FileName = Path.Combine(Server.MapPath("~/Images/"), FileName);
                            productVM.ImageFile.SaveAs(FileName);
                        }
                        else
                        {
                            //if Image is not an updated field
                            ProductDomain.Image = _context.Products.Where(x => x.Id == ProductDomain.Id).Select(x => x.Image).FirstOrDefault();
                        }

                        _context.Entry(ProductDomain).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("Index", "Product");
                }

                return View("AddOrEditProduct", productVM);
            }
        }


        /// <summary>
        /// This method Deletes Multiple records
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMultiple(FormCollection formCollection)
        {
            using (var _context = new ApplicationDbContext())
            {
                if (formCollection["DeleteId"] == null)   //checks for not items checked
                {
                    TempData["NoDeleteItems"] = "NoDeleteItems"; //for alert
                    //ViewBag.Message = "NoDeleteItems";

                    List<Product> ProductsDomain = await _context.Products.ToListAsync();
                    List<ProductVM> productVMs = Mapper.Map<List<Product>, List<ProductVM>>(ProductsDomain);
                    return View("Index", productVMs);
                }

                string[] ids = formCollection["DeleteId"].Split(new char[] { ',' });   //stores all selected item ids in array 
                List<Product> tblProduct = new List<Product>();

                foreach (var id in ids)      //stores selected product objects for deletion
                {
                    int Id = int.Parse(id);
                    Product product = await _context.Products.Where(x => x.Id == Id).FirstOrDefaultAsync();
                    tblProduct.Add(product);
                }

                _context.Products.RemoveRange(tblProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Product");
            }
        }




    }
}