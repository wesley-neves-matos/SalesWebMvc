﻿using SalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public SaleStatus Status { get; set; }

        public int SellerId { get; set; }
        public Seller? Seller { get; set; }
        public SalesRecord()
        {
        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
