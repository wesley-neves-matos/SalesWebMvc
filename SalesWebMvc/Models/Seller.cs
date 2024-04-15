﻿namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal MyProperty { get; set; }

        public Department Department { get; set; }
        ICollection<SalesRecord> SalesRecords { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, decimal myProperty, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            MyProperty = myProperty;
            Department = department;
        }

        public void AddSale(SalesRecord sale)
        {
            SalesRecords.Add(sale);
        }

        public void RemoveSale(SalesRecord sale)
        {
            SalesRecords.Remove(sale);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return SalesRecords.Where(i => i.Date >= initial &&
                                            i.Date <= final)
                               .Sum(i => i.Amount);
        }

    }
}