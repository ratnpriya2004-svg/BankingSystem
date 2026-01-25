using System;
using System.ComponentModel.DataAnnotations;

namespace BankingPro.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0;
        public string AccountType { get; set; } = "Savings";
        public string? UserId { get; set; }
    }

    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public int FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
    }
}
