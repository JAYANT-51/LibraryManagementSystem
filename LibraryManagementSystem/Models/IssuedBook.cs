using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class IssuedBook
    {
        [Key]
        public int IssueID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
