using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab12Chumbirayco.Data;
using Lab12Chumbirayco.Models;

namespace Lab12Chumbirayco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController(VetCareDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Owner>>> GetOwners([FromQuery] string? name, [FromQuery] string? email)
    {
        var query = db.Owners.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(name)) query = query.Where(o => o.Name.Contains(name));
        if (!string.IsNullOrWhiteSpace(email)) query = query.Where(o => o.Email.Contains(email));
        return Ok(await query.OrderBy(o => o.Name).ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Owner>> GetOwner(int id)
    {
        var owner = await db.Owners.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        return owner is null ? NotFound() : Ok(owner);
    }

    [HttpGet("{id:int}/pets")]
    public async Task<ActionResult<IEnumerable<Pet>>> GetOwnerPets(int id)
    {
        if (!await db.Owners.AnyAsync(o => o.Id == id)) return NotFound();
        return Ok(await db.Pets.AsNoTracking().Where(p => p.OwnerId == id)
            .OrderBy(p => p.Name).ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Owner>> PostOwner(Owner owner)
    {
        if (await db.Owners.AnyAsync(o => o.Email == owner.Email))
            return Conflict(new { message = "Ya existe un dueño con ese correo." });
        owner.Id = 0;
        owner.Pets = new List<Pet>();
        db.Owners.Add(owner);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOwner), new { id = owner.Id }, owner);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutOwner(int id, Owner owner)
    {
        if (id != owner.Id) return BadRequest(new { message = "El id de la ruta debe coincidir con el id del cuerpo." });
        var existing = await db.Owners.FindAsync(id);
        if (existing is null) return NotFound();
        if (await db.Owners.AnyAsync(o => o.Email == owner.Email && o.Id != id))
            return Conflict(new { message = "Ya existe un dueño con ese correo." });
        existing.Name = owner.Name;
        existing.Email = owner.Email;
        existing.Phone = owner.Phone;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOwner(int id)
    {
        var owner = await db.Owners.FindAsync(id);
        if (owner is null) return NotFound();
        if (await db.Pets.AnyAsync(p => p.OwnerId == id))
            return Conflict(new { message = "No se puede eliminar un dueño que tiene mascotas." });
        db.Owners.Remove(owner);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
