using System;
using System.Collections.Generic;

namespace MyLibrary
{
    public class Member
    {
        public string Name { get; private set; }
        public int MemberId { get; private set; }
        public DateTime MembershipDate { get; private set; }
        private List<Book> _borrowedBooks;

        public Member(string name, int memberId)
        {
            Name = name;
            MemberId = memberId;
            MembershipDate = DateTime.Now;
            _borrowedBooks = new List<Book>();
        }

        public void BorrowBook(Book book)
        {
            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("Book is not available for borrowing.");
            }
            book.CheckOut();
            _borrowedBooks.Add(book);
        }

        public void ReturnBook(Book book)
        {
            if (!_borrowedBooks.Contains(book))
            {
                throw new InvalidOperationException("This book was not borrowed by the member.");
            }
            book.ReturnBook();
            _borrowedBooks.Remove(book);
        }

        public IReadOnlyList<Book> GetBorrowedBooks()
        {
            return _borrowedBooks.AsReadOnly();
        }

        public override string ToString()
        {
            return $"Member: {Name} (ID: {MemberId}, Since: {MembershipDate.ToShortDateString()})";
        }

        // Additional utility methods
        public int TotalBorrowedBooks()
        {
            return _borrowedBooks.Count;
        }
    }
}
