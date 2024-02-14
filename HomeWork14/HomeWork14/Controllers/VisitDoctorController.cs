using HomeWork14.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace HomeWork14.Controllers
{
    public class VisitDoctorController : Controller
    {
        private const string filePath = "bookingDetails.json";

        public IActionResult Index()
        {
            return View();
        }

        [Route("/booking",Name ="Booking")]
        public IActionResult Booking()
        {
            //return Content("Hello, this is booking page");

            return View("~/Views/Home/Booking.cshtml");
        }

        public ActionResult BookingPerson(PersonModel personModel)
        {
            if (!timeValid(personModel.Time))
            {
                ModelState.AddModelError("Time", "Doctor Booking time must be between 10:00 and 19:00");
                return View("Index", personModel);
            }

            List<PersonModel> personModels = loadPersonModels();

            personModels.Add(personModel);

            saveBookingDetails(personModels);

            return RedirectToAction("Index");
        }




        private bool timeValid(string time)
        {
            if(DateTime.TryParse(time, out DateTime bookingTime))
            {
                DateTime startTime = DateTime.Parse("10:00");
                DateTime finishTime = DateTime.Parse("19:00");
                return bookingTime.TimeOfDay >= startTime.TimeOfDay && bookingTime.TimeOfDay <= finishTime.TimeOfDay;
            }
            return false;
        }



        private List<PersonModel> loadPersonModels()
        {
            if(System.IO.File.Exists(filePath))
            {
                string jsonData = System.IO.File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<PersonModel>>(jsonData);
            }
            return new List<PersonModel>();
        }

        private void saveBookingDetails(List<PersonModel> personModels)
        {
            string jsonData = JsonSerializer.Serialize(personModels);
            System.IO.File.WriteAllText(filePath, jsonData);
        }
    }
}
