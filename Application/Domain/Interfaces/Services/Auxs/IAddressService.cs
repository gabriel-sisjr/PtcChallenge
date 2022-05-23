using Domain.AuxModels;

namespace Domain.Interfaces.Services.Auxs
{
    public interface IAddressService
    {
        /// <summary>
        /// Get the address by cep passed.
        /// </summary>
        /// <param name="cep"></param>
        /// <returns>Address object if is a valid cep, otherwise null.</returns>
        Task<Address?> GetAddressByCep(string cep);
    }
}
