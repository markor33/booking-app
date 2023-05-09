namespace Reservations.API.Security
{
    public interface IHospitalAPIClient
    {
        public bool ValidateAuthorizationHeader(string authorizationHeader);
    }
}
