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
            return new List<EstablishmentDTO> 
            {
                new EstablishmentDTO(1, "Schiphol", "Evert van de Beekstraat 202", "1118 CP", "Amsterdam", "Nederland"),
                new EstablishmentDTO(2, "Charles de Gaulle", "Rue des Halles 95700", "95700", "Roissy-en-France", "Frankrijk"),
                new EstablishmentDTO(3, "Frankfurt am Main", "Flughafenstraße 60549", "60549", "Frankfurt am Main", "Duitsland"),
                new EstablishmentDTO(4, "Barajas", "Avda. de la Hispanidad", "28042", "Madrid", "Spanje"),
                new EstablishmentDTO(5, "El Prat", "Carretera del Prat", "08820", "Barcelona", "Spanje")
            };
        }

        public void ReadEstablishments(string pad)
        {
            throw new NotImplementedException();
        }
    }
}
