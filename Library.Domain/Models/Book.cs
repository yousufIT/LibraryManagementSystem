using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    public class Book
    {
        public int BookID { get; set; }     
        public string Title { get; set; }     
        public DateTime PublishedDate { get; set; }  
        public string Description { get; set; } 
        public int? MainCategoryID { get; set; } 
        public int? SubCategoryID { get; set; }  
        public string Publisher { get; set; }   
        public int Pages { get; set; }        
        public string Language { get; set; }    
        public decimal Price { get; set; }     
        public string Edition { get; set; }    
        public string Format { get; set; }     
        public string Location { get; set; }  
        public  Category MainCategory { get; set; }
        public  Category SubCategory { get; set; } 
        public  List<BookAuthor> BookAuthors { get; set; }
        public Book()
        {
            BookAuthors = new List<BookAuthor>();
        }
    }
}
