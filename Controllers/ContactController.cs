using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICRUD.Data;
using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
           return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContacRequest addContacRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContacRequest.Address,
                Email = addContacRequest.Email,
                FullName = addContacRequest.FullName,
                Phone = addContacRequest.Phone
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
          var contact = await dbContext.Contacts.FindAsync(id);

            if(contact != null) 
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

              await  dbContext.SaveChangesAsync();

                return Ok(contact);
            }
            return NotFound();
        }


    }
}
