using ContactList_Shared.DTO;
using ContactList_Shared.Models;

namespace ContactList_WebAPI.Interfaces;

public interface IContactRepository
{
    Task<Contact> AddAsync(Contact contact);
    Task<Contact> GetAsync(int id);
    Task<IEnumerable<Contact>> GetAsync();
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(int id);
    Task<bool> IsValidCredentials(Login login);
}
