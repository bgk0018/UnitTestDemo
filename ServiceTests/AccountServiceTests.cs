using Domain.Accounts;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Service;
using Service.Requests;
using ServiceTests.AutoMoq;
using System;
using System.Linq;
using Xunit;

namespace ServiceTests
{
    public class AccountServiceTests
    {
        //I've used mock objects to just show that Autofixture AutoMoqDataAttribute can generate these
        //I'm not actually using any mocking behavior testing in TheConstructoreMethod class
        public class TheConstructorMethod
        {
            [Theory(DisplayName= "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input([Frozen]Mock<IAccountFactory> mockFactory,
                                                [Frozen]Mock<IAccountRepository> mockRepo)
            {
                IAccountService sut = new AccountService(mockRepo.Object, mockFactory.Object);
            }

            [Theory(DisplayName = "Fail_With_Null_Factory"), AutoMoqData]
            public void Fail_With_Null_Factory([Frozen]Mock<IAccountRepository> mockRepo)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    IAccountService sut = new AccountService(mockRepo.Object, null);
                });
            }

            [Theory(DisplayName = "Fail_With_Null_Repository"), AutoMoqData]
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
        public class TheWithdrawMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input_Withdraw_All([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //Setup mock to return specified account with specified paramter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Id, account.Balance, currency);
                
                sut.Withdraw(request);

                Assert.True(account.Balance == 0);
            }

            [Theory(DisplayName = "Verify_Repository_Call_Count"), AutoMoqData]
            public void Verify_Repository_Call_Count([Frozen]Mock<IAccountFactory> mockFactory,
                                                        [Frozen]Mock<IAccountRepository> mockRepo,
                                                        Account account,
                                                        AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Id, account.Balance, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                sut.Withdraw(request);

                mockRepo.Verify(p => p.Get(account.Id), Times.Once);
            }

            [Theory(DisplayName = "Fail_With_Invalid_Currency"), AutoMoqData]
            public void Fail_With_Invalid_Currency([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //selecting a different CurrencyType than what the Account was generated with
                var domainCurrency = Enum.GetValues(typeof(Domain.Currency))
                                        .Cast<Domain.Currency>()
                                        .Where(p => p != account.CurrencyType)
                                        .FirstOrDefault();

                //currency type is different between service contract and domain
                var differentCurrency = new Mapper().Get(domainCurrency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Id, account.Balance, differentCurrency);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Withdraw(request);
                });
            }

