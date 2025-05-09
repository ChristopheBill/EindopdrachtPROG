
using RentalService.Domain;

namespace RentalService.Presentation
{
    public class RentalServiceApplication
    {
        private readonly DomainManager _domainManager;

        public RentalServiceApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;
        }
    }

}
