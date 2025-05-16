using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Repositories
{
    public interface IReservationRepository
    {
        public List<Reservation> GetReservations();
        public void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId);
    }
}
