using RentalService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Repositories
{
    public interface IReservationRepository
    {
        public List<ReservationDTO> GetReservations();
        public List<ReservationDTO> GetReservationsByCustomerIdEstablishmentIdDate(int customerId, int establishmentId, DateTime date);
        public void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId);
        public void RemoveReservation (int reservationId);
    }
}
