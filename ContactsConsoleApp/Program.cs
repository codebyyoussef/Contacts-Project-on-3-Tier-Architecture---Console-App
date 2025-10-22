using System;
using System.Data;
using ContactsBusinessLayer;

namespace ContactsConsoleApp
{
    internal class Program
    {
        static void testFindContact(int ID)
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

        static void testAddNewContanct()
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

        static void testUpdateContact(int ID)
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

        static void testDeleteContact(int ID)
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

        static void testListContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();

            Console.WriteLine("Contacts Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}, {row["FirstName"]}, {row["LastName"]}, {row["Phone"]}");
            }
        }

        static void testIsContactExist(int ID)
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

        //---Test Country Business

        static void testFindCountryByID(int ID)
        {
            clsCountry country = clsCountry.Find(ID);

            if (country != null)
            {
                Console.WriteLine($"CountryID: {country.ID}");
                Console.WriteLine($"CountryName: {country.Name}");
            }
            else
            {
                Console.WriteLine("Country with ID = " + ID + " not found!");
            }
        }

        static void testFindCountryByName(string countryName)
        {
            clsCountry country = clsCountry.Find(countryName);

            if (country != null)
            {
                Console.WriteLine($"CountryID: {country.ID}");
                Console.WriteLine($"CountryName: {country.Name}");
            }
            else
            {
                Console.WriteLine("Invalid country name. Ensure it exists in the database before proceeding.");
            }
        }

        static void testAddNewCountry(string countryName)
        {
            clsCountry country = new clsCountry();

            country.Name = countryName;

            if (country.Save())
            {
                Console.WriteLine("Country added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add country. Please try again.");
            }
        }

        static void testUpdateCountry(int ID)
        {
            clsCountry country = clsCountry.Find(ID);

            if (country != null)
            {
                // Update whatever info you want
                country.Name = "Morocco";

                if (country.Save())
                {
                    Console.WriteLine("Country information updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update country. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Update failed, country not found in the database.");
            }
        }

        static void testDeleteCountry(int ID)
        {
            if (clsCountry.IsCountryExsit(ID))
            {
                if (clsCountry.DeleteCountry(ID))
                {
                    Console.WriteLine("Country deleted Successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete country.");
                }
            }
            else
            {
                Console.WriteLine("No, country is not exist.");
            }
            
        }
       
        static void testListCountries()
        {
            DataTable dataTable = clsCountry.GetAllCountries();

            Console.WriteLine("Countries List:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["CountryID"]}, {row["CountryName"]}");
            }
        }

        static void testIsCountryExistByID(int ID)
        {
            if (clsCountry.IsCountryExsit(ID))
            {
                Console.WriteLine("Yes, country is exist.");
            }
            else
            {
                Console.WriteLine("No, country is not exist.");
            }
        }

        static void testIsCountryExistByName(string countryName)
        {
            if (clsCountry.IsCountryExsit(countryName))
            {
                Console.WriteLine("Yes, country is exist.");
            }
            else
            {
                Console.WriteLine("No, country is not exist.");
            }
        }

        static void Main(string[] args)
        {
            //testFindContact(2);
            //testAddNewContanct();
            //testUpdateContact(1);
            //testDeleteContact(8);
            //testListContacts();
            //testIsContactExist(1);

            //testFindCountryByID(1);
            //testFindCountryByName("Germany");
            //testAddNewCountry("Morocco");
            //testUpdateCountry(6);
            //testDeleteCountry(7);
            //testListCountries();
            testIsCountryExistByID(1);
            testIsCountryExistByName("Canada");


        }
    }
}
