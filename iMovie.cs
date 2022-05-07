using System;

namespace MyDVDManagerApp
{
    //The specification of Movie ADT
    interface iMovie
    {
        string Title // get and set the tile of this movie
        {
            get;
            set;
        }
        string Genre //get and set the genre of this movie
        {
            get;
            set;
        }

        string Classification //get and set the classification of this movie
        {
            get;
            set;
        }

        int Duration //get and set the duration of this movie
        {
            get;
            set;
        }

        int AvailableCopies //get and set the number of the copies of this movie currently available to lend
        {
            get;
            set;
        }

        int NoBorrowings //get and set the number of times that this movie has been borrowed
        {
            get;
            set;
        }

        string ToString(); //return a string containning the title, genre, classification, duration, and the number of copies of this movie currently in the community library 
    }
    class Movie:iMovie
    {
        #region Fields
        private string title;
        private string genre;
        private string classification;
        private int duration;
        private int availableCopies;
        private int noBorrowings;
        #endregion

        #region Properties
        public string Title { get => title; set => title = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Classification { get => classification; set => classification = value; }
        public int Duration { get => duration; set => duration = value; }
        public int AvailableCopies { get => availableCopies; set => availableCopies = value; }
        public int NoBorrowings { get => noBorrowings; set => noBorrowings = value; }
        #endregion

        #region Constructors
        public Movie(string title)
        {
            this.Title = title;
        }      
        public Movie(string title, string genre, string classification, int duration, int availableQty)
        {
            this.Title = title;
            this.Genre = genre;
            this.Classification = classification;
            this.Duration = duration;
            this.AvailableCopies = availableQty;
            this.NoBorrowings = 0;
        }
        public Movie(string title, string genre, string classification, int duration, int availableQty, int noBorrowings) // use for generate data for Testing.
        {
            this.Title = title;
            this.Genre = genre;
            this.Classification = classification;
            this.Duration = duration;
            this.AvailableCopies = availableQty;
            this.NoBorrowings = noBorrowings;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return (Title + "\nGenre: " + Genre + ", Classification: " + Classification 
                + ", Duration: " + Duration + "minutes, Available Quantity: " + AvailableCopies + ", No. of borrowings: " + NoBorrowings + ".");
        }
        #endregion
    }
}
