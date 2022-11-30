using Microsoft.AspNetCore.Mvc;
using WebAPICRUD.Data;

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
        public IActionResult GetContact()
        {
           return Ok(dbContext.Contacts.ToList());
        }
    }
}
