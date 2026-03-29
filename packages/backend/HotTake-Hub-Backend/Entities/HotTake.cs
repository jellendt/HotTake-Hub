namespace HotTake_Hub_Backend.Entities
{
    public class HotTake
    {
        public required Guid Id { get; set; }
        public required string Text { get; set; }
        public required Guid AuthorId { get; set; }
        public required DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public bool IsDeleted { get; set; }
    }
}
