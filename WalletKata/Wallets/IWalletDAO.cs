namespace WalletKata.Wallets
{
    using System.Collections.Generic;
    using WalletKata.Users;

    public interface IWalletDAO
    {
        List<Wallet> FindWalletsByUser(User user);
    }
}