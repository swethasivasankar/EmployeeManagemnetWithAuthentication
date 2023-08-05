using System.ComponentModel.DataAnnotations;

namespace Assignment.Contracts.DTO
{
    public class CreateEmployeeDTO
    {
        public int EmployeeDetailId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public int GenderRefId { get; set; }
        public string ContactNumber { get; set; } 
        public string? AlternateNumber { get; set; }
        public string Email { get; set; } 
        public string Address { get; set; } 
        public string City { get; set; } 
        public string State { get; set; } 
        public string Country { get; set; } 
        public string? Zip { get; set; }
        public string EmployeeNumber { get; set; } 
		public int EmpDesignationId { get; set; }
        public int PracticeRefId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Doj { get; set; }
        public int EmpTypeId { get; set; }
        public int? UserId { get; set; } 

        
    }
}