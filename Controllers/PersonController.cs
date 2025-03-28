namespace Household_Balance_Tracker.Controllers;

using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using Household_Balance_Tracker.Models;
using Household_Balance_Tracker.Models.ViewModels;

/// <summary>
/// Class responsible for managing person data between veiws and model
/// </summary>
public class PersonController : Controller
{
    public static Dictionary<string, Person>? people = new Dictionary<string, Person>();
    public static int idCounter = 0;

    public IActionResult InsertPersonForm()
    {
        return View();
    }

    public IActionResult RemovePersonForm()
    {
        return View();
    }

    public IActionResult ListPeople()
    {
        return View();
    }

    [HttpPost]
    public IActionResult InsertPerson(PersonViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (people.ContainsKey(model.email))
            {
                return View("InsertPersonFailed", model);
            }
            people.Add(model.email, new Person(idCounter++, model.name, model.age, model.email));

            ViewBag.Massage = "Registration Successfull";
            return View("SuccessfulPersonWarning", model);
        }
        else
        {
            return View("InsertPersonForm");
        }
    }

    [HttpPost]
    public IActionResult RemovePerson(DeletePersonViewModel? model)
    {
        if (!ModelState.IsValid)
        {
            return View("RemovePersonForm");
        }

        if (people.Count == 0 || !people.ContainsKey(model.email))
        {
            return View("PersonRemovalFailed", model);
        }

        bool uselessReturn = TransactionController.removePersonTransactions(model.email);
        people.TryGetValue(model.email, out Person toBeRemoved);

        people.Remove(model.email);
        return View("SuccessfulPersonRemoval", new PersonViewModel(toBeRemoved.getName(),
                                                   toBeRemoved.getAge(),
                                                   toBeRemoved.getEmail()));
    }
}