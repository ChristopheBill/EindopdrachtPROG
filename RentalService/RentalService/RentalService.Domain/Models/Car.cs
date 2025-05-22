using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Models
{
    public class Car
    {
        private int _id;
        private string _licensePlate;
        private string _model;
        private int _seats;
        private List<string> _motorTypes = ["Hybrid", "Gasoline", "Diesel", "Electric"];
        private string _motorType;
        private Establishment _establishment;

        public Car()
        {
        }

        public Car(string licensePlate, string model, int seats, string motorType)
        {
            LicensePlate = licensePlate;
            Model = model;
            Seats = seats;
            MotorType = motorType;
        }

        public Car(int id, string licensePlate, string model, int seats, string motorType) : this(licensePlate, model, seats, motorType)
        {
            Id = id;
        }

        public Car(string licensePlate, string model, int seats, string motorType, Establishment establishment) : this(licensePlate, model, seats, motorType)
        {
            Establishment = establishment;
        }

        public Car(int id, string licensePlate, string model, int seats, string motorType, Establishment establishment) : this(id, licensePlate, model, seats, motorType)
        {
            Establishment = establishment;
        }

        public int Id
        {
            get => _id;
            init
            {
                if (value < 0) 
                {
                    throw new ArgumentException("Id cannot be less than 0.", (nameof(Id))); 
                }
                _id = value;
            }
        }
        public string LicensePlate
        {
            get => _licensePlate;
            init
            { if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("License plate cannot be null or empty.", (nameof(LicensePlate)));
                }
                if (value.Length != 10)
                {
                    throw new ArgumentException("License plate must be 8 characters.", (nameof(LicensePlate)));
                }
                _licensePlate = value;
            }
        }
        public string Model 
        {
            get => _model;
            init 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Model cannot be null or empty.", (nameof(Model)));
                }
                _model = value; 
            } 
        }
        public int Seats 
        { 
            get => _seats;
            init 
            {
                if (value > 10)
                {
                    throw new ArgumentException("Car seats cannot be more than 10°.", (Seats.ToString()));
                }
                if (value >= 2) { _seats = value; }
                 
                else { throw new ArgumentOutOfRangeException("Number of seats cannot be less than 2.", (Seats.ToString())); }
            } 
            
        }
        public string MotorType
        {
            get => _motorType;
            init
            {
                if (_motorTypes.Contains(value))
                {
                    _motorType = value;
                }
                else
                {
                    throw new ArgumentException("Motor type can only be Hybrid, Gasoline, Diesel or Electric.");
                }
            }

        }
        public Establishment Establishment 
        {
            get => _establishment;
            set
            {
                _establishment = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Car car &&
                   Id == car.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}