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
        private string _licensePlate;
        //private string _model;
        //private int _seats;
        private List<string> _motorTypes = ["Hybrid", "Gasoline", "Diesel", "Electric"];
        private string _motorType;
        public Car(string lincensePlate, string model, int seats, string engineType)
        {
            LincensePlate = lincensePlate;
            Model = model;
            Seats = seats;
            MotorType = engineType;
        }

        public string LincensePlate
        {
            get => _licensePlate; 
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(LicensePlate));
                _licensePlate = value;
            }
        }
        public string Model { get; init; }
        public int Seats { get; init; }
        public string MotorType
        {
            get => _motorType;
            init
            {
                if (_motorTypes.Contains(value))
                {
                    MotorType = value;
                }
                else
                {
                    throw new ArgumentException("Motor type can only be Hybrid, Gasoline, Diesel or Electric.");
                }
            }
        }

    }
}
