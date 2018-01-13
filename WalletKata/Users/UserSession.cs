namespace WalletKata.Users
{
    using WalletKata.Exceptions;

    public class UserSession : IUserSession
    {
        public User GetLoggedUser()
        {
            throw new ThisIsAStubException("UserSession.IsUserLoggedIn() should not be called in an unit test");
        }
    }
}