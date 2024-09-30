using FastFood.Models;
using FastFood.Repositories;
using FastFood.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

//namespace FastFood.Web.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class ItemController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IWebHostEnvironment _webHostEnvironment;

//        public ItemController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
//        {
//            _context = context;
//            _webHostEnvironment = webHostEnvironment;
//        }

//        // GET: Admin/Item
//        [HttpGet]
//        public IActionResult Index()
//        {
//            var items = _context.Items.Include(x => x.Category).Include(y => y.SubCategory).ToList();
//            return View(items);
//        }

//        // GET: Admin/Item/Create
//        [HttpGet]
//        public IActionResult Create()
//        {
//            ItemViewModel vm = new ItemViewModel();
//            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title");
//            return View(vm);
//        }

//        // POST: Admin/Item/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(ItemViewModel vm)
//        {
//            if (ModelState.IsValid)
//            {
//                Item model = new Item
//                {
//                    Name = vm.Name,
//                    Description = vm.Description,
//                    Price = vm.Price,
//                    CategoryId = vm.CategoryId,
//                    SubCategoryId = vm.SubCategoryId
//                };

//                if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
//                {
//                    var uploadDir = @"Images/Items";
//                    var filename = Guid.NewGuid().ToString() + "-" + Path.GetFileName(vm.ImageUrl.FileName);
//                    var path = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir, filename);

//                    if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, uploadDir)))
//                    {
//                        Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, uploadDir));
//                    }

//                    using (var fileStream = new FileStream(path, FileMode.Create))
//                    {
//                        await vm.ImageUrl.CopyToAsync(fileStream);
//                    }

//                    model.ImageUrl = "/" + uploadDir + "/" + filename;
//                }

//                _context.Items.Add(model);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }

//            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title");
//            return View(vm);
//        }

//        // GET: Admin/Item/Edit/5
//        [HttpGet]
//        public IActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var item = _context.Items.Find(id);
//            if (item == null)
//            {
//                return NotFound();
//            }

//            var vm = new ItemViewModel
//            {
//                Id = item.Id,
//                Name = item.Name,
//                Description = item.Description,
//                Price = item.Price,
//                CategoryId = item.CategoryId,
//                SubCategoryId = item.SubCategoryId,
//               // ImageUrl = item.Image
//            };

//            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title", vm.CategoryId);
//            return View(vm);
//        }

//        // POST: Admin/Item/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, ItemViewModel vm)
//        {
//            if (id != vm.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var item = await _context.Items.FindAsync(id);
//                    if (item == null)
//                    {
//                        return NotFound();
//                    }

//                    item.Name = vm.Name;
//                    item.Description = vm.Description;
//                    item.Price = vm.Price;
//                    item.CategoryId = vm.CategoryId;
//                    item.SubCategoryId = vm.SubCategoryId;

//                    if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
//                    {
//                        var uploadDir = @"Images/Items";
//                        var filename = Guid.NewGuid().ToString() + "-" + Path.GetFileName(vm.ImageUrl.FileName);
//                        var path = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir, filename);

//                        if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, uploadDir)))
//                        {
//                            Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, uploadDir));
//                        }

//                        using (var fileStream = new FileStream(path, FileMode.Create))
//                        {
//                            await vm.ImageUrl.CopyToAsync(fileStream);
//                        }

//                        // Delete old image if new image is uploaded
//                        if (!string.IsNullOrEmpty(item.ImageUrl))
//                        {
//                            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, item.ImageUrl.TrimStart('/'));
//                            if (System.IO.File.Exists(oldImagePath))
//                            {
//                                System.IO.File.Delete(oldImagePath);
//                            }
//                        }

//                        item.ImageUrl = "/" + uploadDir + "/" + filename;
//                    }

//                    _context.Update(item);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ItemExists(vm.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }

//                return RedirectToAction(nameof(Index));
//            }

//            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title", vm.CategoryId);
//            return View(vm);
//        }

//        // GET: Admin/Item/Delete/5
//        [HttpGet]
//        public IActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var item = _context.Items.Include(i => i.Category).Include(i => i.SubCategory)
//                .FirstOrDefault(m => m.Id == id);

//            if (item == null)
//            {
//                return NotFound();
//            }

//            return View(item);
//        }

//        // POST: Admin/Item/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var item = await _context.Items.FindAsync(id);
//            if (item == null)
//            {
//                return NotFound();
//            }

//            // Delete the image file
//            if (!string.IsNullOrEmpty(item.ImageUrl))
//            {
//                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, item.ImageUrl.TrimStart('/'));
//                if (System.IO.File.Exists(imagePath))
//                {
//                    System.IO.File.Delete(imagePath);
//                }
//            }

//            _context.Items.Remove(item);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ItemExists(int id)
//        {
//            return _context.Items.Any(e => e.Id == id);
//        }
//    }
//}



namespace FastFood.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        public ItemController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var items = _context.Items.Include(x => x.Category).Include(y => y.SubCategory).ToList();
            return View(items); 
        }


        [HttpGet]
        public IActionResult Create()
        {
            ItemViewModel vm = new ItemViewModel();
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title");
            return View(vm);
        }

        [HttpGet]
        public IActionResult GetSubCategory(int categoryId)
        {
            var subCategory = _context.SubCategories.Where(x => x.CategoryId == categoryId).FirstOrDefault();
            return Json(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel vm)
        {
            Item model = new Item();
            if (ModelState.IsValid)
            {
                if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
                {
                    var uploadDir = @"Images/Items";
                    var filename = Guid.NewGuid().ToString() + "-" + vm.ImageUrl.FileName;
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir
                    , filename);
                    await vm.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    model.ImageUrl = "/" + uploadDir + "/" + filename;
                    
                }
                _context.Items.Add(model);
                // _context.SaveChanges();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title", vm.CategoryId);
            return View(vm);
        }

        // GET: Admin/Item/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var vm = new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.CategoryId,
                SubCategoryId = item.SubCategoryId,
                 //ImageUrl = item.ImageUrl
            };

            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title", vm.CategoryId);
           
            return View(vm);
        }

         // POST: Admin/Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _context.Items.FindAsync(id);
                    if (item == null)
                    {
                        return NotFound();
                    }

                    item.Name = vm.Name;
                    item.Description = vm.Description;
                    item.Price = vm.Price;
                    item.CategoryId = vm.CategoryId;
                    item.SubCategoryId = vm.SubCategoryId;

                    if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
                    {
                        var uploadDir = @"Images/Items";
                        var filename = Guid.NewGuid().ToString() + "-" + Path.GetFileName(vm.ImageUrl.FileName);
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir, filename);

                        if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, uploadDir)))
                        {
                            Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, uploadDir));
                        }

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await vm.ImageUrl.CopyToAsync(fileStream);
                        }

                        // Delete old image if new image is uploaded
                        if (!string.IsNullOrEmpty(item.ImageUrl))
                        {
                            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, item.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        item.ImageUrl = "/" + uploadDir + "/" + filename;
                    }

                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex) { return NotFound(); }
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ItemExists(vm.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title", vm.CategoryId);
            return View(vm);
        }

        //        // GET: Admin/Item/Delete/5
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _context.Items.Include(i => i.Category).Include(i => i.SubCategory)
                .FirstOrDefault(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        //        // POST: Admin/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // Delete the image file
            if (!string.IsNullOrEmpty(item.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, item.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }


    }
}

