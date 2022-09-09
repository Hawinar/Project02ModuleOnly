using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project02TestModule
{
    public class Order
    {
        /*
         Рекомендуем к вводу в ваш txt файл, который будет вашей БД:
         Admin|Admin|Сотрудник|
         */
        public string OrderId;
        public string SNP; //Surname Name Patronymic
        public string Vendor;
        public string Model;
        public string TypeOfWork;

        public string Cost;
        public string DateOfAcceptance;

        public string Status;

        public string ordersHistory = string.Empty; //Для заполнения lableOrdersList в клиентском окне


        public string OutputCost; //Для вывода на окно для сотрудников
        public string OutputDateOfAcceptance; //Для вывода на окно для сотрудников
        public string OutputStatus; //Для вывода на окно для сотрудников
        public string SNPvendorModelWork; //Для информации о ФИО, марки автомобиля,модели автомобиля и заказных работ 

        public Order(string orderId, string snp, string vendor, string model, string typeOfWork, string cost, string dateOfAcceptance, string status)
        {
            OrderId = orderId;
            SNP = snp;
            Vendor = vendor;
            Model = model;
            TypeOfWork = typeOfWork;
            Cost = cost;
            DateOfAcceptance = dateOfAcceptance;
            Status = status;
        }
        public void MakeClientOrder(string OrderId, string SNP, string Vendor, string Model, string TypeOfWork, string Cost, string DateOfAcceptance, string Status, string Path)
        {
            var logFile = File.ReadAllText(Path);
            logFile = logFile.Replace("\r\n", "");
            var logList = logFile.ToString().Split("|");

            try
            {
                int i = 7;
                if (i > logList.Length - 1)
                {
                    OrderId = "1";
                }
                else
                {
                    for (i = 7; i < logList.Length - 1; i += 8)
                    {
                        OrderId = logList[i - 7];
                        int tmp = int.Parse(OrderId);
                        tmp++;
                        OrderId = tmp.ToString();
                    }
                }

            }
            catch
            {

            }

            List<Order> orders = new List<Order>();
            {
                orders.Add(new Order(OrderId, SNP, Vendor, Model, TypeOfWork, Cost, DateOfAcceptance, Status));
            }

            using (StreamWriter sw = new StreamWriter(Path, true, Encoding.Unicode))
            {
                sw.WriteLine((orders[orders.Count - 1].OrderId + "|" + orders[orders.Count - 1].SNP + "|" + orders[orders.Count - 1].Vendor + "|" + orders[orders.Count - 1].Model + "|"
                    + orders[orders.Count - 1].TypeOfWork + "|" + orders[orders.Count - 1].Cost + "|" + orders[orders.Count - 1].DateOfAcceptance + "|"
                    + orders[orders.Count - 1].Status + "|").ToString());
                sw.Dispose();
            }
        }
        public void GetMyOrders(string inputSNP, string Path)
        {
            List<Order> orders = new List<Order>();

            byte Coincidences = 0; //Совпадений

            using (StreamReader sr = new StreamReader(Path, Encoding.Unicode))
            {
                var logFile = File.ReadAllText(Path);
                logFile = logFile.Replace("\r\n", "");
                var logList = logFile.ToString().Split("|");

                for (int i = 7; i < logList.Length - 1; i += 8)
                {
                    OrderId = logList[i - 7];
                    SNP = logList[i - 6];
                    Vendor = logList[i - 5];
                    Model = logList[i - 4];
                    TypeOfWork = logList[i - 3];
                    Cost = logList[i - 2];
                    DateOfAcceptance = logList[i - 1];
                    Status = logList[i];
                    orders.Add((Order)new Order(OrderId, SNP, Vendor, Model, TypeOfWork, Cost, DateOfAcceptance, Status));
                }
                sr.Dispose();
                for (int j = 0; j < orders.Count; j += 1)
                {
                    if (inputSNP == orders[j].SNP)
                    {
                        ordersHistory += orders[j].OrderId + " " + orders[j].Vendor + " " + orders[j].Model + " " + orders[j].TypeOfWork + " " + orders[j].Cost + " " + orders[j].DateOfAcceptance + " " + orders[j].Status + "\n";
                        Coincidences++;
                    }
                }
                if (Coincidences == 0)
                {
                    ordersHistory = "Вы ещё не совершали записей";
                }
            }
        }
        public void GetOrderAtId(string inputId, string Path)
        {
            List<Order> orders = new List<Order>();

            byte Coincidences = 0; //Совпадений

            using (StreamReader sr = new StreamReader(Path))
            {
                var logFile = File.ReadAllText(Path);
                logFile = logFile.Replace("\r\n", "");
                var logList = logFile.ToString().Split("|");

                for (int i = 7; i < logList.Length; i += 8)
                {

                    OrderId = logList[i - 7];
                    SNP = logList[i - 6];
                    Vendor = logList[i - 5];
                    Model = logList[i - 4];
                    TypeOfWork = logList[i - 3];
                    Cost = logList[i - 2];
                    DateOfAcceptance = logList[i - 1];
                    Status = logList[i];
                    orders.Add((Order)new Order(OrderId, SNP, Vendor, Model, TypeOfWork, Cost, DateOfAcceptance, Status));
                }
                sr.Dispose();
                for (int j = 0; j < orders.Count; j += 1)
                {
                    if (inputId == (orders[j].OrderId).ToString())
                    {
                        SNPvendorModelWork = "Заказчик:" + " " + orders[j].SNP + "\n" + "Марка:" + " " + orders[j].Vendor + "\n" + "Модель:" + " " + orders[j].Model + "\n" + "Заказные работы:" + " " + orders[j].TypeOfWork;
                        OutputCost = orders[j].Cost;
                        OutputDateOfAcceptance = orders[j].DateOfAcceptance;
                        OutputStatus = orders[j].Status;
                        Coincidences++;
                    }
                }
                if (Coincidences == 0)
                {
                    ordersHistory = "Заказа по такому номеру не существует";
                }
            }
        }
        public void ChangeOrder(string inputId, string Path, string newCost, string newDateOfAcceptance, string newStatus)
        {
            List<Order> orders = new List<Order>();
            using (StreamReader sr = new StreamReader(Path))
            {
                var logFile = File.ReadAllText(Path);
                logFile = logFile.Replace("\r\n", "");
                var logList = logFile.ToString().Split("|");

                for (int i = 7; i < logList.Length; i += 8)
                {

                    OrderId = logList[i - 7];
                    SNP = logList[i - 6];
                    Vendor = logList[i - 5];
                    Model = logList[i - 4];
                    TypeOfWork = logList[i - 3];
                    Cost = logList[i - 2];
                    DateOfAcceptance = logList[i - 1];
                    Status = logList[i];
                    orders.Add((Order)new Order(OrderId, SNP, Vendor, Model, TypeOfWork, Cost, DateOfAcceptance, Status));
                }
                sr.Dispose();

                for (int j = 7; j < logList.Length - 1; j += 8)
                {
                    if (inputId == logList[j - 7].ToString())
                    {

                        FileStream fs = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);


                        using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                        {
                            logList[j - 2] = logList[j - 2].Replace(logList[j - 2], newCost);
                            logList[j - 1] = logList[j - 1].Replace(logList[j - 1], newDateOfAcceptance);
                            logList[j] = logList[j].Replace(logList[j], newStatus);

                            string toWrite = string.Empty;
                            for (int k = 7; k < logList.Length; k += 8)
                            {
                                toWrite = ($"{logList[k - 7]}|{logList[k - 6]}|{logList[k - 5]}|{logList[k - 4]}|{logList[k - 3]}|{logList[k - 2]}|{logList[k - 1]}|{logList[k]}|\n");
                                sw.Write(toWrite);
                            }
                            sw.Dispose();

                        }
                        fs.Dispose();


                    }
                }
            }
        }
    }
    public class Account
    {
        public string Response = string.Empty; //Для метода register, для проверки на существующий аккаунт

        public string SNP;
        public string Password;
        public string Role;

        public Account(string snp, string password, string role)
        {
            SNP = snp;
            Password = password;
            Role = role;
        }
        public void Register(string SNP, string Password, string Role, string Path, string inputSNP, string inputPassword)
        {
            int accountCount = 0;
            List<Account> accounts = new List<Account>();
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
            byte Coincidences = 0; //Совпадений
            for (int j = 0; j < accounts.Count; j++)
            {

                if (accounts[j].SNP == inputSNP)
                {
                    Coincidences++;
                }
            }
            if (Coincidences == 0)
            {
                accounts[accounts.Count - 1].SNP = inputSNP;
                accounts[accounts.Count - 1].Password = inputPassword;
                accounts[accounts.Count - 1].Role = "Клиент";
                using (StreamWriter sw = new StreamWriter(Path, true))
                {
                    sw.WriteLine((accounts[accounts.Count - 1].SNP + "|" + accounts[accounts.Count - 1].Password + "|" + accounts[accounts.Count - 1].Role + "|").ToString());
                    sw.Dispose();
                }
            }
            else
            {
                Response = "Такой уже есть";
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
            for (int j = 0; j < accounts.Count; j += 1)
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

