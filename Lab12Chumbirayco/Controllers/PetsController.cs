using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab12Chumbirayco.Data;
using Lab12Chumbirayco.Models;

namespace Lab12Chumbirayco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController(VetCareDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> GetPets(
        [FromQuery] int? ownerId, [FromQuery] string? species, [FromQuery] string? name)
    {
        var query = db.Pets.AsNoTracking().Include(p => p.Owner).AsQueryable();
        if (ownerId.HasValue) query = query.Where(p => p.OwnerId == ownerId.Value);
        if (!string.IsNullOrWhiteSpace(species)) query = query.Where(p => p.Species.Contains(species));
        if (!string.IsNullOrWhiteSpace(name)) query = query.Where(p => p.Name.Contains(name));
        return Ok(await query.OrderBy(p => p.Name).ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pet>> GetPet(int id)
    {
        var pet = await db.Pets.AsNoTracking().Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == id);
        return pet is null ? NotFound() : Ok(pet);
    }

    [HttpPost]
    public async Task<ActionResult<Pet>> PostPet(Pet pet)
    {
        if (!await db.Owners.AnyAsync(o => o.Id == pet.OwnerId))
            return BadRequest(new { message = "La clave ownerId no corresponde a un dueño existente." });
        pet.Id = 0;
        pet.Owner = null;
        db.Pets.Add(pet);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPet), new { id = pet.Id }, pet);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPet(int id, Pet pet)
    {
        if (id != pet.Id) return BadRequest(new { message = "El id de la ruta debe coincidir con el id del cuerpo." });
        var existing = await db.Pets.FindAsync(id);
        if (existing is null) return NotFound();
        if (!await db.Owners.AnyAsync(o => o.Id == pet.OwnerId))
            return BadRequest(new { message = "La clave ownerId no corresponde a un dueño existente." });
        existing.Name = pet.Name;
        existing.Species = pet.Species;
        existing.Age = pet.Age;
        existing.OwnerId = pet.OwnerId;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePet(int id)
    {
        var pet = await db.Pets.FindAsync(id);
        if (pet is null) return NotFound();
        db.Pets.Remove(pet);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
