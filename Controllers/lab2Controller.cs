using lab_1.Models.Entities;
using lab_1.Models.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab_1.Controllers
{
    public class lab2Controller : Controller
    {
        // GET: lab2
        [AllowAnonymous]
        public ActionResult ListOfStudents()
        {

            List<Студенты> students = new List<Студенты>();
            using (var db = new Entities1())
            {
                students = db.Студенты.OrderByDescending(x => x.Дата_рождения)
                    .ThenBy(x => x.Фамилия)
                    .ThenBy(x => x.Имя).ToList();
            }
            List<StudentVM> studentsVM = new List<StudentVM>();
            foreach(var student in students)
            {
                StudentVM studentVM = new StudentVM {
                    ID_студента = student.ID_студента,
                    Фамилия = student.Фамилия,
                    Имя = student.Имя,
                    Отчество = student.Отчество,
                    Адрес_проживания = student.Адрес_проживания,
                    Дата_рождения = student.Дата_рождения,
                    Пол = student.Пол,
                    Уровень_владения_ИЯ = student.Уровень_владения_ИЯ,
                };
                studentsVM.Add(studentVM);
            }
            return View(studentsVM);
        }
        [Authorize]
        public ActionResult StudentDetails(Guid id) {
            Студенты model = new Студенты();
            using(var db = new Entities1())
            {
                model = db.Студенты.Find(id);
            }

            StudentVM studentVM = new StudentVM
            {
                ID_студента = model.ID_студента,
                Фамилия = model.Фамилия,
                Имя = model.Имя,
                Отчество = model.Отчество,
                Адрес_проживания = model.Адрес_проживания,
                Дата_рождения = model.Дата_рождения,
                Пол = model.Пол,
                Уровень_владения_ИЯ = model.Уровень_владения_ИЯ,
            };
            return View(studentVM);
        }

        public List<Tuple<string, string>> GetEnglishSkillsList()
        {
            List<Tuple<string, string>> list = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Высокий","Профессионал"),
                new Tuple<string, string>("Средний","Уверенный"),
                new Tuple<string, string>("Низкий","Начинающий"),
            };
            return list;
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateStudent() {
            ViewBag.Skills = new SelectList(GetEnglishSkillsList(), "Item1", "Item2");
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]

        public ActionResult CreateStudent(StudentVM newStudent)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Entities1())
                {
                    Студенты student = new Студенты()
                    {
                        ID_студента = Guid.NewGuid(),
                        Фамилия = newStudent.Фамилия,
                        Имя = newStudent.Имя,
                        Отчество = newStudent.Отчество,
                        Адрес_проживания = newStudent.Адрес_проживания,
                        Дата_рождения = newStudent.Дата_рождения,
                        Пол = newStudent.Пол,
                        Уровень_владения_ИЯ = newStudent.Уровень_владения_ИЯ,
                    };
                    db.Студенты.Add(student);
                    db.SaveChanges();

                }


                return RedirectToAction("ListOfStudents");
            }
            ViewBag.Skills = new SelectList(GetEnglishSkillsList(), "Item1", "Item2");

            return View(newStudent);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public ActionResult EditStudent(Guid studentID)
        {
            StudentVM studentVM;
            using (var db = new Entities1())
            {
                Студенты student = db.Студенты.Find(studentID);
                studentVM = new StudentVM
                {
                    ID_студента = student.ID_студента,
                    Фамилия = student.Фамилия,
                    Имя = student.Имя,
                    Отчество = student.Отчество,
                    Адрес_проживания = student.Адрес_проживания,
                    Дата_рождения = student.Дата_рождения,
                    Пол = student.Пол,
                    Уровень_владения_ИЯ = student.Уровень_владения_ИЯ,
                };
            }
            ViewBag.Skills = new SelectList(GetEnglishSkillsList(), "Item1", "Item2",studentVM.Уровень_владения_ИЯ);
            return View(studentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]

        public ActionResult EditStudent(StudentVM newStudent)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Entities1())
                {
                    Студенты student = new Студенты
                    {
                        ID_студента = newStudent.ID_студента,
                        Фамилия = newStudent.Фамилия,
                        Имя = newStudent.Имя,
                        Отчество = newStudent.Отчество,
                        Адрес_проживания = newStudent.Адрес_проживания,
                        Дата_рождения = newStudent.Дата_рождения,
                        Пол = newStudent.Пол,
                        Уровень_владения_ИЯ = newStudent.Уровень_владения_ИЯ,
                    };
                    db.Студенты.Attach(student);
                    db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }


                return RedirectToAction("ListOfStudents");
            }
            ViewBag.Skills = new SelectList(GetEnglishSkillsList(), "Item1", "Item2");

            return View(newStudent);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public ActionResult DeliteStudent(Guid studentID)
        {
            StudentVM studentVM;
            using (var db = new Entities1())
            {
                Студенты student = db.Студенты.Find(studentID);
                studentVM = new StudentVM
                {
                    ID_студента = student.ID_студента,
                    Фамилия = student.Фамилия,
                    Имя = student.Имя,
                    Отчество = student.Отчество,
                    Адрес_проживания = student.Адрес_проживания,
                    Дата_рождения = student.Дата_рождения,
                    Пол = student.Пол,
                    Уровень_владения_ИЯ = student.Уровень_владения_ИЯ,
                };
            }
            return View(studentVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeliteStudent")]
        public ActionResult DeliteConfirmed(Guid studentID)
        {

            using (var db = new Entities1())
            {
                Студенты student = new Студенты
                {
                    ID_студента = studentID
                };
                db.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

            }


            return RedirectToAction("ListOfStudents");



        }
        [ChildActionOnly]
        public ActionResult ShowGroups(Guid studentID) {
            string message = "";
            using (var db = new Entities1())
            {
                var studentGroup = db.Студенты_в_группах.FirstOrDefault(s => s.ID_студента == studentID);

                if (studentGroup != null)
                {
                    // Находим группу студента по ID группы из записи в таблице Студенты_в_группах
                    var group = db.Группы.FirstOrDefault(g => g.ID_группы == studentGroup.ID_группы);

                    if (group != null)
                    {
                        message = $"Группа: {group.Наименование}";
                        return PartialView("_PartialGroup", message);

                    }
                }
                
            }
            return RedirectToAction("ListOfStudents");

        }
    }

}

