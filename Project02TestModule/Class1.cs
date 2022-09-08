using System;
using System.Collections.Generic;
using System.IO;

namespace Project02TestModule
{
    public class Order
    {

        public string SNP; //Surname Name Patronymic
        public string Vendor;
        public string Model;
        public string TypeOfWork;

        public string Cost;
        public string DateOfAcceptance;

        public string Status;

        public Order(string snp, string vendor, string model, string typeOfWork, string cost, string dateOfAcceptance, string status)
        {
            SNP = snp;
            Vendor = vendor;
            Model = model;
            TypeOfWork = typeOfWork;
            Cost = cost;
            DateOfAcceptance = dateOfAcceptance;
            Status = status;
        }
        public void MakeClientOrder(string SNP, string Vendor, string Model, string TypeOfWork, string Cost, string DateOfAcceptance, string Status, string Path)
        {
            List<Order> ordersClient = new List<Order>();
            {
                new Order(SNP, Vendor, Model, TypeOfWork,Cost, DateOfAcceptance, Status);
            }
            
            using (StreamWriter sw = new StreamWriter(Path, true))
            {
                sw.WriteLine((ordersClient[ordersClient.Count - 1].SNP + "|" + ordersClient[ordersClient.Count - 1].Vendor + "|" + ordersClient[ordersClient.Count - 1].Model + "|").ToString());
                sw.Dispose();
            }
        }
    }
    public class Account
    {
        public string SNP;
        public string Password;
        public string Role;

        public Account(string snp, string password, string role)
        {
            SNP = snp;
            Password = password;
            Role = role;
        }
        public void Register(string SNP, string Password, string Role, string Path)
        {
            List<Account> accounts = new List<Account>();
            {
                accounts.Add(new Account(SNP, Password, Role));
            }

            using(StreamWriter sw = new StreamWriter(Path, true))
            {
                sw.WriteLine((accounts[accounts.Count - 1].SNP + "|" + accounts[accounts.Count - 1].Password + "|" + accounts[accounts.Count - 1].Role + "|").ToString());
                sw.Dispose();
            }
        }
        public string Login(string SNP, string Password, string Role, string Path, string inputSNP, string inputPassword)
        {
            List<Account> accounts = new List<Account>();
            int accountCount = 0;
            string Response = string.Empty; //ИМЕННО эта переменная отвечает за то, какое окно будет открыто в приложении
            using (StreamReader sr = new StreamReader(Path))
            {
                var logFile = File.ReadAllText(Path);
                logFile = logFile.Replace("\r\n", "");
                var logList = logFile.ToString().Split("|");

                for (int i = 3; i < logList.Length; i += 3)
                {
                    SNP = logList[i - 3];
                    Password = logList[i - 2];
                    Role = logList[i - 1];
                    accounts.Add((Account)new Account(SNP, Password, Role));
                    accountCount++;
                }
                sr.Dispose();
            }
            for(int j = 0; j<accounts.Count; j += 1)
            {
                if (inputSNP == accounts[j].SNP && inputPassword == accounts[j].Password)
                {
                    Response = "S"; //Successful - успешно
                    if (Response == "S" && accounts[j].Role == "Клиент")
                    {
                        Response = "SC"; //Successful Client - Успешно, клиент
                    }
                    else
                    {
                        Response = "SE"; //Successful Employee - Успешно, сотрудник
                    }
                }
            }
           
            
            return Response;
        }

    }
}
