using System;
using System.Data;
using System.Data.SqlClient;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsCountry
    {
        public enum enMode { AddNew, Update };
        enMode Mode;

        public int ID { get; set; }
        public string Name { get; set; }

        public clsCountry()
        {
            ID = -1;
            Name = "";
            Mode = enMode.AddNew;
        }

        private clsCountry(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
            Mode = enMode.Update;
        }

        public static clsCountry Find(int ID)
        {
            string countryName = "";

            if (clsCountryData.GetCountryInfoByID(ID, ref countryName))
            {
                return new clsCountry(ID, countryName);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID))
            {
                return new clsCountry(ID, CountryName);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewCountry()
        {
            this.ID = clsCountryData.AddNewCountry(this.Name);
            return this.ID != -1;
        }

        private bool _UpdateCountry()
        {
            return clsCountryData.UpdateCountry(this.ID, this.Name);
        }

        public static bool DeleteCountry(int ID)
        {
            return clsCountryData.DelteCountry(ID);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static bool IsCountryExsit(int ID)
        {
            return clsCountryData.IsCountryExist(ID);
        }

        public static bool IsCountryExsit(string countryName)
        {
            return clsCountryData.IsCountryExist(countryName);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateCountry();
                       
            }
            return false;
        }

    }
}