            [Theory(DisplayName = "Fail_With_Withdraw_Over_Balance"), AutoMoqData]
            public void Fail_With_Withdraw_Over_Balance([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Id, account.Balance + 1, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Withdraw(request);
                });
            }

            [Theory(DisplayName = "Fail_With_Withdraw_Negative_Amount"), AutoMoqData]
            public void Fail_With_Withdraw_Negative_Amount([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountWithdrawRequest request = new AccountWithdrawRequest(account.Id, -1, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Withdraw(request);
                });
            }
        }

        public class TheDepositMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input_Deposit([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //Setup mock to return specified account with specified paramter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountDepositRequest request = new AccountDepositRequest(account.Id, account.Balance, currency);
                var balance = account.Balance;

                sut.Deposit(request);

                Assert.True(account.Balance == balance * 2);
            }

            [Theory(DisplayName = "Verify_Repository_Call_Count"), AutoMoqData]
            public void Verify_Repository_Call_Count([Frozen]Mock<IAccountFactory> mockFactory,
                                                        [Frozen]Mock<IAccountRepository> mockRepo,
                                                        Account account,
                                                        AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountDepositRequest request = new AccountDepositRequest(account.Id, account.Balance, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                sut.Deposit(request);

                mockRepo.Verify(p => p.Get(account.Id), Times.Once);
            }

            [Theory(DisplayName = "Fail_With_Invalid_Currency"), AutoMoqData]
            public void Fail_With_Invalid_Currency([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //selecting a different CurrencyType than what the Account was generated with
                var domainCurrency = Enum.GetValues(typeof(Domain.Currency))
                                        .Cast<Domain.Currency>()
                                        .Where(p => p != account.CurrencyType)
                                        .FirstOrDefault();

                //currency type is different between service contract and domain
                var differentCurrency = new Mapper().Get(domainCurrency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                AccountDepositRequest request = new AccountDepositRequest(account.Id, account.Balance, differentCurrency);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Deposit(request);
                });
            }


            [Theory(DisplayName = "Fail_With_Deposit_Negative_Amount"), AutoMoqData]
            public void Fail_With_Deposit_Negative_Amount([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                //currency type is different between service contract and domain
                var currency = new Mapper().Get(account.CurrencyType);
                AccountDepositRequest request = new AccountDepositRequest(account.Id, -1, currency);

                //Setup mock to return specified account with specified parameter account.Id
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    sut.Deposit(request);
                });
            }
        }

        public class TheTransferMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                AccountService sut)
            {
                //I can't anonymize all values on the accounts, the Currencies need to be the same
                //so I will attempt to anonymize whatever I can using the fixture
                var fixture = new Fixture();

                var to = new Account(Guid.NewGuid(), fixture.Create<decimal>(), Domain.Currency.AUS);
                var from = new Account(Guid.NewGuid(), fixture.Create<decimal>(), Domain.Currency.AUS);

                mockRepo.Setup(p => p.Get(to.Id)).Returns(to);
                mockRepo.Setup(p => p.Get(from.Id)).Returns(from);

                var currency = new Mapper().Get(to.CurrencyType);
                AccountTransferRequest request = new AccountTransferRequest(from.Id, to.Id, from.Balance, Currency.AUS);
                var toBalance = to.Balance;
                var fromBalance = from.Balance;

                sut.Transfer(request);

                Assert.True(to.Balance == toBalance + fromBalance);
            }
        }

        public class TheCreateMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                AccountCreateRequest request,
                                                                AccountService sut)
            {
                var domainCurrency = new Mapper().Get(request.CurrencyType);
                var account = new Account(Guid.NewGuid(), 0, domainCurrency);

                mockFactory.Setup(p => p.Create(domainCurrency)).Returns(account);

                var response = sut.Create(request);

                Assert.True(response.Id == account.Id);
            }

            [Theory(DisplayName = "Verify_Factory_Call_Count"), AutoMoqData]
            public void Verify_Factory_Call_Count([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                AccountCreateRequest request,
                                                                AccountService sut)
            {
                var domainCurrency = new Mapper().Get(request.CurrencyType);
                var account = new Account(Guid.NewGuid(), 0, domainCurrency);

                mockFactory.Setup(p => p.Create(domainCurrency)).Returns(account);

                var response = sut.Create(request);

                mockFactory.Verify(p => p.Create(account.CurrencyType), Times.Once);
            }

            [Theory(DisplayName = "Verify_Repository_Call_Count"), AutoMoqData]
            public void Verify_Repository_Call_Count([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                AccountCreateRequest request,
                                                                AccountService sut)
            {
                var domainCurrency = new Mapper().Get(request.CurrencyType);
                var account = new Account(Guid.NewGuid(), 0, domainCurrency);

                mockFactory.Setup(p => p.Create(domainCurrency)).Returns(account);

                var response = sut.Create(request);

                mockRepo.Verify(p => p.Save(account), Times.Once);
            }
        }

        public class TheGetMethod
        {
            [Theory(DisplayName = "Succeed_With_Valid_Input"), AutoMoqData]
            public void Succeed_With_Valid_Input([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                AccountGetRequest request = new AccountGetRequest(account.Id);

                var result = sut.Get(request);

                Assert.True(account.Id == result.Id);
            }

            [Theory(DisplayName = "Verify_Repository_Call_Count"), AutoMoqData]
            public void Verify_Repository_Call_Count([Frozen]Mock<IAccountFactory> mockFactory,
                                                                [Frozen]Mock<IAccountRepository> mockRepo,
                                                                Account account,
                                                                AccountService sut)
            {
                mockRepo.Setup(p => p.Get(account.Id)).Returns(account);

                AccountGetRequest request = new AccountGetRequest(account.Id);

                var result = sut.Get(request);

                mockRepo.Verify(p => p.Get(account.Id), Times.Once);
            }
        }
    }
}
