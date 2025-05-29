using RentalService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        public Customer(CustomerDTO customer)
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.Email;
            Street = customer.Street;
            PostalCode = customer.PostalCode;
            City = customer.City;
            Country = customer.Country;
        }

        public Customer()
        {
        }

        public int Id
        {
            get => _id;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id cannot be less than 0", (nameof(Id)));
                }
                _id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name cannot be empty or whitespace", (nameof(FirstName)));
                }
                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("First name cannot contain digits", (nameof(FirstName)));
                }
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Last name cannot be empty or whitespace", (nameof(LastName)));
                }
                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Last name cannot contain digits", (nameof(LastName)));
                }
                _lastName = value;
            }
        }
        public string Email
        {
            get => _email;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email cannot be empty or whitespace", (nameof(Email)));
                }
                if (!value.Contains("@") || !value.Contains("."))
                {
                    throw new ArgumentException("Email must contain '@' and '.'", (nameof(Email)));
                }
                    _email = value;
            }
        }
        
        public string Street
        {
            get => _street;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Street cannot be empty or whitespace", (nameof(Street)));
                }
                if (!value.Any(char.IsDigit)) 
                {
                    throw new ArgumentException("Street must contain a number", (nameof(Street)));
                }
                    _street = value;
            }
        }
        public string PostalCode
        {
            get => _postalCode;
            init
            {
                if (int.Parse(value) < 999)
                {
                    throw new ArgumentException("Zipcode is not supported");
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Postal code cannot be empty or whitespace", (nameof(PostalCode)));
                }
                if (value.Length != 4)
                {
                    throw new ArgumentException("Postal code must be 4 characters long", (nameof(PostalCode)));
                }
                _postalCode = value;
            }
        }
        public string City
        {
            get => _city;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("City cannot be empty or whitespace", (nameof(City)));
                }
                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("City cannot contain digits", (nameof(City)));
                }
                _city = value;
            }
        }
        public string Country
        {
            get => _country;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Country cannot be empty or whitespace", (nameof(Country)));
                }
                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Country cannot contain digits", (nameof(Country)));
                }
                _country = value;
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
