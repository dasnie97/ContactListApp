using ContactList_Shared.DTO;
using ContactList_Shared.Helpers;
using ContactList_Shared.Models;
using ContactList_WebAPI.DBContext;
using ContactList_WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactList_WebAPI.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ContactListAppContext _context;

    public ContactRepository(ContactListAppContext contactListAppContext)
    {
        _context = contactListAppContext;
    }

    public async Task<Contact> AddAsync(Contact contact)
    {
        if (EmailAlreadyExists(contact))
        {
            throw new Exception($"Email: '{contact.Email}' already exists!");
        }

        var relatedCategory = FindRelatedCategory(contact);

        if (relatedCategory == null)
        {
            await _context.Categories.AddAsync(contact.Category);
        }
        else
        {
            contact.Category = relatedCategory;
        }
        contact.Password = PasswordEncryption.EncryptPassword(contact.Password);
        await _context.Contacts.AddAsync(contact);
        _context.SaveChanges();
        return contact;
    }

    public async Task DeleteAsync(int id)
    {
        var contact = await GetAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }
    }

    public async Task<Contact> GetAsync(int id)
    {
        return await Task.Run(() => _context.Contacts.Include(c => c.Category).Where(c => c.Id == id).FirstOrDefault());
    }

    public async Task<IEnumerable<Contact>> GetAsync()
    {
        return await Task.Run(() => _context.Contacts.Take(1000));
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await Task.Run(() => _context.Categories.Take(1000));
    }

    public async Task UpdateAsync(Contact contact)
    {
        if (EmailAlreadyExists(contact))
        {
            throw new Exception($"Email: '{contact.Email}' already exists!");
        }

        var relatedCategory = FindRelatedCategory(contact);

        if (relatedCategory == null)
        {
            await _context.Categories.AddAsync(contact.Category);
        }
        else
        {
            contact.Category = relatedCategory;
        }

        _context.Contacts.Update(contact);
        _context.SaveChanges();
    }

    public async Task<bool> IsValidCredentials(Login login)
    {
        var contactWithSuchEmail = await Task.Run(() => _context.Contacts.Where(c => c.Email == login.Email).FirstOrDefault());
        if (contactWithSuchEmail != null)
        {
            return PasswordEncryption.VerifyPassword(login.Password, contactWithSuchEmail.Password);
        }
        return false;
    }

    private bool EmailAlreadyExists(Contact contact)
    {
        if (_context.Contacts.Where(c => c.Email == contact.Email && c != contact).Any())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Category? FindRelatedCategory(Contact contact)
    {
        var relatedCategory = _context.Categories.Where(c => c.Name == contact.Category.Name && c.Subcategory == contact.Category.Subcategory).SingleOrDefault();
        return relatedCategory;
    }
}
