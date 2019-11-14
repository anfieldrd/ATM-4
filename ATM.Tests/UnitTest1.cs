using System;
using System.Web.Mvc;
using ATM.Controllers;
using ATM.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ATM.Models.ApplicationDbContext;

namespace ATM.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FooActionReturnsBaoutView()
        {
            var homeController = new HomeController();
            var result = homeController.Foo() as ViewResult;
            Assert.AreEqual("About", result.ViewName);
        }

        [TestMethod]
        public void ContactFormSaysThanks()
        {
            var homeController = new HomeController();
            var result = homeController.Contact("I like your bank.") as ViewResult;
            Assert.IsNotNull(result.ViewBag.TheMessage);
        }

        [TestMethod]
        public void BalanceIsCorrectAfterDeposit()
        {
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.CheckingAccounts = new FakeDbSet<CheckingAccount>();

            var checkingAccount = new CheckingAccount { Id=1, AccountNumber="000123TEST", Balance=0 };
            fakeDb.CheckingAccounts.Add(checkingAccount);

            fakeDb.Transactions = new FakeDbSet<Transaction>();
            var transactionController = new TransactionController(fakeDb);

            transactionController.Deposit(new Transaction { CheckingAccountId = 1, Amount = 25 });
            
            Assert.AreEqual(25, checkingAccount.Balance);
        }
    }
}
