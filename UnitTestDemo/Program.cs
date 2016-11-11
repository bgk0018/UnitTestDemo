

using System;

namespace Service
{
    class Program
    {
        private static string EXIT = "X";

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1 Create Account");
                Console.WriteLine("2 Get Account");
                Console.WriteLine("3 Withdraw");
                Console.WriteLine("4 Deposit");
                Console.WriteLine("5 Transfer");
                Console.WriteLine("X Exit");

                var response = Console.ReadLine();

                if (response.ToUpper() == EXIT)
                    break;

                SelectAction(response);

            } while (true);
        }

        private static void SelectAction(string response)
        {
            switch(response)
            {
                case "1":
                    Create();
                    break;
                case "2":
                    GetAccount();
                    break;
                case "3":
                    Withdraw();
                    break;
                case "4":
                    Deposit();
                    break;
                case "5":
                    Transfer();
                    break;
                default:
                    Console.WriteLine("I don't recognize that menu item.");
                    break;
            }
        }

        private static void Transfer()
        {
            var service = new AccountService();

            service.Transfer(new Requests.AccountTransferRequest("1234"))
        }

        private static void Deposit()
        {
            throw new NotImplementedException();
        }

        private static void Withdraw()
        {
            throw new NotImplementedException();
        }

        private static void GetAccount()
        {
            throw new NotImplementedException();
        }

        private static void Create()
        {
            throw new NotImplementedException();
        }
    }
}
