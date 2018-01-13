namespace WalletKata.Wallets
{
    using System.Collections.Generic;
    using System.Linq;
    using WalletKata.Exceptions;
    using WalletKata.Users;

    public class WalletService
    {
        private IUserSession _userSession;
        private IWalletDAO _walletDao;

        public WalletService(IWalletDAO walletDao, IUserSession userSession)
        {
            _walletDao = walletDao;
            _userSession = userSession;
        }

        public List<Wallet> GetWalletsByUser(User user)
        {
            User loggedUser = _userSession.GetLoggedUser();
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }

            User friend = user.GetFriends().SingleOrDefault(p => p.Equals(loggedUser));
            if (friend != null)
            {
                return _walletDao.FindWalletsByUser(user);
            }

            return new List<Wallet>();
        }
    }
}