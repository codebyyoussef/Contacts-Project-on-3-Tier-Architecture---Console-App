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
        public string Code { get; set; }
        public string PhoneCode { get; set; }

        public clsCountry()
        {
            ID = -1;
            Name = "";
            Code = "";
            PhoneCode = "";
            Mode = enMode.AddNew;
        }

        private clsCountry(int ID, string Name, string Code, string PhoneCode)
        {
            this.ID = ID;
            this.Name = Name;
            this.Code = Code;
            this.PhoneCode = PhoneCode;
            Mode = enMode.Update;
        }

        public static clsCountry Find(int ID)
        {
            string countryName = "", code = "", phoneCode = "";

            if (clsCountryData.GetCountryInfoByID(ID, ref countryName, ref code, ref phoneCode))
            {
                return new clsCountry(ID, countryName, code, phoneCode);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string countryName)
        {
            int ID = -1;
            string code = "", phoneCode = "";
            if (clsCountryData.GetCountryInfoByName(countryName, ref ID, ref code, ref phoneCode))
            {
                return new clsCountry(ID, countryName, code, phoneCode);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewCountry()
        {
            this.ID = clsCountryData.AddNewCountry(this.Name, this.Code, this.PhoneCode);
            return this.ID != -1;
        }

        private bool _UpdateCountry()
        {
            return clsCountryData.UpdateCountry(this.ID, this.Name, this.Code, this.PhoneCode);
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
