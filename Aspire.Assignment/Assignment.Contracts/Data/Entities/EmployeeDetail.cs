using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Contracts.Data.Entities
{
    public partial class EmployeeDetail
    {
        public int EmployeeDetailId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public int GenderRefId { get; set; }
        public string ContactNumber { get; set; } = null!;
        public string? AlternateNumber { get; set; }
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? Zip { get; set; }
        public string EmployeeNumber { get; set; } = null!;
        public int EmpDesignationId { get; set; }
        public int PracticeRefId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Doj { get; set; }
        public int EmpTypeId { get; set; }

        public int UserId { get; set; } 

        public virtual ReferenceTbl GenderRef { get; set; } = null!;
        public virtual ReferenceTbl PracticeRef { get; set; } = null!;
        public virtual ReferenceTbl EmplDesig { get; set; } = null!;
        public virtual ReferenceTbl EmplType { get; set; } = null!;

        
       public virtual User user { get; set; } = null!;

    }
}
