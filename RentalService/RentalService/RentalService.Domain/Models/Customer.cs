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
        private string _street;
        private string _city;
        private string _postalCode;
        private string _country;

        public Customer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public Customer(string firstName, string lastName, string email, string street, string postalCode, string city, string country) : this(firstName, lastName, email)
        {
            Street = street;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public Customer(int id, string firstName, string lastName, string email, string street, string postalCode, string city, string country) : this(firstName, lastName, email, street, postalCode, city, country)
        {
            Id = id;
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
            init =>
            //{
            //    if (string.IsNullOrWhiteSpace(Email))
            //    {
            //        throw new ArgumentException("Email cannot be empty");
            //    }
            //    else if (IsEmailUnique(value))
            //    {
                    Email = value;
                //}
                //else
                //{
                //    throw new ArgumentException("Email already exists.");
                //}
            }
        
        public string Street
        {
            get => _street;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Street));
                _street = value;
                ;
            }
        }
        public string PostalCode
        {
            get => _postalCode;
            init
            {
                //if (value > 999)
                //{
                //    throw new ArgumentException("Zipcode is not supported");
                //}
                //else
                //{
                    _postalCode = value;
                //}
            }
        }
        public string City
        {
            get => _city;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
               _city = value;
            }
        }
        public string Country
        {
            get => _country;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
               _country = value;
            }
        }

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

        public override bool Equals(object? obj)
        {
            return obj is Customer customer &&
                   Id == customer.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string? ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
