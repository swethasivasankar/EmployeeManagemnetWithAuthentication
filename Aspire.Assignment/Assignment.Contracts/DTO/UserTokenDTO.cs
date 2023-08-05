using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Contracts.DTO
{
  //for employee login created this
    public class UserTokenDTO
    {
      //  public int Id { get; set; }
        public string Token { get; set; }
        public string? Roles { get; set; }
        public int EmpDetailsID{get; set;}


    }
}
