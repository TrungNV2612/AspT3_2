using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspT3
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsManager { get; set; }
        public Employee() { }
        public Employee(int id, string name, string email, string password, int isManager) 
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.IsManager = isManager;
        }        
               
    }
}
