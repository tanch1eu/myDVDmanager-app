using System;
using System.Linq;
using static System.Console;

namespace MyDVDManagerApp
{
    //The specification of MemberCollection ADT, which is used to store and manipulate a collection of members
    interface iMemberCollection
    {
        int Number // get the number of members in the community library
        {
            get;
        }
        void add(iMember aMember); //add a new member to this member collection, make sure there are no duplicates in the member collection
        bool search(iMember aMember); //search a given member in this member collection. Return true if this memeber is in the member collection; return false otherwise
        void delete(iMember aMember); //delete a given member from this member collection, a member can be deleted only when the member currently is not holding any movie DVD
        void clear(); // remove all the members in this member collection
        iMember[] toArray(); //output the members into an array of iMember
    }
    class MemberCollection : iMemberCollection
    {
        #region Fields
        private iMember[] members;
        private int number;
        #endregion

        #region Properties
        public int Number { get => number; set => number = value; }
        internal Member EmptyObj { get; } = new Member("*empty", "*empty");
        #endregion

        #region Constructor
        public MemberCollection()
        {
            members = new Member[20];
            number = 0;
            for (int i = 0; i < 20; i++)
                members[i] = EmptyObj;
                //members[i] = new Member();
        }
        #endregion

        #region Methods
        public void add(iMember aMember)
        {
            bool memberExist = search(aMember);
            if (memberExist == false)
            {
                members[number] = aMember;
                number++;
                WriteLine();
                WriteLine("Successfully registering member to the library system.");         
            }
            else
            {
                WriteLine();
                WriteLine("Error. Repeated name. The member has been already registered in the library system.");
            }
            WriteLine("Press any key to return back to menu.");
            ReadKey();
        }
        public bool search(iMember aMember)
        {
            bool isFounded = false;
            for (int i = 0; i < number; i++)
            {
                if ((members[i].FirstName == aMember.FirstName) && (members[i].LastName == aMember.LastName))
                {
                    isFounded = true;
                    break;
                }
            }
            return isFounded;
        }
        public void delete(iMember aMember)
        {           
            bool ismemberExist = search(aMember);
            bool hasBorrowing = false;
            if (ismemberExist == true)
            {
                for(int i = 0; i < members.Length; i++)
                {
                    if((members[i].FirstName == aMember.FirstName)&&(members[i].LastName == aMember.LastName))
                    {                       
                        for(int j = 0; j < 5; j++)
                        {
                            if (members[i].GetBorrowingMovieDVDs[j] != null)
                            {
                                hasBorrowing = true;break;
                            }
                        }
                        if (hasBorrowing == false)
                        {
                            members[i] = EmptyObj;
                            WriteLine("\nSuccessfully remove member from the library system.");
                            members = members.Except(new Member[] { EmptyObj }).ToArray();
                            number--;
                        }
                        else
                        {
                            WriteLine("Error. Can not remove. This member has not return their borrowings.");
                        }
                    }
                }
            }
            else
            {
                WriteLine("Sorry. We don\'t have this member in the library system.");
            }
            WriteLine("\nPress any key to return back to menu.");
            ReadKey();
        }
        public iMember[] toArray()
        {
            iMember[] Arr = new Member[number];
            for (int i = 0; i < number; i++)
            {
                Arr[i] = members[i];
            }
            return Arr;
        }
        public void clear()
        {
            number = 0;
            for (int i = 0; i < 20; i++)
                members[i] = EmptyObj;
        } // No-use function, not exist in the system menu/sub-menu.
        #endregion
    }
}