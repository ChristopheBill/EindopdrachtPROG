using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Models
{
    public class Customer
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        static List<string> _registeredEmails = new();
        private string _streetName;
        private string _city;
        private int _zipCode;
        private string _country;
        public Customer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public Customer(string firstName, string lastName, string email, string streetName, int zipCode, string city, string country) : this(firstName, lastName, email)
        {
            //Id = id;
            StreetName = streetName;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }

        public int Id
        {
            get => _id;
            init
            {

                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual((value), 0);
                _id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(FirstName));
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(LastName));
                _lastName = value;
            }
        }
        public string Email
        {
            get => _email;
            init
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    throw new ArgumentException("Email cannot be empty");
                }
                else if (IsEmailUnique(value))
                {
                    Email = value;
                }
                else
                {
                    throw new ArgumentException("Email already exists.");
                }
            }
        }
        public string StreetName
        {
            get => _streetName;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(StreetName));
                _streetName = value;
                ;
            }
        }
        public int ZipCode
        {
            get => _zipCode;
            init
            {
                if (value > 999)
                {
                    throw new ArgumentException("Zipcode is not supported");
                }
                else
                {
                    _zipCode = value;
                }
            }
        }
        public string City { get; init; }
        public string Country { get; init; }

        static bool IsEmailUnique(string email)
        {
            if (!_registeredEmails.Contains(email))
            {
                _registeredEmails.Add(email);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
