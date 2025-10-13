using System;
using System.Data;
using ContactsBusinessLayer;

namespace ContactsConsoleApp
{
    internal class Program
    {
        static void TestFindContacts(int ID)
        {
            clsContact contact = clsContact.Find(ID);

            if (contact != null)
            {
                Console.WriteLine(contact.FirstName + " " + contact.LastName);
                Console.WriteLine(contact.Email);
                Console.WriteLine(contact.Phone);
                Console.WriteLine(contact.Address);
                Console.WriteLine(contact.DateOfBirth);
                Console.WriteLine(contact.CountryID);
                Console.WriteLine(contact.ImagePath);
            }
            else
            {
                Console.WriteLine("Contact [" + ID + "] not found!");
            }
        }

        static void Main(string[] args)
        {
            TestFindContacts(55);
        }
    }
}
