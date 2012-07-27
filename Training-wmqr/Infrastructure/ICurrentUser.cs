namespace Training_wmqr.Infrastructure
{
    public interface ICurrentUser
    {
        string Name();
        string UserName();
        bool IsLoggedIn();
    }
}
