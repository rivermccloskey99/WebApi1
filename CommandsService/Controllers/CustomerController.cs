using Microsoft.AspNetCore.Mvc;

namespace CommandsService
{
    [Route("api/command/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {

        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("-- Inbound POST to Commands Service");

            return Ok("Inbound test from Platforms Controller");
        }
    }
}