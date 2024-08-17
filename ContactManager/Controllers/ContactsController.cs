using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        //Constructor and context
        private readonly ContactContext _contactContext;
        public ContactsController(ContactContext context)
        {
            _contactContext = context;
        }


        //CRUD Operation methods

        //GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _contactContext.Contacts.ToListAsync();
        }

        //GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContactByID(int id)
        {
            var contact = await _contactContext.Contacts.FindAsync(id);
            if (contact == null)
                return NotFound();

            return contact;

        }

        //POST
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _contactContext.Contacts.Add(contact);
            await _contactContext.SaveChangesAsync();
            return CreatedAtAction("GetContacts", new { id = contact.Id }, contact);
        }

        //PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            contact.Id = id; //Required for PUT method. This is needed to match the route parameter
            if (id != contact.Id)
                return BadRequest();
            _contactContext.Entry(contact).State = EntityState.Modified;

            try
            {
                await _contactContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                    
            }
            return Ok(new { message = $"Contact with ID {id} Updated Successfully" });
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _contactContext.Contacts.FindAsync(id);
            if (contact == null)
                return NotFound();
            _contactContext.Contacts.Remove(contact);
            await _contactContext.SaveChangesAsync();
            return Ok(new {message = $"Contact with ID {id} Deleted Successfully"});
        }


        //PRIVATE METHOD
        private bool ContactExists(int id)
        {
            return _contactContext.Contacts.Any(e => e.Id == id);
        }

    }
}
