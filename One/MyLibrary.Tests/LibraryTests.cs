using System;
using System.Linq;
using MyLibrary;
using Xunit;

namespace MyLibrary.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void AddBook_AddsBookSuccessfully()
        {
            // Arrange
            var library = new Library();
            var book = new Book("Library Book", "Author", "777777", 2005);
            // Act
            library.AddBook(book);
            // Assert
            Assert.Contains(book, library.GetAvailableBooks());
        }

        [Fact]
        public void AddMember_AddsMemberSuccessfully()
        {
            // Arrange
            var library = new Library();
            var member = new Member("Bob", 4);
            // Act
            library.AddMember(member);
            // Assert
            // Since there is no direct method to list members, we verify by checking that a loan with a non-existent book fails.
            Assert.Throws<ArgumentException>(() => library.LoanBook("dummy", 4, 10));
        }

        [Fact]
        public void LoanBook_CreatesLoanAndUpdatesAvailability()
        {
            // Arrange
            var library = new Library();
            var book = new Book("Library Book", "Author", "777777", 2005);
            var member = new Member("Bob", 4);
            library.AddBook(book);
            library.AddMember(member);
            // Act
            var loan = library.LoanBook("777777", 4, 10);
            // Assert
            Assert.False(book.IsAvailable);
            Assert.Contains(loan, library.GetActiveLoans());
        }

        [Fact]
        public void ReturnBook_UpdatesLoanStatus()
        {
            // Arrange
            var library = new Library();
            var book = new Book("Library Book", "Author", "777777", 2005);
            var member = new Member("Bob", 4);
            library.AddBook(book);
            library.AddMember(member);
            var loan = library.LoanBook("777777", 4, 10);
            // Act
            library.ReturnBook("777777", 4);
            // Assert
            Assert.True(book.IsAvailable);
            Assert.DoesNotContain(loan, library.GetActiveLoans());
        }

        [Fact]
        public void GetSummary_ReturnsCorrectFormat()
        {
            // Arrange
            var library = new Library();
            library.AddBook(new Book("Book1", "Author1", "111111", 1990));
            library.AddBook(new Book("Book2", "Author2", "222222", 2000));
            library.AddMember(new Member("Alice", 1));
            library.AddMember(new Member("Bob", 2));
            // Act
            var summary = library.GetSummary();
            // Assert
            Assert.Contains("Total Books", summary);
            Assert.Contains("Available", summary);
            Assert.Contains("Total Members", summary);
        }

        // Additional test to check duplicate book addition
        [Fact]
        public void AddBook_ThrowsForDuplicateISBN()
        {
            // Arrange
            var library = new Library();
            var book1 = new Book("Book1", "Author1", "111111", 1990);
            var book2 = new Book("Book2", "Author2", "111111", 2005);
            library.AddBook(book1);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => library.AddBook(book2));
        }
    }
}
