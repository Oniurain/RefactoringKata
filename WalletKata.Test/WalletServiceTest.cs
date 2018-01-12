namespace WalletKata.Test
{
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using WalletKata.Exceptions;
    using WalletKata.Users;
    using WalletKata.Wallets;

    public class WalletServiceTest
    {
        Mock<IUserSession> _mockUserSession;
        Mock<IWalletDAO> _walletDao;
        WalletService testedService;

        [SetUp]
        public void SetUp()
        {
            _mockUserSession = new Mock<IUserSession>();
            _walletDao = new Mock<IWalletDAO>();
            testedService = new WalletService(_walletDao.Object);
            testedService.userSession = _mockUserSession.Object;
        }

        [Test]
        public void TestWalletService_GetWalletsByUser_NoLoggedUser_ThrowsNotLoggedException()
        {
            User paramUser = new User();
            _mockUserSession.Setup(p => p.GetLoggedUser()).Returns<User>(null);

            Assert.Throws<UserNotLoggedInException>(() => testedService.GetWalletsByUser(paramUser));
        }

        [Test]
        public void TestWalletService_GetWalletsByUser_LoggedUserWithoutFriend_EmptyList()
        {
            User paramUser = new User();
            User loggedUser = new User();
            _mockUserSession.Setup(p => p.GetLoggedUser()).Returns(loggedUser);

            List<Wallet> result = testedService.GetWalletsByUser(paramUser);

            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void TestWalletService_GetWalletsByUser_LoggedUserIsNotFriend_EmptyList()
        {
            User paramUser = new User();
            User loggedUser = new User();
            paramUser.AddFriend(new User());
            List<Wallet> walletsByUser = new List<Wallet>() { new Wallet() };
            _mockUserSession.Setup(p => p.GetLoggedUser()).Returns(loggedUser);
            _walletDao.Setup(p => p.FindWalletsByUser(paramUser)).Returns(walletsByUser);

            List<Wallet> result = testedService.GetWalletsByUser(paramUser);

            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void TestWalletService_GetWalletsByUser_LoggedUserIsFriend_WalletList()
        {
            User paramUser = new User();
            User loggedUser = new User();
            paramUser.AddFriend(loggedUser);
            List<Wallet> walletsByUser = new List<Wallet>() { new Wallet() };
            _mockUserSession.Setup(p => p.GetLoggedUser()).Returns(loggedUser);
            _walletDao.Setup(p => p.FindWalletsByUser(paramUser)).Returns(walletsByUser);

            List<Wallet> result = testedService.GetWalletsByUser(paramUser);

            Assert.That(result.Count, Is.EqualTo(walletsByUser.Count));
        }
    }
}
