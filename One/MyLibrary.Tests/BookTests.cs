using System;
using MyLibrary;
using Xunit;

namespace MyLibrary.Tests
{
    public class BookTests
    {
        [Fact]
        public void CheckOut_ChangesAvailability()
        {
            // Arrange
            var book = new Book("Test Title", "Test Author", "123456789", 2000);
            // Act
            book.CheckOut();
            // Assert
            Assert.False(book.IsAvailable);
        }

        [Fact]
        public void ReturnBook_ChangesAvailability()
        {
            // Arrange
            var book = new Book("Test Title", "Test Author", "123456789", 2000);
            book.CheckOut();
            // Act
            book.ReturnBook();
            // Assert
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void UpdatePublicationYear_SetsNewYear()
        {
            // Arrange
            var book = new Book("Test Title", "Test Author", "123456789", 2000);
            // Act
            book.UpdatePublicationYear(2020);
            // Assert
            Assert.Equal(2020, book.PublicationYear);
        }

        [Fact]
        public void IsClassic_ReturnsCorrectValue()
        {
            // Arrange
            var oldBook = new Book("Old Book", "Old Author", "987654321", 1960);
            var newBook = new Book("New Book", "New Author", "123450987", 2005);
            // Act & Assert
            Assert.True(oldBook.IsClassic());
            Assert.False(newBook.IsClassic());
        }

        // Additional tests to ensure method robustness
        [Fact]
        public void CheckOut_ThrowsWhenBookNotAvailable()
        {
            // Arrange
            var book = new Book("Test Title", "Test Author", "123456789", 2000);
            book.CheckOut();
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => book.CheckOut());
        }

        [Fact]
        public void ReturnBook_ThrowsWhenBookIsAlreadyAvailable()
        {
            // Arrange
            var book = new Book("Test Title", "Test Author", "123456789", 2000);
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => book.ReturnBook());
        }
    }
}
