namespace GameStore.Infrastructure.DTO
{
    public class ChangePasswordDTO
    {
        public int UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
