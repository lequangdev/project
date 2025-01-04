
namespace Common.Model
{
    public class BaseModel
    {
        public DateTime? created { get; set; }
        public string? create_by { get; set; }
        public DateTime? modified { get; set; }
        public string? modify_by { get; set; }
    }
}