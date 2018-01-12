namespace WalletKata.Wallets
{
    using System.Collections.Generic;
    using WalletKata.Exceptions;
    using WalletKata.Users;

    public class WalletDAO : IWalletDAO
    {
        public List<Wallet> FindWalletsByUser(User user)
        {
            throw new ThisIsAStubException("WalletDAO.FindWalletsByUser() should not be called in an unit test");
        }
    }
}