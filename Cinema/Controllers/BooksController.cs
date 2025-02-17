﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Cinema.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Description,Genre,BookAddedTime,PublishYear,CoverImage")] Books books, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("CoverImage", "Please Upload Image");
                }


                if (!ModelState.IsValid)
                {
                    return View(books);
                }

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var imageToBeUploadedByteArray = memoryStream.ToArray();
                        books.CoverImage = imageToBeUploadedByteArray;
                    }
                }

                if (books.PublishYear <= DateTime.Now.Year)
                {
                    _context.Add(books);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("PublishYear", "Selected year can not be greater than current");
                }

            }
            return View(books);
        }


        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Author,Description,Genre,BookAddedTime,PublishYear,CoverImage")] Books books,  IFormFile file)
        {
            if (id != books.BookId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("CoverImage", "Please Upload Image");
                }

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var imageToBeUploadedByteArray = memoryStream.ToArray();
                        books.CoverImage = imageToBeUploadedByteArray;
                    }
                    if (books.PublishYear <= DateTime.Now.Year)
                    {
                        try
                        {
                            _context.Update(books);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!BooksExists(books.BookId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }

                        }
                        return RedirectToAction(nameof(Index));

                        //_context.Add(books);
                        //await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("PublishYear", "Selected year can not be greater than current");
                    }
                    

                }

             
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Books.FindAsync(id);
            _context.Books.Remove(books);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }

}
