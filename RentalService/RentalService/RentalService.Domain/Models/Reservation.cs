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
        private int _customer;
        private int _car;
        private int _establishment;
        private DateTime _startDatum;
        private DateTime _eindDatum;

        public Reservation(int id, DateTime startDatum, DateTime eindDatum, int customerId, int carId, int establishmentId)
        {
            Id = id;
            CustomerId = customerId;
            CarId = carId;
            EstablishmentId = establishmentId;
            StartDatum = startDatum;
            EindDatum = eindDatum;
        }

        public int Id 
        { 
            get => _id; 
            set => _id = value;
        }
        public int CustomerId { get => _customer; set => _customer = value; }
        public int CarId { get => _car; set => _car = value; }
        public int EstablishmentId { get => _establishment; set => _establishment = value; }
        public DateTime StartDatum 
        {
            get => _startDatum;
            set
            {
                //if (StartDatum < DateTime.Now)
                //{
                //    throw new ArgumentException("Startdatum moet in de toekomst liggen.");
                //}
                    _startDatum = value; 
            }
        }
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
