namespace ChatApp.Services.Models.Message
{
    public record MessageCreateModel
    {
        public string Content { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
    }
}