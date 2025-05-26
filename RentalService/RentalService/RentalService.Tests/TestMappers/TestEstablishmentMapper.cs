using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Tests.TestMappers
{
    internal class TestEstablishmentMapper : IEstablishmentRepository
    {
        public List<EstablishmentDTO> GetEstablishments()
        {
            throw new NotImplementedException();
        }

        public void ReadEstablishments(string pad)
        {
            throw new NotImplementedException();
        }
    }
}
