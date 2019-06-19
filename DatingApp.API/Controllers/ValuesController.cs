using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        public readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context=context;
        }

        // GET api/values
        [Authorize(Policy="VipOnly")]
        public string GetOnlyAdmin()
        {
            //var values=await _context.Values.ToListAsync();
            return  "Only Admin";            
        }

        
        
        // [HttpGet]
        // [Authorize(Policy="Admin")]
        // public string GetOnlyMember()
        // {
        //     //var values=await _context.Values.ToListAsync();
        //     return  "Only Member";            
        // }

        

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetById(int id)
        // {
        //     var value=await _context.Values.FirstOrDefaultAsync(x=>x.Id==id);
        //     if(value==null)
        //         return NotFound();
        //     return Ok(value);
        // }
        
        
        
        // [HttpGet]
        // [Authorize(Roles="Member")]
        // public ActionResult<IEnumerable<string>> GetMember()
        // {
        //     return new string[] { "Only Member" };
        // }

        //   [HttpGet]
        // [AllowAnonymous]
        // public ActionResult<IEnumerable<string>> GetAnonymous()
        // {
        //     return new string[] { "Only Anonymous" };
        // }

        // // GET api/values/5
        // // [HttpGet("{id}")]
        // // public ActionResult<string> Get(int id)
        // // {
        // //     return "value";
        // // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
