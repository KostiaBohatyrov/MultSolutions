using System;
using MyLibrary;
using Xunit;

namespace MyLibrary.Tests
{
    public class MemberTests
    {
        [Fact]
        public void BorrowBook_AddsBookToBorrowedList()
        {
            // Arrange
            var member = new Member("John Doe", 1);
            var book = new Book("Test Book", "Author", "111111", 1999);
            // Act
            member.BorrowBook(book);
            // Assert
            Assert.Contains(book, member.GetBorrowedBooks());
        }

        [Fact]
        public void ReturnBook_RemovesBookFromBorrowedList()
        {
            // Arrange
            var member = new Member("John Doe", 1);
            var book = new Book("Test Book", "Author", "111111", 1999);
            member.BorrowBook(book);
            // Act
            member.ReturnBook(book);
            // Assert
            Assert.DoesNotContain(book, member.GetBorrowedBooks());
        }

        [Fact]
        public void TotalBorrowedBooks_ReturnsCorrectCount()
        {
            // Arrange
            var member = new Member("John Doe", 1);
            var book1 = new Book("Test Book 1", "Author", "111111", 1999);
            var book2 = new Book("Test Book 2", "Author", "222222", 2001);
            // Act
            member.BorrowBook(book1);
            member.BorrowBook(book2);
            // Assert
            Assert.Equal(2, member.TotalBorrowedBooks());
        }

        [Fact]
        public void BorrowBook_ThrowsWhenBookNotAvailable()
        {
            // Arrange
            var member = new Member("John Doe", 1);
            var book = new Book("Test Book", "Author", "111111", 1999);
            book.CheckOut();
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => member.BorrowBook(book));
        }

        // Additional tests for return operation exceptions
        [Fact]
        public void ReturnBook_ThrowsWhenBookNotBorrowed()
        {
            // Arrange
            var member = new Member("John Doe", 1);
            var book = new Book("Test Book", "Author", "111111", 1999);
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => member.ReturnBook(book));
        }

        // Extra test to check membership date is set correctly
        [Fact]
        public void MembershipDate_IsSetOnCreation()
        {
            // Arrange
            var before = DateTime.Now;
            var member = new Member("Jane Doe", 2);
            var after = DateTime.Now;
            // Assert
            Assert.True(member.MembershipDate >= before && member.MembershipDate <= after);
        }
    }
}
