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

        [HttpPut("updateComputer")]
        public object UpdateComputer([FromQuery] Guid id, [FromBody] UpdateComputerDto updateComputerDto)
        {
            var computer = context.Computers.Find(id);

            if (computer != null)
            {
                computer.Brand = updateComputerDto.Brand;
                computer.Type = updateComputerDto.Type;
                computer.Displyay = updateComputerDto.Displyay;
                computer.Memory = updateComputerDto.Memory;
                computer.OsId = updateComputerDto.OsId;

                context.Computers.Update(computer);
                context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres frissítés", result = computer });
            }
            return StatusCode(404, new { message = "Nincs ilyen Id-vel computer.", result = computer });
        }

        [HttpDelete("deleteComputer")]
        public object DeleteComputer([FromQuery] Guid id)
        {
            var computer = context.Computers.FirstOrDefault(c => c.Id == id);

            if (computer != null)
            {
                context.Computers.Remove(computer);
                context.SaveChanges();

                return StatusCode(204);
            }

            return StatusCode(404, new { message = "Nincs ilyen Id-vel computer." });
        }

        [HttpGet("computerWithOs")]
        public object ComputerWithOs()
        {
            var computers = context.Computers
                .Join(
                context.Osystems,
                c => c.OsId,
                o => o.Id,
                (c,o) => new {
                    c.Id,
                    c.Brand,
                    c.Type,
                    c.Displyay,
                    c.Memory,
                    o.Name,
                    o.Version
                })
                .ToList();

            if (computers.Count > 0)
            {
                return StatusCode(200, new { message = "Lekérdzve", result = computers });
            }

            return StatusCode(404, new { message = "Üres tábla", result = computers });
        }
    }
}
