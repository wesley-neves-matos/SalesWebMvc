using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]

        public DateTime BirthDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        ICollection<SalesRecord> SalesRecords { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, decimal salary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Salary = salary;
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
