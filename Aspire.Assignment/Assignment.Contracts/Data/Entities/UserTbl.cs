using System;
using System.Collections.Generic;

namespace Assignment.Contracts.Data.Entities
{
    public partial class UserTbl
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
