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

        static void UpdateContact(int ID)
        {
            clsContact contact = clsContact.Find(ID);

            if (contact != null)
            {
                contact.FirstName = "Samira";
                contact.LastName = "El Fadil";
                contact.Email = "Samira@gmail.com";
                contact.Phone = "0687546321";
                contact.Address = "Sidi Mbarek Marrakesh";
                contact.DateOfBirth = new DateTime(1978, 1, 1);
                contact.CountryID = 2;
                contact.ImagePath = "";

                if (contact.Save())
                {
                    Console.WriteLine("Contact updated successfully.");
                }
            }
            else
            {
                Console.WriteLine("Failed to update contact.");
            }
        }

        static void DeleteContact(int ID)
        {
            if (clsContact.IsContactExist(ID))
            {
                if (clsContact.DeleteContact(ID))
                {
                    Console.WriteLine("Contact deleted Successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete contact.");
                }
            }
            else
            {
                Console.WriteLine("The contact with ID = " + ID + " is not exist.");
            }
        }

        static void ListContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();

            Console.WriteLine("Contacts Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}, {row["FirstName"]}, {row["LastName"]}, {row["Phone"]}");
            }
        }

        static void IsContactExist(int ID)
        {
            if (clsContact.IsContactExist(ID))
            {
                Console.WriteLine("Yes, contact is exist.");
            }
            else
            {
                Console.WriteLine("No, contact is not exist.");
            }
        }

        static void Main(string[] args)
        {
            //FindContact(2);
            //AddNewContanct();
            //UpdateContact(1);
            //DeleteContact(8);
            //ListContacts();
            IsContactExist(1);
        }
    }
}
