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

    }
}
