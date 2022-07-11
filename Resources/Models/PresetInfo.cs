namespace Resources.Models
{
    public class PresetInfo
    {
        public int Id { get; set; }
        public string PresetName { get; set; } = default!;
        public bool IsPublished { get; set; }
    }
}
