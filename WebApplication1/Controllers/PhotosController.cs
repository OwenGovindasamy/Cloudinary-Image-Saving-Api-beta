using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //TODO: work on delete cloud entry 
    public class PhotosController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Photo>> Add([FromForm]Add.Command command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet]
        public async Task<ActionResult<List<Photo>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> Details(string id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }
        
        [HttpPost("{paramsk}")]
        public async Task<ActionResult<SearchResult>> Search(string paramsk)
        {
            return await Mediator.Send(new SearchImage.Query{ paramsk = paramsk });
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(string id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }
    }
}