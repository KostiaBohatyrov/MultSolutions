using System;

namespace MyLibrary
{
    public class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int PublicationYear { get; private set; }
        public bool IsAvailable { get; private set; }

        public Book(string title, string author, string isbn, int publicationYear)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            IsAvailable = true;
        }

        public void CheckOut()
        {
            if (!IsAvailable)
            {
                throw new InvalidOperationException("Book is already checked out.");
            }
            IsAvailable = false;
        }

        public void ReturnBook()
        {
            if (IsAvailable)
            {
                throw new InvalidOperationException("Book was not checked out.");
            }
            IsAvailable = true;
        }

        public void UpdatePublicationYear(int newYear)
        {
            if (newYear < 0)
            {
                throw new ArgumentException("Publication year cannot be negative.");
            }
            PublicationYear = newYear;
        }

        public override string ToString()
        {
            return $"{Title} by {Author} (ISBN: {ISBN}, Year: {PublicationYear}, Available: {IsAvailable})";
        }

        // Additional helper methods
        public bool IsClassic()
        {
            return PublicationYear < 1970;
        }
    }
}
