using CalculatorMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorMVC.Controllers
{
    public class CalculatorController : Controller
    {
        private CalculationRepository _repository = new CalculationRepository();

        // GET: Calculator
        public ActionResult Index()
        {
            return View(new CalculationModel());
        }

        [HttpPost]
        public ActionResult Index(CalculationModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Operation)
                {
                    case "Add":
                        model.Result = model.Number1 + model.Number2;
                        break;
                    case "Subtract":
                        model.Result = model.Number1 - model.Number2;
                        break;
                    case "Multiply":
                        model.Result = model.Number1 * model.Number2;
                        break;
                    case "Divide":
                        if (model.Number2 != 0)
                        {
                            model.Result = model.Number1 / model.Number2;
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cannot divide by zero.");
                            return View(model);  // Return view if there's a validation error
                        }
                        break;
                }

                // Save calculation to database
                try
                {
                    _repository.SaveCalculation(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to the database: " + ex.Message);
                    return View(model);  // Stay on the same page if the save fails
                }

                // Redirect to result page
                return RedirectToAction("Result", new 
                { 
                    Number1 = model.Number1, 
                    Number2 = model.Number2, 
                    Operation = model.Operation, 
                    Result = model.Result 
                });
            }
            return View(model);
        }

        // GET: Calculator/Result
        public ActionResult Result(int Number1, int Number2, string Operation, int Result)
        {
            // Recreate the model to pass to the view
            var model = new CalculationModel
            {
                Number1 = Number1,
                Number2 = Number2,
                Operation = Operation,
                Result = Result
            };

            return View(model);  // Load the Result view with the model
        }
    }
}
