using System;
using static System.Console;
using System.Linq;

namespace MyDVDManagerApp
{
    interface ToolLibrarySystem
    {
        void add(); // add DVDs of a new movie to the system      
        void add(string mvTitle, int quantity); //add new DVDs of an existing movie to the system
        void delete(iMovie aMovie); //remove a given movie from the system
        void add(iMember aMember); //add a new memeber to the system
        void delete(iMember aMember); //delete a member from the system 
        void getConnectPhone(string firstname, string lastname); //given a member's first name and last name, find the contact phone number of this member
        void displayAllMovies(); //display the information about all the movies in the library
        void displayOneMovie(string movieTitle); //display all the information about about amovie, given the title of the movie 
        void borrowMovie(iMember aMember, iMovie aMovie); //a member borrows a movie DVD from the library
        void returnMovie(iMember aMember, iMovie aMovie); //a member returns a movie DVD to the library
        void getMovieDVDs(iMember aMember); //get a list of movie DVDs that are currently held by a given member
        void getBorrowers(string movieTitle); //given the title of a movie, return all the members who are currently borrowing that movie
        void displayTop3(); //Display top three most frequently borrowed movies by the members in the library in the descending order by the number of times the movie has been borrowed.

        #region These Functions are used for Testing only!
        void addAuto(); // add automatically movies to the system, generating original database. Delete after Test.
        void addAutoMember(); // add automatically members to the system, generating original database. Delete after Test.
        void displayAllMember(); // display all membership in the database. Delete after Test.
        #endregion
    }
    class SystemLibrary : ToolLibrarySystem
    {
        private static int capacity = 29;
        private static iMovieCollection Collection = new MovieCollection(capacity);
        private static iMovie[] array;
        private static iMemberCollection MemberCollection = new MemberCollection();
        private static iMember[] memberInfoArray;

