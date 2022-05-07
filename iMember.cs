using System;
using static System.Console;

namespace MyDVDManagerApp
{
    //The specification of Member ADT
    interface iMember
    {
        string FirstName  //get and set the first name of this member
        {
            get;
            set;
        }
        string LastName //get and set the last name of this member
        {
            get;
            set;
        }
        string ContactNumber //get and set the contact number of this member
        {
            get;
            set;
        }
        int Pin //get and set a four-digit pin number
        {
            get;
            set;
        }
        string[] GetBorrowingMovieDVDs //get a list of movies that this member is currently borrowing
        {
            get;
            set;
        }
        void addMovie(iMovie aMovie); //add a given movie DVD to the list of movies DVDs that this member is currently holding
        void deleteMovie(iMovie aMovie); //delete a given movie DVD from the list of movie DVDs that this member is currently holding
        string ToString(); // return a string containing the first name, last name and contact number of this memeber
    }
    class Member:iMember
    {
        #region Fields
        private string firstName;
        private string lastName;
        private string contactNumber;
        private int pin;
        private string[] getBorrowingMovieDVDs;
        #endregion

        #region Properties
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public int Pin { get => pin; set => pin = value; }
        public string[] GetBorrowingMovieDVDs { get => getBorrowingMovieDVDs; set => getBorrowingMovieDVDs = value; }
        #endregion

        #region Constructors
        public Member(string firstname, string lastname, string contactnumber, int password)
        {
            FirstName = firstname;
            LastName = lastname;
            ContactNumber = contactnumber;
            Pin = password;
            GetBorrowingMovieDVDs = new string[5]; // a registered member can only borrow 5 movies.
            
        }
        public Member(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
        public Member(string firstname, string lastname, int password)
        {
            FirstName = firstname;
            LastName = lastname;
            Pin = password;
        }
        #endregion

        #region Methods
        public void addMovie(iMovie aMovie)
        {
            bool is5MoviesBorrowed = true;
            for (int i = 0; i < GetBorrowingMovieDVDs.Length; i++)
            {
                if (GetBorrowingMovieDVDs[i] == aMovie.Title)
                {
                    WriteLine();
                    WriteLine("Sorry. You have already borrowed this movie.");
                    is5MoviesBorrowed = false;
                    break;
                }
                else if (GetBorrowingMovieDVDs[i] == null)
                {
                    GetBorrowingMovieDVDs[i] = aMovie.Title;
                    is5MoviesBorrowed = false;
                    WriteLine();
                    WriteLine($"Successfully borrow movie DVD: {aMovie.Title}, quantity: 1."); //user can only borrow 1 DVD at a time.
                    break;
                }               
            }
            if (is5MoviesBorrowed == true)
            {
                WriteLine();
                WriteLine("Sorry. You have already borrowed 5 movies. Please return one of them, then try again.");
            }
        }
        public void deleteMovie(iMovie aMovie)
        {
            bool foundMovie = false;
            for (int i = 0; i < GetBorrowingMovieDVDs.Length; i++)
            {
                if (GetBorrowingMovieDVDs[i] == aMovie.Title)
                {
                    GetBorrowingMovieDVDs[i] = null;
                    foundMovie = true;
                    break;
                }               
            }
            if (foundMovie == false)
            {
                WriteLine("Error. This movie is not in your borrowings list. Try another movie.");
            }
        } // the program doesn't use this function to return movie DVD.
        public override string ToString()
        {
            return ("First Name: " + FirstName + "\nLast Name: " + LastName + "\nPhone Number: " + ContactNumber);
        }
        #endregion

    }
}
