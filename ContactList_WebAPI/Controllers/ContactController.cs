using ContactList_Shared.DTO;
using ContactList_Shared.Models;
using ContactList_WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactList_WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [SwaggerOperation(Summary = "Creates new contact")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateContactDTO newContact)
    {
        try
        {
            var contact = await _contactService.AddAsync(newContact);
            return Created($"api/Contact/{contact.Id}", contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [SwaggerOperation(Summary = "Gets contact by id")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var contact = await _contactService.GetAsync(id);
        if (contact == null) return NotFound();
        return Ok(contact);
    }

    [SwaggerOperation(Summary = "Gets all contacts")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var contact = await _contactService.GetAsync();
        if (contact == null) return NotFound();
        return Ok(contact);
    }

    [SwaggerOperation(Summary = "Gets all categories")]
    [HttpGet("/api/Category")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _contactService.GetAllCategoriesAsync();
        if (categories == null) return NotFound();
        return Ok(categories);
    }

    [SwaggerOperation(Summary = "Deletes contact")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.DeleteAsync(id);
        return NoContent();
    }

    [SwaggerOperation(Summary = "Updates contact")]
    [HttpPut]
    public async Task<IActionResult> Update(DetailedContactDTO updatedContact)
    {
        try
        {
            await _contactService.UpdateAsync(updatedContact);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [SwaggerOperation(Summary = "Check login credentials")]
    [HttpPost("login")]
    public async Task<IActionResult> Login(Login login)
    {
        if (await _contactService.IsValidCredentials(login))
        {
            return Ok();
        }
        else
        {
            return Unauthorized();
        }
    }
}
