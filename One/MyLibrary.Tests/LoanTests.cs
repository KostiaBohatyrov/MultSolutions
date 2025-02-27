using System;
using MyLibrary;
using Xunit;

namespace MyLibrary.Tests
{
    public class LoanTests
    {
        [Fact]
        public void Loan_CreatesValidLoan()
        {
            // Arrange
            var book = new Book("Loan Book", "Author", "555555", 2010);
            var member = new Member("Alice", 3);
            // Act
            var loan = new Loan(book, member, 14);
            // Assert
            Assert.Equal(book, loan.LoanedBook);
            Assert.Equal(member, loan.Borrower);
            Assert.False(loan.IsReturned);
        }


        [Fact]
        public void IsOverdue_ReturnsFalseForActiveLoanWithinDueDate()
        {
            // Arrange
            var book = new Book("Loan Book", "Author", "555555", 2010);
            var member = new Member("Alice", 3);
            var loan = new Loan(book, member, 14);
            // Act & Assert
            Assert.False(loan.IsOverdue());
        }

        [Fact]
        public void ExtendLoan_ExtendsDueDate()
        {
            // Arrange
            var book = new Book("Loan Book", "Author", "555555", 2010);
            var member = new Member("Alice", 3);
            var loan = new Loan(book, member, 14);
            DateTime originalDueDate = loan.DueDate;
            // Act
            loan.ExtendLoan(7);
            // Assert
            Assert.Equal(originalDueDate.AddDays(7), loan.DueDate);
        }

        [Fact]
        public void ExtendLoan_ThrowsForInvalidExtraDays()
        {
            // Arrange
            var book = new Book("Loan Book", "Author", "555555", 2010);
            var member = new Member("Alice", 3);
            var loan = new Loan(book, member, 14);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => loan.ExtendLoan(0));
        }

        [Fact]
        public void ReturnLoan_ThrowsIfAlreadyReturned()
        {
            // Arrange
            var book = new Book("Loan Book", "Author", "555555", 2010);
            var member = new Member("Alice", 3);
            var loan = new Loan(book, member, 14);
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => loan.ReturnLoan());
        }
    }
}
