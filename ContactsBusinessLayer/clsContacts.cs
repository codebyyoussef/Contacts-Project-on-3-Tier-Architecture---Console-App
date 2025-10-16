﻿using System;
using System.Data;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        private enum enMode { AddNew, Update};
        enMode Mode;

        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public int CountryID { set; get; }
        public string ImagePath { set; get; }

        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsContact(int ID, string FirstName, string LastName,
            string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }

        private bool _AddNewContact()
        {
            // Call Data Access Layer
            this.ID = clsContactDataAccess.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
            return this.ID != -1;
        }

        public static clsContact Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = 1;

            if (clsContactDataAccess.GetContactInfoByID(ID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address,
                                                ref DateOfBirth,  ref CountryID, ref ImagePath))
            {
                return new clsContact(ID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;
        }
    }
}
