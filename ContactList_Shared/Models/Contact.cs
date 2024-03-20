﻿namespace ContactList_Shared.Models;

public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
}