        public void addAuto()
        {
            Collection.addAuto("Avengers: Infinity War", "Sci-fi", "PG", 130, 10, 5);
            Collection.addAuto("Spider-Man", "Sci-fi", "PG", 108, 12, 8);
            Collection.addAuto("Star Wars: The Last Jedi", "Sci-fi", "PG", 102, 10, 6);
            Collection.addAuto("Furie", "Action", "MA15+", 90, 5, 3);
            Collection.addAuto("A Thousand Words", "Comedy", "MA15+", 100, 10, 5);
            Collection.addAuto("Insidious: The Last Key", "Thriller", "M", 92, 5, 4);
            Collection.addAuto("Mission Impossible: Ghost", "Action", "M", 105, 10, 7);
            Collection.addAuto("Grown Ups 2", "Family", "M", 95, 8, 4);
            Collection.addAuto("Minions", "Animated", "PG", 98, 12, 10);
            Collection.addAuto("Spirited Away", "Animated", "PG", 95, 12, 8);
            Collection.addAuto("Star Trek: Into the Darkness", "Adventure", "M", 102, 10, 9);
            Collection.addAuto("Jurassic World", "Adventure", "M", 105, 12, 8);

            array = Collection.toArray();

        }
        public void add()
        {
            Collection.add();
            array = Collection.toArray();
        }
        public void add(string title, int quantity)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Title == title)
                {
                    array[i].AvailableCopies += quantity;
                    WriteLine();
                    WriteLine($"Successfully update quantity for movie \"{title}\"");
                    break;
                }
            }
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public void delete(iMovie aMovie)
        {
            Collection.delete(aMovie);
            array = Collection.toArray();
        }
        public void addAutoMember()
        {
            iMember m1 = new Member("AAA", "hello", "091817173", 1234);
            MemberCollection.add(m1);

            iMember m2 = new Member("BBB", "hello", "092889222", 1234);
            MemberCollection.add(m2);

            iMember m3 = new Member("CCC", "hello", "0827918981", 1234);
            MemberCollection.add(m3);

            memberInfoArray = MemberCollection.toArray();
        }
        public void add(iMember aMember)
        {
            MemberCollection.add(aMember);
            memberInfoArray = MemberCollection.toArray();
        }
        public void delete(iMember aMember)
        {
            MemberCollection.delete(aMember);
            memberInfoArray = MemberCollection.toArray();
        }
        public void getConnectPhone(string firstname, string lastname)
        {
            iMember member = new Member(firstname, lastname);
            memberInfoArray = MemberCollection.toArray();
            bool memberFound = false;
            for (int i = 0; i < memberInfoArray.Length; i++)
            {
                if ((memberInfoArray[i].FirstName == member.FirstName) && (memberInfoArray[i].LastName == member.LastName))
                {
                    memberFound = true;
                    WriteLine();
                    WriteLine(memberInfoArray[i].ToString());
                    break;
                }
            }
            if (memberFound == false)
            {
                WriteLine();
                Write("Can not find this member in the system library. The member has not been registered.");
            }
            WriteLine();
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public void displayAllMovies()
        {
            WriteLine("All movies available in the library:");
            WriteLine();
            for (int i = 0; i < array.Length; ++i)
            {
                WriteLine(array[i].ToString());
                WriteLine("-----------------------------------------------------");
                WriteLine();
            }
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public void displayAllMember()
        {
            WriteLine();
            WriteLine("All registered members in the library:");
            WriteLine();
            for (int i = 0; i < memberInfoArray.Length; ++i)
            {
                WriteLine(memberInfoArray[i].ToString());
                WriteLine("-----------------------------------------------------");
                WriteLine();
            }
        }
        public void displayOneMovie(string title)
        {
            bool isMovieExist = false;
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i].Title == title)
                {
                    isMovieExist = true;
                    WriteLine();
                    WriteLine("Information about the movie:");
                    WriteLine();
                    WriteLine(array[i].ToString());
                    WriteLine();
                }
            }
            if (isMovieExist == false)
            {
                WriteLine();
                WriteLine("Sorry. We don\'t have this movie in the library system.");
            }
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public void borrowMovie(iMember aMember, iMovie aMovie)
        {
            bool memberFound = false;
            bool movieFound = false;
            if (memberInfoArray != null)
            {
                for (int i = 0; i < memberInfoArray.Length; i++)
                {
                    if ((memberInfoArray[i].FirstName == aMember.FirstName) && (memberInfoArray[i].LastName == aMember.LastName) && (memberInfoArray[i].Pin == aMember.Pin))
                    {
                        memberFound = true;                       
                        for (int j = 0; j < array.Length; j++)
                        {
                            if (array[j].Title == aMovie.Title)
                            {
                                memberInfoArray[i].addMovie(aMovie);
                                array[j].NoBorrowings++;
                                movieFound = true;
                                break;
                            }                           
                        }
                        if(movieFound == false)
                        {
                            WriteLine("\nThis movie does not exist in the library system. Try another movie.");
                        }
                        break;
                    }
                }
                if (memberFound == false)
                {
                    WriteLine("\nError. Can not borrow movie." +
                        "\nYou are not been registered membership yet." +
                        "\nOr, maybe you have entered wrong first/last name, or password. Try again.");
                }                
            }
            else
            {
                WriteLine("\nError. Membership list is empty!");
            }
            WriteLine("\nPress any key to return back to menu.");
            ReadKey();
        }
        public void returnMovie(iMember aMember, iMovie aMovie)
        {
            bool memberFound = false;
            bool movieFound = false;
            if (memberInfoArray != null)
            {
                for (int i = 0; i < memberInfoArray.Length; i++)
                {
                    if ((memberInfoArray[i].FirstName == aMember.FirstName) && (memberInfoArray[i].LastName == aMember.LastName) && (memberInfoArray[i].Pin == aMember.Pin))
                    {
                        memberFound = true;
                        for (int j = 0; j < memberInfoArray[i].GetBorrowingMovieDVDs.Length; j++)
                        {
                            if (memberInfoArray[i].GetBorrowingMovieDVDs[j] == aMovie.Title)
                            {
                                movieFound = true;
                                memberInfoArray[i].GetBorrowingMovieDVDs = memberInfoArray[i].GetBorrowingMovieDVDs.Except(new string[] { aMovie.Title }).ToArray();
                                WriteLine();
                                WriteLine("Successfully return movie DVD to library system.");
                                break;
                            }
                        }
                        if (movieFound == false)
                        {
                            WriteLine();
                            Write("Error. This movie has not been in your borrowing list, or wrong movie title. Try again.");
                        }
                        break;
                    }
                }
                if (memberFound == false)
                {
                    WriteLine("\nError. Can not return this movie." +
                        "\nYou are not been registered membership yet." +
                        "\nOr, maybe you have entered wrong first/last name, or password. Try again.");
                }
            }
            else
            {
                WriteLine("\nError. Membership list is empty!");
            }
            WriteLine();
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public void getMovieDVDs(iMember aMember)
        {
            bool memberFound = false;
            if (memberInfoArray != null)
            {
                for (int i = 0; i < memberInfoArray.Length; i++)
                {
                    if ((memberInfoArray[i].FirstName == aMember.FirstName) && (memberInfoArray[i].LastName == aMember.LastName) && (memberInfoArray[i].Pin == aMember.Pin))
                    {
                        memberFound = true;
                        WriteLine($"\nAll movies borrowed by {aMember.FirstName} {aMember.LastName}: ");
                        for (int j = 0; j < memberInfoArray[i].GetBorrowingMovieDVDs.Length; j++)
                        {
                            WriteLine(memberInfoArray[i].GetBorrowingMovieDVDs[j]);
                        }
                        break;
                    }
                }
                if (memberFound == false)
                {
                    WriteLine("\nError. Can not borrow movie." +
                       "\nYou are not been registered membership yet." +
                       "\nOr, maybe you have entered wrong first/last name, or password. Try again.");
                }
            }
            else
            {
                WriteLine("\nError. Membership list is empty!");
            }
            WriteLine("\nPress any key to return back to menu.");
            ReadKey();
        }
        public void getBorrowers(string movieTitle)
        {
            bool isFound = false;
            WriteLine($"\nSearching result for movie title:\"{movieTitle}\"");
            if (memberInfoArray != null)
            {
                for (int i = 0; i < memberInfoArray.Length; i++)
                {
                    for (int j = 0; j < memberInfoArray[i].GetBorrowingMovieDVDs.Length; j++)
                    {
                        if (memberInfoArray[i].GetBorrowingMovieDVDs[j] == movieTitle)
                        {
                            WriteLine();
                            WriteLine(memberInfoArray[i].ToString());
                            isFound = true;
                        }
                    }
                }
                if (isFound == false)
                {
                    WriteLine();
                    WriteLine("Not found. No member is keeping this movie DVD, or incorrect movie title. Try again.");
                }
            }
            else
            {
                WriteLine("\nError. Membership list is empty!");
            }
            WriteLine();
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public void displayTop3()
        {
            int[] topMovies = new int[array.Length];
            for (int i = 0; i < topMovies.Length; i++)
            {
                topMovies[i] = array[i].NoBorrowings;
            }
            HeapSort(topMovies);
            WriteLine("\nTop 3 borrowed movies: ");
            for (int i = topMovies.Length - 1; i > topMovies.Length - 4; i--)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j].NoBorrowings == topMovies[i])
                    {
                        WriteLine();
                        WriteLine(array[j].ToString());
                    }
                }
            }
            WriteLine("\nPress any key to return back to menu.");
            ReadKey();
        }
        #region Heap Sort - Functions for deployment
        // sort the elements in an array
        private static void HeapSort(int[] data)
        {
            //Use the HeapBottomUp procedure to convert the array, data, into a heap
            HeapBottomUp(data);

            //repeatly remove the maximum key from the heap and then rebuild the heap
            for (int i = 0; i <= data.Length - 2; i++)
            {
                MaxKeyDelete(data, data.Length - i);
            }
        }
        // convert a complete binary tree into a heap
        private static void HeapBottomUp(int[] data)
        {
            int n = data.Length;
            for (int i = (n - 1) / 2; i >= 0; i--)
            {
                int k = i;
                int v = data[i];
                bool heap = false;
                while ((!heap) && ((2 * k + 1) <= (n - 1)))
                {
                    int j = 2 * k + 1;  //the left child of k
                    if (j < (n - 1))   //k has two children
                        if (data[j] < data[j + 1])
                            j = j + 1;  //j is the larger child of k
                    if (v >= data[j])
                        heap = true;
                    else
                    {
                        data[k] = data[j];
                        k = j;
                    }
                }
                data[k] = v;
            }
        }
        //delete the maximum key and rebuild the heap
        private static void MaxKeyDelete(int[] data, int size)
        {
            //1. Exchange the root’s key with the last key K of the heap;
            int temp = data[0];
            data[0] = data[size - 1];
            data[size - 1] = temp;

            //2. Decrease the heap’s size by 1;
            int n = size - 1;

            //3. “Heapify” the complete binary tree.
            bool heap = false;
            int k = 0;
            int v = data[0];
            while ((!heap) && ((2 * k + 1) <= (n - 1)))
            {
                int j = 2 * k + 1; //the left child of k
                if (j < (n - 1))   //k has two children
                    if (data[j] < data[j + 1])
                        j = j + 1;  //j is the larger child of k
                if (v >= data[j])
                    heap = true;
                else
                {
                    data[k] = data[j];
                    k = j;
                }
            }
            data[k] = v;
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
            SystemLibrary Library = new SystemLibrary();
            Library.addAuto();
            

            bool isProgramQuit = false;
            bool isStaff = false;
            while (isProgramQuit == false)
            {
                bool isReturnMain = false;
                int choiceMain;
                DisplayMain();
                WriteLine("Enter your choice here >>> ");
                choiceMain = Int16.Parse(ReadLine());
                if (choiceMain == 1) { isStaff = checkStaff(); }
                while (isReturnMain == false)
                {
                    int choiceStaff;
                    int choiceMember;
                    #region Staff Menu
                    if (choiceMain == 1 && isStaff == true)
                    {
                        //Check for valid username and password;                        
                        Clear();
                        DisplayStaffMenu();
                        WriteLine();
                        WriteLine("Enter your choice here >>> ");
                        choiceStaff = Int16.Parse(ReadLine());
                        switch (choiceStaff)
                        {
                            case 0:
                                Clear();
                                DisplayMain();
                                WriteLine("Enter your choice here >>> ");
                                choiceMain = Int16.Parse(ReadLine());
                                isStaff = false;
                                if (choiceMain == 1) { isStaff = checkStaff(); }
                                break;
                            case 1:
                                Clear();
                                Library.add();
                                Clear();
                                break;
                            case 2:
                                Clear();
                                WriteLine("Input movie title you want to update it\'s quantity: ");
                                string title = ReadLine();
                                WriteLine("Input quantity: ");
                                int quantity = Int16.Parse(ReadLine());
                                Library.add(title, quantity);
                                break;
                            case 3:
                                Clear();
                                WriteLine("Input movie title you want to delete: ");
                                string mvtitle = ReadLine();
                                iMovie MovieToDel = new Movie(mvtitle);
                                Library.delete(MovieToDel);
                                break;
                            case 4:
                                Clear();
                                WriteLine("Register for a new member. Enter the followings >>>");
                                WriteLine("Input firstname: ");
                                string firstname = ReadLine();
                                WriteLine("Input lastname: ");
                                string lastname = ReadLine();
                                WriteLine("Input contact phone number: ");
                                string contactnum = ReadLine();
                                WriteLine("Input a 4-digit number password: ");
                                int password = Int32.Parse(ReadLine());
                                iMember newMember = new Member(firstname, lastname, contactnum, password);
                                Library.add(newMember);
                                //Library.displayAllMember(); 
                                break;
                            case 5:
                                Clear();
                                WriteLine("Remove a member. Enter the followings >>>");
                                WriteLine("Input firstname: ");
                                string firstnameToDel = ReadLine();
                                WriteLine("Input lastname: ");
                                string lastnameToDel = ReadLine();
                                iMember delMember = new Member(firstnameToDel, lastnameToDel);
                                Library.delete(delMember); 
                                //Library.displayAllMember(); 
                                break;
                            case 6:
                                Clear();
                                WriteLine("Want to find a member? Enter the followings >>>");
                                WriteLine("Input firstname: ");
                                string firstnameToFind = ReadLine();
                                WriteLine("Input lastname: ");
                                string lastnameToFind = ReadLine();
                                Library.getConnectPhone(firstnameToFind, lastnameToFind);
                                break;
                            case 7:
                                Clear();
                                WriteLine("Input the movie title to search for it\'s borrower: ");
                                string movietitle = ReadLine();
                                Library.getBorrowers(movietitle);
                                break;
                        }
                    }
                    #endregion

                    #region Member Menu
                    else if (choiceMain == 2)
                    {
                        //Check for valid member by their firstname, lastname and password;                       
                        Clear();
                        DisplayMemberMenu();
                        WriteLine();
                        WriteLine("Enter your choice here >>> ");
                        choiceMember = Int16.Parse(ReadLine());
                        switch (choiceMember)
                        {
                            case 0:
                                Clear();
                                DisplayMain();
                                WriteLine("Enter your choice here >>> ");
                                choiceMain = Int16.Parse(ReadLine());
                                isStaff = false;
                                if (choiceMain == 1) { isStaff = checkStaff(); }
                                break;
                            case 1:
                                Clear();
                                Library.displayAllMovies();
                                break;
                            case 2:
                                Clear();
                                WriteLine("Want to find a movie? Input it\'s title here: ");
                                string title = ReadLine();
                                Library.displayOneMovie(title);
                                break;
                            case 3:
                                Clear();
                                string firstname, lastname, moviename;
                                int password;
                                WriteLine("Input your first name: ");
                                firstname = ReadLine();
                                WriteLine("Input your last name: ");
                                lastname = ReadLine();
                                WriteLine("Input password: ");
                                password = Int32.Parse(ReadLine());
                                WriteLine("Input movie title: ");
                                moviename = ReadLine();
                                iMember renter = new Member(firstname, lastname, password);
                                iMovie rentMovie = new Movie(moviename);
                                Library.borrowMovie(renter, rentMovie);
                                break;
                            case 4:
                                Clear();
                                WriteLine("Input your first name: ");
                                string firstnameReturn = ReadLine();
                                WriteLine("Input your last name: ");
                                string lastnameReturn = ReadLine();
                                WriteLine("Input password: ");
                                int passwordReturn = Int32.Parse(ReadLine());
                                WriteLine("Input movie title to return: ");
                                string returnMVTitle = ReadLine();
                                iMovie returnMovie = new Movie(returnMVTitle);
                                iMember returnMember = new Member(firstnameReturn, lastnameReturn, passwordReturn);
                                Library.returnMovie(returnMember, returnMovie);
                                break;
                            case 5:
                                Clear();
                                WriteLine("Searching for current borrowings of a member.");
                                WriteLine("Input first name: ");
                                string firstnameSearch = ReadLine();
                                WriteLine("Input last name: ");
                                string lastnameSearch = ReadLine();
                                WriteLine("Input password: ");
                                int passwordSearch = Int32.Parse(ReadLine());
                                iMember MemberToSearch = new Member(firstnameSearch, lastnameSearch, passwordSearch);
                                Library.getMovieDVDs(MemberToSearch);
                                break;
                            case 6:
                                Clear();
                                Library.displayTop3();
                                break;
                        }
                    }
                    #endregion

                    // End Program.
                    else if (choiceMain == 0)
                    {
                        Clear();
                        WriteLine("End the program.");
                        ReadKey();
                        break;
                    }
                }
                isProgramQuit = true;
            }

        }

        public static void DisplayMain()
        {
            WriteLine("======================================================================");
            WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
            WriteLine("======================================================================");
            WriteLine();
            WriteLine("Main Menu");
            WriteLine("----------------------------------------------------------------------");
            WriteLine("Select the following:");
            WriteLine();
            WriteLine("1. Staff");
            WriteLine("2. Member");
            WriteLine("0. End the program");
            WriteLine();
        }
        public static void DisplayStaffMenu()
        {
            WriteLine();
            WriteLine("Staff Menu");
            WriteLine("----------------------------------------------------------------------");
            WriteLine();
            WriteLine("1. Add DVDs of a NEW movie to the system.");
            WriteLine("2. Add new DVDs of an EXISTING movie to the system.");
            WriteLine("3. Remove a movie from the system.");
            WriteLine("4. Register a new member to the system.");
            WriteLine("5. Remove a registered member from the system.");
            WriteLine("6. Find a member's contact phone number, given the member's name.");
            WriteLine("7. Find a member who is currently renting a particular movie.");
            WriteLine("0. Return to the main menu.");
            WriteLine();
        }
        public static void DisplayMemberMenu()
        {
            WriteLine();
            WriteLine("Member Menu");
            WriteLine("--------------------------------------------------------------------------------");
            WriteLine();
            WriteLine("1. Browse all the movie.");
            WriteLine("2. Display all the information about a movie, given the title of the movie.");
            WriteLine("3. Borrow a movie DVD.");
            WriteLine("4. Return a movie DVD.");
            WriteLine("5. List current borrowing DVDs.");
            WriteLine("6. Display top 3 movies rented by members.");
            WriteLine("0. Return to the main menu.");
            WriteLine();
        }
        public static bool checkStaff()
        {
            const string staffName = "staff";
            const string passWord = "today123";
            string name;
            string pass;
            bool isGoodName = false;
            bool isGoodPass = false;
            bool isStaffValid;
            Clear();
            WriteLine("For security purpose. Please Enter your staff username: ");
            name = ReadLine();
            if (name == staffName) isGoodName = true;
            WriteLine("Enter your password: ");
            pass = ReadLine();
            if (pass == passWord) isGoodPass = true;
            isStaffValid = isGoodName && isGoodPass;
            while (isStaffValid == false)
            {
                Clear();
                WriteLine("You have entered wrong username or password. Try again...");
                WriteLine("Enter username: ");
                name = ReadLine();
                WriteLine("Enter password: ");
                pass = ReadLine();
                if (name == staffName) isGoodName = true;
                if (pass == passWord) isGoodPass = true;
                isStaffValid = isGoodName && isGoodPass;
            }
            return isStaffValid;
        }       
    }
}
