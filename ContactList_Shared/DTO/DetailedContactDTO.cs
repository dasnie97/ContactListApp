﻿namespace ContactList_Shared.DTO;

public class DetailedContactDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
}
