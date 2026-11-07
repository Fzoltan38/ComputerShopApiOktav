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


        [HttpGet("allComputer")]
        public object AllComputer()
        {
            var computer = context.Computers.ToList();

            if (computer.Count > 0)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = computer });
            }

            return StatusCode(404, new { message = "Üres tábla", result = computer });
        }

        [HttpGet("computerById")]
        public object ComputerById([FromQuery] Guid id)
        {
            var computer = context.Computers.SingleOrDefault(c => c.Id == id);

            if (computer != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = computer });
            }

            return StatusCode(404, new { message = "Nincs ilyen Id-vel computer.", result = computer });
        }


    }
}
