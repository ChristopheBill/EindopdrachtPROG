using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Tests.TestMappers
{
    internal class TestReservationMapper : IReservationRepository
    {
        public List<ReservationDTO> GetReservations()
        {
            throw new NotImplementedException();
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
