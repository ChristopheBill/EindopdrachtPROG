using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Repositories
{
    public interface IEstablishmentRepository
    {
        public List<Establishment> GetEstablishments();
        public void ReadEstablishments(string pad);
    }
}
