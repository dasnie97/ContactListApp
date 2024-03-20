namespace ContactList_Shared.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subcategory { get; set; }

    public ICollection<Contact> Contacts { get; set; }

    public Category(string name, string subcategory)
    {
        Name = name;
        Subcategory = subcategory;
    }

    public Category()
    {
        
    }
}
