namespace ChatApp.Services.Models.Group
{
    public record GroupCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
    }
}