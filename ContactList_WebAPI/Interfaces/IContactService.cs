using ContactList_Shared.DTO;
using ContactList_Shared.Models;

namespace ContactList_WebAPI.Interfaces;

public interface IContactService
{
    Task<ContactDTO> AddAsync(CreateContactDTO contact);
    Task<DetailedContactDTO> GetAsync(int id);
    Task<IEnumerable<ContactDTO>> GetAsync();
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    Task UpdateAsync(DetailedContactDTO contact);
    Task DeleteAsync(int id);
    Task<bool> IsValidCredentials(Login login);
}
