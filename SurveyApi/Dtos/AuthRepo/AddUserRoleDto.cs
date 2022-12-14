namespace SurveyApi.Dtos.AuthRepo
{
    public class AddUserRoleDto
    {
        public Guid UsersIdRole { get; set; }
        public Guid RolesIdUser { get; set; }
    }
}
