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
        //private string _model;
        //private int _seats;
        private List<string> _motorTypes = ["Hybrid", "Gasoline", "Diesel", "Electric"];
        private string _motorType;
        public Car(string licensePlate, string model, int seats, string motorType)
        {
            LicensePlate = licensePlate;
            Model = model;
            Seats = seats;
            MotorType = motorType;
        }

        public Car(int id, string licensePlate, string model, int seats, string motorType) : this (licensePlate, model, seats, motorType)
        {
            Id = id;
        }

        public int Id { get => _id;
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
        public string Model { get; init; }
        public int Seats { get; init; }
        public string MotorType
        {
            get => _motorType;
            init
            { try
                {
                    //if (_motorTypes.Contains(value))
                    //{
                    ArgumentException.ThrowIfNullOrEmpty(value);
                    MotorType = value;
                }
                catch (StackOverflowException ex)
                {
                    
                }
                //}
                //else
                //{
                    //throw new ArgumentException("Motor type can only be Hybrid, Gasoline, Diesel or Electric.");
                }
            }
        }

    }

