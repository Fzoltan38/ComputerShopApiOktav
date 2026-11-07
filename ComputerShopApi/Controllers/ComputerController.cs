using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApi.Controllers
{
    [Route("computers")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        public ComputershopoktavContext context = new ComputershopoktavContext();


        [HttpPost("newComputer")]
        public object AddNewComputer([FromBody]AddNewComputerDto addNewComputerDto)
        {
            var computer = new Computer
            {
                Id = Guid.NewGuid(),
                Brand = addNewComputerDto.Brand,
                Type = addNewComputerDto.Type,
                Displyay = addNewComputerDto.Displyay,
                Memory = addNewComputerDto.Memory,
                CreatedTime = DateTime.Now,
                OsId = addNewComputerDto.OsId
            };

            if (computer != null)
            {
                context.Computers.Add(computer);
                context.SaveChanges();

                return StatusCode(201, new { message = "Sikeres felvétel", result = computer });
            }

            return StatusCode(400, new { message = "Sikertelen felvétel", result = computer });
        }


    }
}
