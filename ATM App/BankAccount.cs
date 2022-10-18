using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ATM_App
{
    public class BankAccount
    {
        public string AccountNumber;
        public string Name;
        public int Pin;
        public int deposit;
        public int withdraw;
        public int selection;
        public string FilePath = @"C:\Users\oyins\Desktop\Oyinda\AtmDetails\";

        public void AtmMenuScreen()
        {
            Console.WriteLine("Welcome to Sukoko Bank ATM Services\nPlease Select a valid Option:\nPress 1 to process a transaction\nPress 2 to close the application.");
            selection = Convert.ToInt32(Console.ReadLine());
            if (selection == 1)
            {
                TransactionMenuScreen();
            }
            else if (selection == 2)
            {
                Console.WriteLine("Thank you for using our application");
                Thread.Sleep(3000);
                System.Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Incorrect Entry.Try Again");
            }
        }

        public void TransactionMenuScreen()
        {
            Console.WriteLine("Welcome to SUKOKO BANK!!!");
            Console.WriteLine("Enter your Name:");
            Name = Console.ReadLine();
            Console.WriteLine("Enter your Account Number:");
            AccountNumber = Console.ReadLine();
            Console.WriteLine("Enter your Pin:");
            Pin = Convert.ToInt32(Console.ReadLine());

            if (File.Exists($"{FilePath}{AccountNumber}.txt"))
            {
                string[] filecheck = File.ReadAllLines($"{FilePath}{AccountNumber}.txt");

                List<string> list = new List<string>();

                list.Add(AccountNumber);
                list.Add(Pin.ToString());

                if (list[1] == filecheck[2])
                {
                    Console.WriteLine("Logging...........\n===============\n");
                    Menu();
                }
                else
                {
                    Console.WriteLine("Wrong Pin");
                    TransactionMenuScreen();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText($"{FilePath}{AccountNumber}.txt"))
                {
                    sw.WriteLine(Name);
                    sw.WriteLine(AccountNumber);
                    sw.WriteLine(Pin);
                }
                using (StreamWriter accountBalanceFile = File.CreateText($"{FilePath}{AccountNumber}ab.txt"))
                {
                    accountBalanceFile.WriteLine(5000);
                }
                using (var bankStatementFile = File.Create($"{FilePath}{AccountNumber}bs.txt"))
                {

                }

                Menu();
            }

        }

        public void Menu()
        {
            Console.WriteLine("Hey " + Name + ", WELCOME!!!");

            while (true)
            {
                Console.WriteLine("What would you like to do?\nPress 1 to Check Account Balance" + "\nPress 2 to Withdraw\nPress 3 to deposit\nPress 4 to go to home screen\nPress 5 to print receipt\nPress 6 to Cancel.");
                Console.Write("Enter your Choice: ");
                selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 1)
                {
                    CheckAccountBalance();
                }
                else if (selection == 2)
                {
                    Withdraw();
                }
                else if (selection == 3)
                {
                    Deposit();
                    
                }
                else if (selection == 4)
                {
                    Console.WriteLine("\n============================================\n");
                    AtmMenuScreen();
                }
                else if (selection == 5)
                {
                    PrintReceipt();
                }
                else if (selection == 6)
                {
                    Console.WriteLine("You are logging out.\nThank you for using our application");
                    Thread.Sleep(3000);
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Incorrect Entry.Try Again");
                    AtmMenuScreen();
                }
            }
        }

        public void CheckAccountBalance()
        {
            var Balance = File.ReadAllText($"{FilePath}{AccountNumber}ab.txt");
            var accountBalance = Convert.ToInt32(Balance);
            Console.WriteLine("Your current account balance is " + accountBalance);
        }

        public void Withdraw()
        {
            Console.WriteLine("Enter the amount to withdraw");

            var Balance = File.ReadAllText($"{FilePath}{AccountNumber}ab.txt");
            var accountBalance = Convert.ToInt32(Balance);
            withdraw = Convert.ToInt32(Console.ReadLine());


            if (accountBalance > withdraw)
            {
                if (withdraw % 10 == 0)
                {
                    var currentBalance = accountBalance - withdraw;
                    Console.WriteLine("Processing...........\n===============\n");
                    Console.WriteLine("Please collect your cash " + withdraw);

                    Console.WriteLine("The current balance is now " + currentBalance);

                    using (StreamWriter accountBalanceFile = File.CreateText($"{FilePath}{AccountNumber}ab.txt"))
                    {
                        accountBalanceFile.WriteLine(currentBalance);
                    }

                    using (StreamWriter receiptFile = File.AppendText($"{FilePath}{AccountNumber}bs.txt"))
                    {
                        receiptFile.WriteLine("Debited - " + withdraw);
                        receiptFile.WriteLine("Current Balance is " + currentBalance);
                    }
                }
                else
                    Console.WriteLine("Please enter the amount in multiples of 10");
            }
            else
            {
                Console.WriteLine("Your account doesn't have sufficient balance");
            }
        }

        public void Deposit()
        {
            Console.WriteLine("Enter the amount to be deposited");

            var Balance = File.ReadAllText($"{FilePath}{AccountNumber}ab.txt");
            var accountBalance = Convert.ToInt32(Balance);
            deposit = Convert.ToInt32(Console.ReadLine());
            var currentBalance = accountBalance + deposit;
            Console.WriteLine("Processing...........\n===============\n");

            using (StreamWriter accountBalanceFile = File.CreateText($"{FilePath}{AccountNumber}ab.txt"))
            {
                accountBalanceFile.WriteLine(currentBalance);
            }

            using (StreamWriter receiptFile = File.AppendText($"{FilePath}{AccountNumber}bs.txt"))
            {
                receiptFile.WriteLine("Credited- " + deposit);
                receiptFile.WriteLine("Current Balance is " + currentBalance);
            }

            Console.WriteLine("The current balance in the account is " + currentBalance);
        }

        public void PrintReceipt()
        {
            var printFile = File.ReadAllText($"{FilePath}{AccountNumber}bs.txt");
            Console.WriteLine("Processing...........\n===============\n");
            Console.WriteLine(printFile);
        }
    }
}
