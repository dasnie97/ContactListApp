using AutoMapper;
using ContactList_Shared.DTO;
using ContactList_Shared.Models;
using ContactList_WebAPI.Interfaces;

namespace ContactList_WebAPI.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public ContactService(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<ContactDTO> AddAsync(CreateContactDTO contact)
    {
        var mappedToDomain = _mapper.Map<Contact>(contact);
        await _contactRepository.AddAsync(mappedToDomain);
        return _mapper.Map<ContactDTO>(mappedToDomain);
    }

    public async Task<DetailedContactDTO> GetAsync(int id)
    {
        var contact = await _contactRepository.GetAsync(id);
        return _mapper.Map<DetailedContactDTO>(contact);
    }

    public async Task<IEnumerable<ContactDTO>> GetAsync()
    {
        var contact = await _contactRepository.GetAsync();
        return _mapper.Map<IEnumerable<ContactDTO>>(contact);
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
    {
        var categories = await _contactRepository.GetAllCategoriesAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }

    public async Task UpdateAsync(DetailedContactDTO contact)
    {
        var originalContact = await _contactRepository.GetAsync(contact.Id);
        var updatedContact = _mapper.Map(contact, originalContact);
        await _contactRepository.UpdateAsync(updatedContact);
    }

    public async Task DeleteAsync(int id)
    {
        await _contactRepository.DeleteAsync(id);
    }

    public async Task<bool> IsValidCredentials(Login login)
    {
        return await _contactRepository.IsValidCredentials(login);
    }
}
