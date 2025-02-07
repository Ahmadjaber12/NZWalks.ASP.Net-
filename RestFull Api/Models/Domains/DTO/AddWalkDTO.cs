namespace RestFull_Api.Models.Domains.DTO
{
    public class AddWalkDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string? WalkImageUrl { get; set; }

        public double LengthInKM { get; set; }

        public Guid DifficulityId { get; set; }

        public Guid RegionId { get; set; }

    }
}
