using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WorldDataProject;

namespace WorldDataProject.Models
{
    public class Country
    {
        private string _code;
        private string _name;
        private string _continent;
        private int _population;
        private string _headOfState;
        private string _filterValue;

        public Country(string countryCode, string countryName, string countryContinent, int countryPopulation, string countryHeadOfState)
        {
            _code = countryCode;
            _name = countryName;
            _continent = countryContinent;
            _population = countryPopulation;
            _headOfState = countryHeadOfState;
        }

        public Country(string countryName, string filterValue)
        {
            _name = countryName;
            _filterValue = filterValue;
        }

        public string GetCode()
        {
            return _code;
        }

        public void SetCode(string code)
        {
            _code = code;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetContinent()
        {
            return _continent;
        }

        public void SetContinent(string continent)
        {
            _continent = continent;
        }

        public int GetPopulation()
        {
            return _population;
        }

        public void SetPopulation(int population)
        {
            _population = population;
        }

        public string GetHeadOfState()
        {
            return _headOfState;
        }

        public void SetHeadOfState(string headOfState)
        {
            _headOfState = headOfState;
        }

        public string GetColumn()
        {
            return _filterValue;
        }

        public void SetColumn(string filterValue)
        {
            _filterValue = filterValue;
        }

        public static List<Country> GetAll()
        {
            List<Country> allCountries = new List<Country> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM country;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string countryCode = rdr.GetString(0); //get 0th column
                string countryName = rdr.GetString(1); //get 1st column
                string countryContinent = rdr.GetString(2); //get 2nd column
                int countryPopulation = rdr.GetInt32(6); //get 6th column
                string countryHeadOfState = rdr.GetValue(12).ToString(); //converts null values to string so code doesn't break
                Country newCountry = new Country(countryCode, countryName, countryContinent, countryPopulation, countryHeadOfState);
                allCountries.Add(newCountry);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCountries;
        }

        public static List<Country> FilterBy(string filterValue)
        {
            List<Country> filteredList = new List<Country> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT name, " + filterValue + " FROM country;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string countryName = rdr.GetString(0);
                string selectedColumn = rdr.GetValue(1).ToString();
                Country newCountry = new Country(countryName, selectedColumn);
                filteredList.Add(newCountry);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return filteredList;
        }
    }
}
