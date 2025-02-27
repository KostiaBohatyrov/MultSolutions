using System;

namespace MyLibrary
{
    public class Loan
    {
        public Book LoanedBook { get; private set; }
        public Member Borrower { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool IsReturned { get; private set; }

        public Loan(Book book, Member member, int loanDurationDays)
        {
            if (loanDurationDays <= 0)
            {
                throw new ArgumentException("Loan duration must be positive.");
            }
            LoanedBook = book;
            Borrower = member;
            LoanDate = DateTime.Now;
            DueDate = LoanDate.AddDays(loanDurationDays);
            IsReturned = false;
        }

        public void ReturnLoan()
        {
            if (IsReturned)
            {
                throw new InvalidOperationException("Loan is already returned.");
            }
            LoanedBook.ReturnBook();
            IsReturned = true;
        }

        public bool IsOverdue()
        {
            return !IsReturned && DateTime.Now > DueDate;
        }

        public override string ToString()
        {
            return $"Loan: {LoanedBook.Title} loaned to {Borrower.Name} on {LoanDate.ToShortDateString()} (Due: {DueDate.ToShortDateString()}, Returned: {IsReturned})";
        }

        // Additional functionality: extend loan duration
        public void ExtendLoan(int extraDays)
        {
            if (extraDays <= 0)
            {
                throw new ArgumentException("Extra days must be positive.");
            }
            if (IsReturned)
            {
                throw new InvalidOperationException("Cannot extend a returned loan.");
            }
            DueDate = DueDate.AddDays(extraDays);
        }
    }
}
