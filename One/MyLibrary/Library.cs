using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary
{
    public class Library
    {
        private List<Book> _books;
        private List<Member> _members;
        private List<Loan> _loans;

        public Library()
        {
            _books = new List<Book>();
            _members = new List<Member>();
            _loans = new List<Loan>();
        }

        public void AddBook(Book book)
        {
            if (_books.Any(b => b.ISBN == book.ISBN))
            {
                throw new ArgumentException("A book with the same ISBN already exists.");
            }
            _books.Add(book);
        }

        public void AddMember(Member member)
        {
            if (_members.Any(m => m.MemberId == member.MemberId))
            {
                throw new ArgumentException("A member with the same ID already exists.");
            }
            _members.Add(member);
        }

        public Loan LoanBook(string isbn, int memberId, int loanDurationDays)
        {
            Book book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null)
            {
                throw new ArgumentException("Book not found.");
            }
            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("Book is not available for loan.");
            }
            Member member = _members.FirstOrDefault(m => m.MemberId == memberId);
            if (member == null)
            {
                throw new ArgumentException("Member not found.");
            }
            member.BorrowBook(book);
            Loan loan = new Loan(book, member, loanDurationDays);
            _loans.Add(loan);
            return loan;
        }

        public void ReturnBook(string isbn, int memberId)
        {
            Loan loan = _loans.FirstOrDefault(l => l.LoanedBook.ISBN == isbn && l.Borrower.MemberId == memberId && !l.IsReturned);
            if (loan == null)
            {
                throw new ArgumentException("Active loan not found for this book and member.");
            }
            loan.ReturnLoan();
        }

        public IEnumerable<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable);
        }

        public IEnumerable<Loan> GetActiveLoans()
        {
            return _loans.Where(l => !l.IsReturned);
        }

        // Additional functionalities for reporting
        public string GetSummary()
        {
            int totalBooks = _books.Count;
            int availableBooks = _books.Count(b => b.IsAvailable);
            int totalMembers = _members.Count;
            int activeLoans = _loans.Count(l => !l.IsReturned);
            return $"Library Summary: Total Books: {totalBooks}, Available: {availableBooks}, Total Members: {totalMembers}, Active Loans: {activeLoans}";
        }
    }
}
