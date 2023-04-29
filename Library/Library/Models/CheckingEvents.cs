using NuGet.ProjectModel;

namespace Library.Models
{
    public class CheckingEvents
    {
        //checking Event Id
        public int Id { get; set; }

        //checking event is deleted?
        public bool IsDeleted { get; set; }        

        //book that's being checked out
        public int BookId { get; set; }

        //User that's checking out the book
        public int UserId { get; set; }
        public CheckingEvents(int bookId, int userId) 
        {          
            BookId = bookId;
            UserId = userId;
        }

        //helper methods if I have any
    }
}
