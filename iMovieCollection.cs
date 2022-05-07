using System;
using static System.Console;
using System.Linq;

namespace MyDVDManagerApp
{
    interface iMovieCollection
    {
        int Number // get the number of movies in the community library
        {
            get;
        }
        void add(); // add a given movie to this tool collection
        void delete(iMovie aMovie); //delete a given movie from this movie collection
        void clear(); //remove all the movies in this movie collection
        bool search(iMovie aMovie); //search a given movie in this movie collection. Return true if this movie is in the movie collection; return false otherwise
        iMovie[] toArray(); //output the movies in this collection to an array of iMovies
        void addAuto(string title, string genre, string classification, int duration, int availableCopies, int noBorrowings); // Automatically add movies data for Testing only.
    }
    class MovieCollection : iMovieCollection
    {
        #region Fields
        private int number; //the number of key-and-value pairs (movies) currently stored in the hashtable
        private int buckets; //number of buckets
        private Movie[] table; //a table storing key-and-value pairs
        private string empty;
        private string deleted;
        #endregion

        #region Properties
        public int Number { get => number; set => number = value; }
        internal Movie EmptyObj { get; } = new Movie("///NO MOVIES///");
        internal Movie DeletedObj { get; } = new Movie("///MOVIE has been DELETED///");
        public string Empty { get => empty; set => empty = EmptyObj.Title; }
        public string Deleted { get => deleted; set => deleted = DeletedObj.Title; }
        #endregion

        #region Constructor
        public MovieCollection(int buckets)
        {
            if (buckets > 0)
                this.buckets = buckets;
            number = 0;
            table = new Movie[buckets];
            for (int i = 0; i < buckets; i++)
                table[i] = EmptyObj;
        }
        #endregion

        #region Methods
        private int Hashing(string title)
        {
            int key1 = title.Length % buckets;
            //int key2 = 11 - (key1 % 11);
            return (key1);
        }
        private int Find_Insertion_Bucket(string title)
        {
            int bucket = Hashing(title);
            int i = 0;
            int offset = 0;
            while ((i < buckets) &&
                (table[(bucket + offset) % buckets].Title != Empty) &&
                (table[(bucket + offset) % buckets].Title != Deleted))
            //++offset; //linear probing
            {
                i++;
                offset = i;
            }
            return (offset + bucket) % buckets;
        }
        public int Search(string title)
        {
            int bucket = Hashing(title);

            int i = 0;
            int offset = 0;
            while ((i < buckets) &&
                (table[(bucket + offset) % buckets].Title != title) &&
                (table[(bucket + offset) % buckets].Title != Empty))
            //offset++;// linear probing
            {
                i++;
                offset = i; // probing
            }
            if (table[(bucket + offset) % buckets].Title == title)
                return (offset + bucket) % buckets;
            else
                return -1;
        }
        public void add()
        {
            string title, genre, classification;
            int duration, availableCopies;
            WriteLine("Want to add a movie? Following these steps: ");
            WriteLine("Input movie TITLE >>> ");
            title = ReadLine();
            WriteLine("Input movie GENRE, it could be Drama, Action, Adventure, Sci-fi, Family, or Thriller >>> ");
            genre = ReadLine();
            WriteLine("Input movie CLASSIFICATION, it could be G, PG, M, or MA15+ >>> ");
            classification = ReadLine();
            WriteLine("Enter the movie duration in minutes >>> ");
            duration = Int32.Parse(ReadLine());
            WriteLine("Enter the number of copies for this movie >>> ");
            availableCopies = Int32.Parse(ReadLine());
            iMovie aMovie = new Movie(title, genre, classification, duration, availableCopies);

            // check the pre-condition
            if ((Number < table.Length) && (Search(aMovie.Title) == -1))
            {
                int bucket = Find_Insertion_Bucket(aMovie.Title);
                table[bucket] = (Movie)aMovie;
                number++;
                WriteLine();
                WriteLine("Successfully adding movie to the library system.");
            }
            else
                WriteLine("This movie has already been in the library system or the system capacity is full!");
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public iMovie[] toArray()
        {
            iMovie[] Arr = new Movie[buckets];
            for (int i = 0; i < buckets; ++i)
            {
                Arr[i] = table[i];
            }
            Arr = Arr.Except(new Movie[] { EmptyObj }).ToArray();
            Arr = Arr.Except(new Movie[] { DeletedObj }).ToArray();
            return Arr;
        }        
        public bool search(iMovie aMovie)
        {
            bool isExist = false;
            for (int i = 0; i < buckets; i++)
            {
                if (table[i].Title == aMovie.Title)
                {
                    isExist = true;
                }
            }
            return isExist;
        }
        public void delete(iMovie aMovie)
        {
            int bucket = Search(aMovie.Title);
            if (bucket != -1)
            {
                table[bucket] = DeletedObj;
                number--;
                WriteLine();
                WriteLine($"Successfully delete movie \"{aMovie.Title}\".");
            }
            else
                Console.WriteLine("The given movie is not in the library system.");
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public void clear()
        {
            number = 0;
            for (int i = 0; i < buckets; i++)
                table[i] = EmptyObj;
        }
        public void addAuto(string title, string genre, string classification, int duration, int availableCopies, int noBorrowings)
        {
            iMovie aMovie = new Movie(title, genre, classification, duration, availableCopies, noBorrowings);
            if ((Number < table.Length) && (Search(aMovie.Title) == -1))
            {
                int bucket = Find_Insertion_Bucket(aMovie.Title);
                table[bucket] = (Movie)aMovie;
                number++;
            }
        } //This Function is used for generating data for Testing only.
        #endregion
    }
}
