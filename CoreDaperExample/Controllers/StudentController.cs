using CoreDaperExample.Models;
using CoreDaperExample.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDaperExample.Controllers
{
    public class StudentController : Controller
    {
        private IStudentRepository rep;
        public StudentController(IStudentRepository _rep)
        {
            rep = _rep;
        }
        public IActionResult Index(string name,string surname,string adress)
        {

            return View(rep.GetList(name,surname,adress));
        }
        public IActionResult Details(int id)
        {
            Student student = rep.Find(id);
            return View(student);


        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("StudentID,Name,SurName,Number,Adress")] Student student)
        {
            if (ModelState.IsValid)
            {
                rep.Add(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        public IActionResult Edit(int? id)
        {
            Student student = rep.Find(id.GetValueOrDefault());
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("StudentID,Name,SurName,Number,Adress")] Student student)
        {
            if (ModelState.IsValid)
            {
                rep.Update(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            rep.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }
       
    }
}
        
            
