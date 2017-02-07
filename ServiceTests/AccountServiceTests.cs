using System;
using System.Linq;
using Domain.Accounts;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Service.Requests;
using Service.Tests.Framework.AutoData;
using Service.Tests.Framework.Categories;
using Xunit;
using Domain.ValueObjects;

namespace Service.Tests
{
    public class AccountServiceTests
    {
        //I've used mock objects to just show that Autofixture AutoMoqDataAttribute can generate these
        //I'm not actually using any mocking behavior testing in TheConstructorMethod class
        [UnitTest]
        public class TheConstructorMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input([Frozen]Mock<IAccountFactory> mockFactory,
                                                [Frozen]Mock<IAccountRepository> mockRepo)
            {
                IAccountService sut = new AccountService(mockRepo.Object, mockFactory.Object);
            }

            [Theory, AutoMoqData]
            public void Fail_With_Null_Factory([Frozen]Mock<IAccountRepository> mockRepo)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    IAccountService sut = new AccountService(mockRepo.Object, null);
                });
            }

            [Theory, AutoMoqData]
            public void Fail_With_Null_Repository([Frozen]Mock<IAccountFactory> mockFactory)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    IAccountService sut = new AccountService(null, mockFactory.Object);
                });
            }
        }

        //[Frozen] returns the same reference to the object being injected into the AccountService for the mockFactory or mockRepo
        // http://blog.ploeh.dk/2010/10/08/AutoDataTheorieswithAutoFixture
        [UnitTest]
        public class TheWithdrawMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //Setup mock to return specified account with specified paramter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Number, account.Balance, currency);

                sut.Withdraw(request);

                Assert.True(account.Balance == 0);
            }

            [Theory, AutoMoqData]
            public void Verify_Repository_Call_Count([Frozen]Mock<IAccountFactory> mockFactory,
                                                        [Frozen]Mock<IAccountRepository> mockRepo,
                                                        Account account,
                                                        AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Number, account.Balance, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                sut.Withdraw(request);

                mockRepo.Verify(p => p.Get(account.Number), Times.Once);
            }

            [Theory, AutoMoqData]
            public void Fail_With_Invalid_Currency([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //selecting a different CurrencyType than what the Account was generated with
                var domainCurrency = Enum
                                        .GetValues(typeof(Domain.Currency))
                                        .Cast<Domain.Currency>()
                                        .FirstOrDefault(p => p != account.CurrencyType);

                //currency type is different between service contract and domain
                var differentCurrency = new Mapper().Get(domainCurrency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Number, account.Balance, differentCurrency);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Withdraw(request);
                });
            }

            [Theory, AutoMoqData]
            public void Fail_With_Withdraw_Over_Balance([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Number, account.Balance + 1, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Withdraw(request);
                });
            }

            [Theory, AutoMoqData]
            public void Fail_With_Withdraw_Negative_Amount([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Number, -1, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Withdraw(request);
                });
            }
        }

        [UnitTest]
        public class TheDepositMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input_Deposit([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //Setup mock to return specified account with specified paramter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountDepositRequest request = new AccountDepositRequest(account.Number, account.Balance, currency);
                var balance = account.Balance;

                sut.Deposit(request);

                Assert.True(account.Balance == balance * 2);
            }

            [Theory, AutoMoqData]
            public void Verify_Repository_Call_Count([Frozen]Mock<IAccountFactory> mockFactory,
                                                        [Frozen]Mock<IAccountRepository> mockRepo,
                                                        Account account,
                                                        AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountDepositRequest request = new AccountDepositRequest(account.Number, account.Balance, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                sut.Deposit(request);

                mockRepo.Verify(p => p.Get(account.Number), Times.Once);
            }

            [Theory, AutoMoqData]
            public void Fail_With_Invalid_Currency([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //selecting a different CurrencyType than what the Account was generated with
                var domainCurrency = Enum
                                        .GetValues(typeof(Domain.Currency))
                                        .Cast<Domain.Currency>()
                                        .FirstOrDefault(p => p != account.CurrencyType);

                //currency type is different between service contract and domain
                var differentCurrency = new Mapper().Get(domainCurrency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                AccountDepositRequest request = new AccountDepositRequest(account.Number, account.Balance, differentCurrency);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Deposit(request);
                });
            }

            [Theory, AutoMoqData]
            public void Fail_With_Deposit_Negative_Amount([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountDepositRequest request = new AccountDepositRequest(account.Number, -1, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Deposit(request);
                });
            }
        }

        [UnitTest]
        public class TheTransferMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input(AccountNumber toAccountNumber,
                                                    AccountNumber fromAccountNumber,
                                                                [Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                AccountService sut)
            {
                //I can't anonymize all values on the accounts, the Currencies need to be the same
                //so I will attempt to anonymize whatever I can using the fixture
                var fixture = new Fixture();

                var to = new Account(toAccountNumber, fixture.Create<decimal>(), Domain.Currency.AUS);
                var from = new Account(fromAccountNumber, fixture.Create<decimal>(), Domain.Currency.AUS);

                mockRepo.Setup(p => p.Get(to.Number)).Returns(to);
                mockRepo.Setup(p => p.Get(from.Number)).Returns(from);

                var currency = new Mapper().Get(to.CurrencyType);
                var request = new AccountTransferRequest(from.Number, to.Number, from.Balance, Currency.AUS);
                var toBalance = to.Balance;
                var fromBalance = from.Balance;

                sut.Transfer(request);

                Assert.True(to.Balance == toBalance + fromBalance);
            }
        }

        [UnitTest]
        public class TheCreateMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input(AccountNumber number,
                                                                [Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                AccountCreateRequest request,
                                                                AccountService sut)
            {
                var domainCurrency = new Mapper().Get(request.CurrencyType);
                var account = new Account(number, 0, domainCurrency);

                mockFactory.Setup(p => p.Create(domainCurrency)).Returns(account);

                var response = sut.Create(request);

                Assert.True(response.Number == account.Number);
            }

            [Theory, AutoMoqData]
            public void Verify_Factory_Call_Count(AccountNumber number,
                                                                [Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                AccountCreateRequest request,
                                                                AccountService sut)
            {
                var domainCurrency = new Mapper().Get(request.CurrencyType);
                var account = new Account(number, 0, domainCurrency);

                mockFactory.Setup(p => p.Create(domainCurrency)).Returns(account);

                var response = sut.Create(request);

                mockFactory.Verify(p => p.Create(account.CurrencyType), Times.Once);
            }

            [Theory, AutoMoqData]
            public void Verify_Repository_Call_Count(AccountNumber number,
                                                                [Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                AccountCreateRequest request,
                                                                AccountService sut)
            {
                var domainCurrency = new Mapper().Get(request.CurrencyType);
                var account = new Account(number, 0, domainCurrency);

                mockFactory.Setup(p => p.Create(domainCurrency)).Returns(account);

                var response = sut.Create(request);

                mockRepo.Verify(p => p.Save(account), Times.Once);
            }
        }

        [UnitTest]
        public class TheGetMethod
        {
            [Theory, AutoMoqData]
            public void Succeed_With_Valid_Input([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                AccountGetRequest request = new AccountGetRequest(account.Number);

                var result = sut.Get(request);

                Assert.True(account.Number == result.Number);
            }

            [Theory, AutoMoqData]
            public void Verify_Repository_Call_Count([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                mockRepo.Setup(p => p.Get(account.Number)).Returns(account);

                AccountGetRequest request = new AccountGetRequest(account.Number);

                var result = sut.Get(request);

                mockRepo.Verify(p => p.Get(account.Number), Times.Once);
            }
        }
    }
}