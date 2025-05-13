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
        private DateTime _startDatum;
        private DateTime _eindDatum;

        public Reservation(int id, DateTime startDatum, DateTime eindDatum, Customer customer, Car car, Establishment establishment)
        {
            Id = id;
            Customer = customer;
            Car = car;
            Establishment = establishment;
            StartDatum = startDatum;
            EindDatum = eindDatum;
        }

        public int Id 
        { 
            get => _id; 
            set => _id = value;
        }
        public Customer Customer { get => _customer; set => _customer = value; }
        public Car Car { get => _car; set => _car = value; }
        public Establishment Establishment { get => _establishment; set => _establishment = value; }
        public DateTime StartDatum { get => _startDatum; set => _startDatum = value; }
        public DateTime EindDatum { get => _eindDatum; set => _eindDatum = value; }

        public void Valideer()
        {
            if (StartDatum < DateTime.Now)
                throw new ArgumentException("Startdatum moet in de toekomst liggen.");
            if (EindDatum <= StartDatum)
                throw new ArgumentException("Einddatum moet na startdatum liggen.");
            if ((EindDatum - StartDatum).TotalDays < 1)
                throw new ArgumentException("Minimum huurperiode is 1 dag.");
        }
    }
}
