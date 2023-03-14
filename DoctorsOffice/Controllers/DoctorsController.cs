using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;
using System.Collections.Generic;
using System.Linq;

namespace DoctorsOffice.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly DoctorsOfficeContext _db;

        public DoctorsController(DoctorsOfficeContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Doctor> model = _db.Doctors.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Specialties = _db.Specialties.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            _db.Doctors.Add(doctor);

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Doctor thisDoctor = _db.Doctors
                                .Include(doctor => doctor.Patients)
                                .Include(doctor => doctor.JoinEntities)
                                .ThenInclude(join => join.Specialty)
                                .FirstOrDefault(doctor => doctor.DoctorId == id);
            return View(thisDoctor);
        }

        public ActionResult Edit(Doctor doctor, int id)
        {
            Doctor thisDoctor = _db.Doctors
                            .Include(doctor => doctor.Patients)
                            .Include(doctor => doctor.JoinEntities)
                            .ThenInclude(join => join.Specialty)
                            .FirstOrDefault(doctor => doctor.DoctorId == id);
            ViewBag.SpecialtyId = new SelectList(_db.Specialties, "SpecialtyId", "Name");
            return View(thisDoctor);
        }

        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            _db.Doctors.Update(doctor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("UpdateDoctorSpec")]
        public ActionResult UpdateDoctorSpec(int DoctorId, int SpecialtyId)
        {
            #nullable enable
            DoctorSpecialty? joinEntity = _db.DoctorSpecialties.FirstOrDefault(join => (join.DoctorId == DoctorId && join.SpecialtyId == SpecialtyId));
            #nullable disable
            if (joinEntity == null && DoctorId != 0)
            {
                _db.DoctorSpecialties.Add(new DoctorSpecialty() { DoctorId = DoctorId, SpecialtyId = SpecialtyId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = DoctorId });
        }

        public ActionResult Delete(int id)
        {
            Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
            return View(thisDoctor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
            _db.Doctors.Remove(thisDoctor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
