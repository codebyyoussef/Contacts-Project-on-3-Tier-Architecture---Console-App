using System;
using System.Data;
using ContactsBusinessLayer;

namespace ContactsConsoleApp
{
    internal class Program
    {
        static void FindContact(int ID)
        {
            clsContact contact = clsContact.Find(ID);

            if (contact != null)
            {
                Console.WriteLine($"FirstName: {contact.FirstName}");
                Console.WriteLine($"LastName: {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone: {contact.Phone}");
                Console.WriteLine($"Address: {contact.Address}");
                Console.WriteLine($"DateOfBirth: {contact.DateOfBirth}");
                Console.WriteLine($"CountryID: {contact.CountryID}");
                Console.WriteLine($"ImagePath: {contact.ImagePath}");
            }
            else
            {
                Console.WriteLine("Contact [" + ID + "] not found!");
            }
        }

        static void AddNewContanct()
        {
            clsContact contact = new clsContact();

            contact.FirstName = "Youssef";
            contact.LastName = "El Hassani";
            contact.Email = "Youssef@gmail.com";
            contact.Phone = "0694384213";
            contact.Address = "Hay sofia Marrakesh";
            contact.DateOfBirth = new DateTime(2003, 3, 7);
            contact.CountryID = 1;
            contact.ImagePath = "";

            if (contact.Save())
            {
                Console.WriteLine("Contact added successfully with ID = " + contact.ID);
            }
        }

        static void Main(string[] args)
        {
            //FindContact(2);
            AddNewContanct();
        }
    }
}
