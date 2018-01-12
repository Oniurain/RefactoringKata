namespace WalletKata.Wallets
{
    using System.Collections.Generic;
    using WalletKata.Users;
    using WalletKata.Exceptions;

    public class WalletService
    {
        private IUserSession _userSession;
        private IWalletDAO _walletDao;

        public IUserSession userSession
        {
            get
            {
                return _userSession != null ? _userSession : UserSession.GetInstance();
            }
            set
            {
                _userSession = value;
            }
        }

        public WalletService(IWalletDAO walletDao)
        {
            _walletDao = walletDao;
        }

        public List<Wallet> GetWalletsByUser(User user)
        {
            List<Wallet> walletList = new List<Wallet>();
            User loggedUser = userSession.GetLoggedUser();
            bool isFriend = false;

            if (loggedUser != null)
            {
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }

                if (isFriend)
                {
                    walletList = _walletDao.FindWalletsByUser(user);
                }

                return walletList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
    }
}