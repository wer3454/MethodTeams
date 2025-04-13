namespace MethodologyMain.Application.Exceptions
{
    public class UserNotInTeamException : Exception
    {
        public UserNotInTeamException()
        {
            
        }

        public UserNotInTeamException(string message)
            : base(message)
        {
            
        }
    }
}