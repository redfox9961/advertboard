namespace BusinessLogic.Model.Dtos
{
    public class RoleDto : DtoBase<long>
    {
        public string RoleName { get; set; }

        public virtual UserDto UserDto { get; set; }
    }
}
