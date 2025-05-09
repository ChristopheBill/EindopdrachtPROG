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
        public Reservation(int id, Customer customer, Car car, Establishment establishment, DateTime startDatum, DateTime eindDatum)
        {
            Id = id;
            Customer = customer;
            Car = car;
            Establishment = establishment;
            StartDatum = startDatum;
            EindDatum = eindDatum;
        }

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Establishment Establishment { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }

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
