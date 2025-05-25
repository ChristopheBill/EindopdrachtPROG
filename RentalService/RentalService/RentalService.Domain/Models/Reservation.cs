using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Models
{
    public class Reservation
    {
        private int _id;
        private Customer _customer;
        private Car _car;
        private Establishment _establishment;
        private DateTime _startDate;
        private DateTime _endDate;
        public Reservation(int id, DateTime startDate, DateTime endDate, Customer customer, Car car, Establishment establishment)
        {
            Id = id;
            Customer = customer;
            Car = car;
            Establishment = establishment;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id 
        { 
            get => _id; 
            set => _id = value;
        }
        public Customer Customer { get => _customer; set => _customer = value; }
        public Car Car { get => _car; set => _car = value; }
        public Establishment Establishment { get => _establishment; set => _establishment = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
    }
}
