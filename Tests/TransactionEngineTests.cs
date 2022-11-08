using Database.Factory;
using Database.Implementation;
using Database.Interface;
using Moq;
using TransactionEngine;
using TransactionEngine.Model;

namespace Tests
{
    [TestClass]
    public class TransactionEngineTests
    {
        private readonly Mock<IDatabaseFactory> _databaseFactoryMock = new Mock<IDatabaseFactory>();
        private readonly Mock<ITransactionHelper> _transactionHelperMock = new Mock<ITransactionHelper>();

        [TestMethod]
        public void MultithreadTransactionTest()
        {
            var playerDb = new PlayerDatabase();
            var transDb = new TransactionDatabase();
            _databaseFactoryMock.SetupGet(x => x.TransactionDatabase).Returns(transDb);
            _databaseFactoryMock.SetupGet(x => x.PlayerDatabase).Returns(playerDb);

            var transHelper = new TransactionHelper(_databaseFactoryMock.Object);
            _transactionHelperMock.Setup(x => x.GetPlayerBalance(It.IsAny<Guid>())).Returns(0);

            var engine = new TransactionEngine.TransactionEngine(_databaseFactoryMock.Object, _transactionHelperMock.Object);

            var player1Guid = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var player2Guid = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-bbbbbbbbbbbb");
            var transGuid1 = Guid.NewGuid();
            var transGuid2 = Guid.NewGuid();

            playerDb.AddPlayer(player1Guid);
            playerDb.AddPlayer(player2Guid);

            var task1 = new Task<bool>(() => engine.ProcessTransaction(player1Guid, transGuid1, TransactionType.Deposit, 10));
            var task2 = new Task<bool>(() => engine.ProcessTransaction(player1Guid, transGuid1, TransactionType.Deposit, 10));
            var task3 = new Task<bool>(() => engine.ProcessTransaction(player2Guid, transGuid2, TransactionType.Deposit, 10));
            var task4 = new Task<bool>(() => engine.ProcessTransaction(player2Guid, transGuid2, TransactionType.Deposit, 10));

            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            Task.WaitAll(new[] { task1, task2, task3, task4 });

            Assert.IsTrue(transDb.GetPlayerTransactions(player1Guid).Count() == 1);
            Assert.IsTrue(transDb.GetPlayerTransactions(player2Guid).Count() == 1);
        }
    }
}