using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [EmailAddress(ErrorMessage = "Enter a valid email!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [Range(100.0, 50000.0, ErrorMessage ="{0} must be from {1} to {2}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Salary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
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
