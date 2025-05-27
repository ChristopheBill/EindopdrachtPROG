using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Tests.TestMappers
{
    class TestReservationMapper : IReservationRepository
    {
        public List<ReservationDTO> GetReservations()
        {
            List<ReservationDTO> reservations = new List<ReservationDTO>
            {
                new ReservationDTO
                {
                    Id = 1,
                    StartDate = new DateTime(2023, 10, 1),
                    EndDate = new DateTime(2023, 10, 5),
                    Customer = new CustomerDTO { Id = 1, FirstName = "John Doe" },
                    Car = new CarDTO { Id = 1, Model = "Toyota Corolla" },
                    Establishment = new EstablishmentDTO { Id = 1, Country = "Main Branch" }
                },
                new ReservationDTO
                {
                    Id = 2,
                    StartDate = new DateTime(2023, 10, 6),
                    EndDate = new DateTime(2023, 10, 10),
                    Customer = new CustomerDTO { Id = 2, FirstName = "Jane Smith" },
                    Car = new CarDTO { Id = 2, Model = "Honda Civic" },
                    Establishment = new EstablishmentDTO { Id = 1, Country = "Main Branch" }
                },
                new ReservationDTO
                {
                    Id = 3,
                    StartDate = new DateTime(2023, 10, 11),
                    EndDate = new DateTime(2023, 10, 15),
                    Customer = new CustomerDTO { Id = 3, FirstName = "Alice Johnson" },
                    Car = new CarDTO { Id = 3, Model = "Ford Focus" },
                    Establishment = new EstablishmentDTO { Id = 2, Country = "Secondary Branch" }
                }
            };
            return reservations;
        }

        public List<ReservationDTO> GetReservationsByCustomerIdEstablishmentIdDate(int customerId, int establishmentId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId)
        {
            throw new NotImplementedException();
        }

        public void RemoveReservation(int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
