namespace Common.Model
{
    public class UserEntity /*: BaseModel*/
    {
        public Guid? user_id { get; set; }
        public string? fullname { get; set; }
        public string? email { get; set; }
        public string? phone_number { get; set; }
        public string? account { get; set; }
        public string? password { get; set; }
        public string? avatar_url { get; set; }
        public string? user_role { get; set; }

    }
}
