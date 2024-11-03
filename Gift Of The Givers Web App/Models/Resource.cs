using System.Reflection;

namespace Gift_Of_The_Givers_Web_App.Models
{
    public class Resource
    {
        public int ResourceID { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

       
    }

}
