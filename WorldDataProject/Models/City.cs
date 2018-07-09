using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WorldDataProject;

namespace WorldDataProject.Models
{
    public class City
    {
        private int _id;
        private string _name;
        private string _countryCode;
        private string _district;
        private int _population;
        private string _filterValue;

        public City(int id, string name, string countryCode, string district, int population)
        {
            _id = id;
            _name = name;
            _countryCode = countryCode;
            _district = district;
            _population = population;
        }

        public City(string name, string filterValue)
        {
            _name = name;
            _filterValue = filterValue;
        }

        public int GetId()
        {
           return _id;
        }

        public void SetId(int id)
        {
           _id = id;
        }
        public string GetName()
        {
           return _name;
        }

        public void SetName(string name)
        {
           _name = name;
        }
        public string GetCountryCode()
        {
           return _countryCode;
        }

        public void SetCountryCode(string countryCode)
        {
           _countryCode = countryCode;
        }
        public string GetDistrict()
        {
           return _district;
        }

        public void SetDistrict(string district)
        {
            _district = district;
        }
        public int GetPopulation()
        {
           return _population;
        }

        public void SetPopulation(int population)
        {
           _population = population;
        }

        public string GetFilterValue()
        {
            return _filterValue;
        }

        public void SetFilterValue(string filterValue)
        {
            _filterValue = filterValue;
        }


        public static List<City> GetAll()
        {
            List<City> allCities = new List<City> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM city;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string countryCode = rdr.GetString(2);
                string district = rdr.GetString(3);
                int population = rdr.GetInt32(4);
                City newCity = new City(id, name, countryCode, district, population);
                allCities.Add(newCity);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCities;
        }

        public static List<City> FilterBy(string filterValue, string order)
        {
            List<City> filteredList = new List<City> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT name, " + filterValue + " FROM city ORDER BY " + filterValue + " " + order + ";";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string cityName = rdr.GetString(0);
                string selectedColumn = rdr.GetValue(1).ToString();
                City newCity = new City(cityName, selectedColumn);
                filteredList.Add(newCity);
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
