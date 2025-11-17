namespace Repositories
{
    public interface IUserPasswordRepository
    {
        int CheckPassword(string password);
    }
}