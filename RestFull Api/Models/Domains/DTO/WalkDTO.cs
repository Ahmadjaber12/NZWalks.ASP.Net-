namespace RestFull_Api.Models.Domains.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? WalkImageUrl { get; set; }

        public double LengthInKM { get; set; }

        public Guid DifficulityId { get; set; }

        public Guid RegionId { get; set; }

        public RegionDTO Region { get; set; }

        public DifficulityDTO Difficulity { get; set; }

    }
}
