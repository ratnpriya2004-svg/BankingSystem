using Microsoft.AspNetCore.Mvc;
using BankingPro.Models;
using System.Security.Claims;
using System.Collections.Generic;

namespace BankingPro.Controllers
{
    public class AccountController : Controller  // NO CONSTRUCTOR!
    {
        public IActionResult Login()
        {
            return View();
        }

        // ✅ Login POST  
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Simple check (same as register email)
            string userId = email.Replace("@", "").Replace(".", "").ToLower() + DateTime.Now.Ticks;
            HttpContext.Session.SetString("UserId", userId);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("Name", email.Split('@')[0]);
            HttpContext.Session.SetString("AccountNumber", "BP" + userId.Substring(0, 8));

            TempData["Success"] = $"✅ Welcome back {email}!";
            return RedirectToAction("Dashboard"); // ← DASHBOARD ONLY
        }

        public IActionResult Register()
        {
            return View();
        }

        // ✅ Register POST
        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                string userId = email.Replace("@", "").Replace(".", "").ToLower() + DateTime.Now.Ticks;
                HttpContext.Session.SetString("UserId", userId);
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("Name", email.Split('@')[0]);
                HttpContext.Session.SetString("AccountNumber", "BP" + userId.Substring(0, 8));

                TempData["Success"] = $"✅ Welcome {email}! Account created!";
                return RedirectToAction("Dashboard"); // ← DASHBOARD ONLY
            }
            TempData["Error"] = "❌ Email और Password भरें!";
            return View();
        }


        public IActionResult Dashboard()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");
            ViewBag.Balance = 125000;

            return View();  // ← Dashboard.cshtml लौटाएगा
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.UserName = HttpContext.Session.GetString("Name");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");
            ViewBag.TotalBalance = 125000;
            ViewBag.JoinDate = "Jan 2026";

            return View();
        }
        // ✅ Insurance GET method (ये add करना था!)
        public IActionResult Insurance()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");
            ViewBag.TotalBalance = 125000;

            return View();
        }
        public IActionResult BalanceEnquiry()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");
            ViewBag.TotalBalance = 125000;
            ViewBag.SavingsBalance = 85000;
            ViewBag.FixedDeposit = 40000;
            ViewBag.CurrentBalance = 0;

            return View();
        }

        public IActionResult UpiQr()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");
            ViewBag.UpiId = HttpContext.Session.GetString("Email").Replace("@gmail.com", "@bankingpro");

            return View();
        }

        public IActionResult Support()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");

            return View();
        }
        public IActionResult EmiCalculator()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");

            return View();
        }
        public IActionResult Settings()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");

            return View();
        }

        public IActionResult Help()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");

            return View();
        }
        // ✅ User Management - MAIN METHOD (ये copy-paste करो)
        public IActionResult AdminUsers()
        {
            if (!CheckSession()) return RedirectToAction("Login");

            // Demo users data
            ViewBag.Users = new List<object>
    {
        new { Name = "Ratnpriya Kumari", Email = "ratnpriya@gmail.com", Account = "BP12345678", Role = "Admin", Status = "Active", Joined = "Jan 2026" },
        new { Name = "Rahul Kumar", Email = "rahul@gmail.com", Account = "BP87654321", Role = "User", Status = "Active", Joined = "Dec 2025" },
        new { Name = "Priya Sharma", Email = "priya@gmail.com", Account = "BP11223344", Role = "User", Status = "Inactive", Joined = "Nov 2025" },
        new { Name = "Amit Singh", Email = "amit@gmail.com", Account = "BP44556677", Role = "Premium", Status = "Active", Joined = "Jan 2026" }
    };

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.AccountNumber = HttpContext.Session.GetString("AccountNumber");

            return View();
        }


        private bool CheckSession()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("UserId"));
        }

        // ALL PROTECTED PAGES
        public IActionResult FeaturesHub() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult BillPayments() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult ApplyLoan() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult FixedDeposit() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult CreditCards() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult AccountStatement() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult TransferMoney() => CheckSession() ? View() : RedirectToAction("Login");
        
       
        public IActionResult MutualFunds() => CheckSession() ? View() : RedirectToAction("Login");
        

        // Bill Payments
        public IActionResult Electricity() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult Water() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult Mobile() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult Gas() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult DTH() => CheckSession() ? View() : RedirectToAction("Login");
        public IActionResult Broadband() => CheckSession() ? View() : RedirectToAction("Login");

        [HttpPost]
        public IActionResult BroadbandBill(string accountNumber, string planType, decimal amount)
        {
            if (!CheckSession()) return RedirectToAction("Login");
            TempData["Success"] = $"✅ Broadband Bill ₹{amount:N0} Paid!<br>Plan: {planType}";
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult TransferMoney(string toAccount, decimal amount, string description)
        {
            if (!CheckSession()) return RedirectToAction("Login");
            TempData["Success"] = $"✅ ₹{amount:N0} transferred to {toAccount}";
            return RedirectToAction("TransferMoney");
        }
       


        [HttpPost]
        public IActionResult WaterBill(string consumerNumber, decimal amount)
        {
            if (!CheckSession()) return RedirectToAction("Login");
            TempData["Success"] = $"Water Bill ₹{amount:N0} Paid!";
            return RedirectToAction("Dashboard");
        }

       
    }
}
