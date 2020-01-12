using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project1
{
    interface IPersonLoader
    {
        List<Person> LoadPeople();
    }

    public class FileBasedPersonLoader : IPersonLoader
    {

        
    }

    class Program
    {
        static void Main()
        {
            var people = LoadPeople();
            GetAnswers(people);
        }

        static void LoadPeople()
        {
            var rows = GetRowsFromFile(@"C:\Users\brlogan\Downloads\namedata\namedata.csv");
            var splitData = GetSplitPartsFromAllRows(rows);  //returns string[]
            var people = ConvertAllDataIntoAllPersons(splitData);
        }

        static void GetAnswers(List<Person> people)
        {
            var populationByState = GetPeopleByState(people);
            var vehicles = GetAllVehiclesFromPeople(people);

            var answer1 = CountTotalPeople(people);
            Console.WriteLine("There are {0} people in this file.", answer1);
            var answer2 = CountPiecesOfData(splitData);
            Console.WriteLine("There are {0} pieces of data in this file.", answer2);
            var answer3 = CountPeopleWhereGenderIsMale(people);
            Console.WriteLine("There are {0} males.", answer3);
            var answer4 = CountPeopleWhereTitleIsMrs(people);
            Console.WriteLine("There are {0} married women.", answer4);
            var answer5 = GetMostPopulousState(populationByState);
            Console.WriteLine("The most populous state is {0}.", answer5);
            var answer6 = GetHeaviestPerson(people);
            Console.WriteLine("The heaviest person is {0} {1} at {2} lbs.", answer6.GivenName, answer6.Surname, answer6.Pounds);
            var answer7 = GetLightestPerson(people);
            Console.WriteLine("The lightest person is {0} {1} at {2} lbs.", answer7.GivenName, answer7.Surname, answer7.Pounds);
            var answer8 = GetOldestPerson(people);
            Console.WriteLine("The oldest person is {0} {1} at {2} years.", answer8.GivenName, answer8.Surname, answer8.Pounds);
            var answer9 = GetMostPopularVehicle(vehicles);
            Console.WriteLine("The most popular vehicle is the {0} {1} {2}.", answer9.Year, answer9.Manufacturer, answer9.Model);
            var answer10 = GetMinOrMaxWeight(people, true);   //true for min, false for max
            Console.WriteLine("{0} {1} weighs {2} lbs.", answer10.GivenName, answer10.Surname, answer10.Pounds);

            Console.ReadLine();
        }

        /* code to build objects from dataset goes here */


        private static Gender GetGenderFromString(string gender)
        {
            Gender g;
            if (gender == "male")
            {
                g = Gender.Male;
            }
            else if (gender == "female")
            {
                g = Gender.Female;
            }
            else
            {
                g = Gender.AttackHelicopter;
            }
            return g;
        }

        private static Person ConvertPartsIntoOnePerson(string[] parts)
        {
            var p = new Person();

            p.Number = int.Parse(parts[0]);
            p.Gender = GetGenderFromString(parts[1]);
            p.NameSet = parts[2];
            p.Title = parts[3];
            p.GivenName = parts[4];
            p.MiddleInitial = parts[5];
            p.Surname = parts[6];
            p.StreetAddress = parts[7];
            p.City = parts[8];
            p.State = parts[9];
            p.StateFull = parts[10];
            p.ZipCode = parts[11];
            p.Country = parts[12];
            p.CountryFull = parts[13];
            p.EmailAddress = parts[14];
            p.Username = parts[15];
            p.Password = parts[16];
            //blank column 17
            p.TelephoneNumber = parts[18];
            p.TelephoneCountryCode = parts[19];
            p.MothersMaiden = parts[20];
            p.Birthday = DateTime.Parse(parts[21]);
            p.Age = int.Parse(parts[22]);
            p.TropicalZodiac = parts[23];
            p.CCType = parts[24];
            p.CCNumber = parts[25];
            p.CVV2 = parts[26];
            p.CCExpires = parts[27];
            p.NationalID = parts[28];
            p.UPS = parts[29];
            p.WesternUnionMTCN = parts[30];
            p.MoneyGramMTCN = parts[31];
            p.Color = parts[32];
            p.Occupation = parts[33];
            p.Company = parts[34];
            p.Vehicle = parts[35];
            p.Domain = parts[36];
            p.BloodType = parts[37];
            p.Pounds = float.Parse(parts[38]);
            p.Kilograms = float.Parse(parts[39]);
            p.FeetInches = parts[40];
            p.Centimeters = float.Parse(parts[41]);
            p.GUID = Guid.Parse(parts[42]);
            p.Latitude = float.Parse(parts[43]);
            p.Longitude = float.Parse(parts[44]);
            
            return p;
        }

        private static List<Person> ConvertAllDataIntoAllPersons(List<string[]> splitData)
        {
            var allPersons = new List<Person>();
            var row = 0;
            foreach (var data in splitData)
            {
                if (row != 0)
                {
                    allPersons.Add(ConvertPartsIntoOnePerson(data));
                }
                else
                {
                    continue;
                }
                row += 1;
            }
            return allPersons;
        }

        private static Dictionary<string, List<Person>> GetPeopleByState(List<Person> people)
        {
            var dictionary = new Dictionary<string, List<Person>>();
            foreach (var person in people)
            {
                if (dictionary.ContainsKey(person.State))
                {
                    dictionary[person.State].Add(person);
                }
                else
                {
                    var stateKeyList = new List<Person>
                    {
                        person  // alt: stateKeyList.Add(person);
                    };
                    dictionary.Add(person.State,stateKeyList);
                }
            }
            return dictionary;
        }

        private static Vehicle GetVehicleFromString(string vehicle)
        {
            var parts = vehicle.Split(new[] { ' ' }, 3);
            var v = new Vehicle();
                v.Year = int.Parse(parts[0]);
                v.Manufacturer = parts[1];
                v.Model = parts[2];
            return v;
        }

        private static List<Vehicle> GetAllVehiclesFromPeople(List<Person> people)
        {
            var allVehicles = new List<Vehicle>();
            foreach (var person in people)
            {
                allVehicles.Add(GetVehicleFromString(person.Vehicle));
            }
            return allVehicles;
        }

        private static Dictionary<string, List<Vehicle>> GetVehicleDictionary(List<Vehicle> vehicles)
        {
            return null;
        }

        /* code to parse objects into answers goes below */

        private static Vehicle GetMostPopularVehicle(List<Vehicle> vehicles)
        {
            var vehicleDictionary = new Dictionary<int, List<Vehicle>>();
            Vehicle mostPopularVehicle = null;
            var vehicleCount = 0;
            foreach(var vehicle in vehicles)
            {
                if (vehicleDictionary.ContainsKey(vehicleCount))
                {
                    vehicleDictionary[vehicleCount].Add(vehicle);
                }
                else
                {
                    var newvehicle = new List<Vehicle>
                    {
                        vehicle
                    };
                    vehicleDictionary.Add(vehicleCount, newvehicle);
                    vehicleCount += 1;
                }
            }

            var highestVehicleCount = 0;
            foreach(var kvp in vehicleDictionary)
            {
                var currentVehicleNumber = kvp.Key;
                vehicleCount = kvp.Value.Count();
                if (vehicleCount > highestVehicleCount)
                {
                    highestVehicleCount = vehicleCount;
                    mostPopularVehicle = kvp.Value[0];
                }
                else
                {
                    continue;
                }
            }
            return mostPopularVehicle;
        }

        private static Person GetOldestPerson(List<Person> people)
        {
            Person oldestPerson = null;
            var highestAge = 0;
            foreach (var person in people)
            {
                if (person.Age > highestAge)
                {
                    oldestPerson = person;
                    highestAge = person.Age;
                }
                else
                {
                    continue;
                }
            }
            return oldestPerson;
        }

        private static Person GetMinOrMaxWeight(List<Person> people, bool min)
        {
            Person myPerson = null;         //null is a perfectly cromulent default value.
            var lowestWeight = 1400.1;     //Guiness Record holder Jon Brower Minnoch was estimated to weigh at most 1,400 lbs.
            var highestWeight = 0.0;      //a person cannot weigh less than 0.0 lbs on Earth.
            {
                foreach (var person in people)
                {
                    if (min == true)
                    {
                        if (person.Pounds < lowestWeight)
                        {
                            myPerson = person;
                            lowestWeight = person.Pounds;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (person.Pounds > highestWeight)
                        {
                            myPerson = person;
                            highestWeight = person.Pounds;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return myPerson;
            }
        }

        private static Person GetLightestPerson(List<Person> people)
        {
            Person lightestPerson = null;
            var lowestWeight = 1400.1;   //Guiness Record holder Jon Brower Minnoch was estimated to weigh 1,400 lbs at his most obese.
            foreach (var person in people)
            {
                if (person.Pounds < lowestWeight)
                {
                    lightestPerson = person;
                    lowestWeight = person.Pounds;
                }
                else
                {
                    continue;
                }
            }
            return lightestPerson;
        }

        private static Person GetHeaviestPerson(List<Person> people)
        {
            Person heaviestPerson = null;   //null is a perfectly cromulent default value
            var highestWeight = 0.0;
            foreach(var person in people)
            {
                if (person.Pounds > highestWeight)
                {
                    heaviestPerson = person;
                    highestWeight = person.Pounds;
                }
                else
                {
                    continue;
                }
            }
            return heaviestPerson;
        }

        private static string GetMostPopulousState(Dictionary<string, List<Person>> populationByState)
        {
            var mostPopulatedState = "";
            var highestPopulation = 0;
            foreach(var kvp in populationByState)
            {
                var currentState = kvp.Key;
                var population = kvp.Value.Count();
                if (population > highestPopulation)
                {
                    mostPopulatedState = currentState;
                    highestPopulation = population;
                }
                else
                {
                    continue;
                }
            }
            return mostPopulatedState;
        }

        private static int CountPeopleWhereTitleIsMrs(List<Person> people)
        {
            var mrs = 0;
            foreach(var person in people)
            {
                if (person.Title == "Mrs.")
                {
                    mrs += 1;
                }
                else
                {
                    continue;
                }
            }
            return mrs;
        }

        private static int CountPeopleWhereGenderIsMale(List<Person> people)
        {
            var males = 0;
            foreach(var person in people)
            {
                if (person.Gender == Gender.Male)
                {
                    males += 1;
                }
                else
                {
                    continue;
                }
            }
            return males;
        }

        private static int CountPiecesOfData(List<string[]> splitData)
        {
            var totalPieces = 0;
            var row = 0;
            foreach(var data in splitData)
            {
                if(row != 0)
                {
                    totalPieces += data.Count();
                }
                else
                {
                    continue;
                }
                row += 1;
            }
            return totalPieces;
        }

        private static int CountTotalPeople(List<Person> people) => people.Count();
    }

    internal class Vehicle
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }

    internal class Person
    {
        public int Number { get; set; }
        public Gender Gender { get; set; }
        public string NameSet { get; set; }
        public string Title { get; set; }
        public string GivenName { get; set; }
        public string MiddleInitial { get; set; }
        public string Surname { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateFull { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string CountryFull { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TelephoneNumber { get; set; }
        public string TelephoneCountryCode { get; set; }
        public string MothersMaiden { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string TropicalZodiac { get; set; }
        public string CCType { get; set; }
        public string CCNumber { get; set; }
        public string CVV2 { get; set; }
        public string CCExpires { get; set; }
        public string NationalID { get; set; }
        public string UPS { get; set; }
        public string WesternUnionMTCN { get; set; }
        public string MoneyGramMTCN { get; set; }
        public string Color { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }
        public string Vehicle { get; set; }
        public string Domain { get; set; }
        public string BloodType { get; set; }
        public float Pounds { get; set; }
        public float Kilograms { get; set; }
        public string FeetInches { get; set; }
        public float Centimeters { get; set; }
        public Guid GUID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    internal enum Gender
    {
        Male,
        Female,
        AttackHelicopter
    }
}
