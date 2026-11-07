namespace ComputerShopApi.Models.DTOs
{
    public class UpdateComputerDto
    {
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public float Displyay { get; set; }
        public int Memory { get; set; }
        public Guid OsId { get; set; }
    }
}
