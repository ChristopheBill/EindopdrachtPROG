using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public int Id
        {
            get => _id;
            init
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual((value), 0);
                _id = value;
            }
        }
        public string LicensePlate
        {
            get => _licensePlate;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                _licensePlate = value;
            }
        }
        public string Model { get => _model; init => _model = value; }
        public int Seats 
        { 
            get => _seats; 
            init => _seats = value; 
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
    }
}