using EnvironmentManager.Data;
using EnvironmentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    // this is a route for the APIController ,
    // this will active once a user goes to certain section of the application
    // we use the Route in order for the HTTPGet request to trigger on THAT particular page.

    // using a [Route] will define the place and map it to the specific controller needed , 
    //but if its going somewhere else it will need to be specified.


    public class EnvironmentController : Controller
    {
        private readonly EnvironmentDBContext _environmentDBContext;

        public EnvironmentController(EnvironmentDBContext environmentDBContext)
        { 
            _environmentDBContext = environmentDBContext;
        }
        // getting all environments form Database 
        [HttpGet]

        public async Task<IActionResult> GetAllEnvironments()
        {
            var environments = await _environmentDBContext.Environment.ToListAsync();

            return Ok (environments);

            
        }
        //--------------------

        // posting a Environment to the Database 
        [HttpPost]

        public async Task<IActionResult> AddEnvironments([FromBody] EnvironmentModel environmentRequest)
        {
            environmentRequest.Id = Guid.NewGuid();

            await _environmentDBContext.Environment.AddAsync(environmentRequest);
            await _environmentDBContext.SaveChangesAsync();

            return Ok(environmentRequest);
        }

        // --------------------

        // updating and singling out Environments using GUID signiture 
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetEnvironment([FromRoute] Guid id )
        {
            var environment = await _environmentDBContext.Environment.FirstOrDefaultAsync(x => x.Id == id);

            if (environment == null)
            {
                return NotFound();
            }

            return Ok(environment);
        }
        //----------------------

        [HttpPut]
        [Route("{id:Guid}")] // this will be to edit and then save changes to Environments that are selected 

        public async Task<IActionResult> UpdateEnvironment([FromRoute] Guid id , EnvironmentModel UpdateEnvironmentRequest)
        {
            var environment = await _environmentDBContext.Environment.FindAsync(id);

            if (environment == null) { return NotFound(); }

            environment.Title = UpdateEnvironmentRequest.Title;

            environment.Name = UpdateEnvironmentRequest.Name;


            await _environmentDBContext.SaveChangesAsync();

            return Ok(environment);


        }
        //--------------------

        // this will Delete the selected Environment when called 

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteEnvironment([FromRoute] Guid id )
        {
            var environment = await _environmentDBContext.Environment.FindAsync(id);


            if (environment == null) 
            
            { 
                return NotFound();
            
            }

            _environmentDBContext.Environment.Remove(environment);

            await _environmentDBContext.SaveChangesAsync();

            return Ok(environment);

        }

        [HttpPatch]
        [Route("{id:Guid}/status")]

        public async Task<IActionResult> StatusChange([FromRoute] Guid id, [FromBody] int newStatus)
        {
            var environment = await _environmentDBContext.Environment.FindAsync(id);

            if (environment == null)
            {
                return NotFound();
            }

            environment.Status = newStatus;

            await _environmentDBContext.SaveChangesAsync();

            return Ok(environment);


        }

    }
}
