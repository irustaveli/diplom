﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace SQLlite_setting
{
    internal class SQLlite
    {
        //public string id_car_name { get; private set; }
        public string id_car_name { get; private set; }
        public string car_name { get; private set; }
        public string id_modelse { get; private set; }
        public string models { get; private set; }
        public string quantity { get; private set; }
        public string id_info_models { get; private set; }
        public string info { get; private set; }
        public string price { get; private set; }
        public string FIO { get; private set; }
        public string login { get; private set; }
        public string password { get; private set; }
        public string role { get; private set; }

        public string employee { get; private set; }
        public string car_model { get; private set; }
        public string telephone { get; private set; }
        public string status { get; private set; }
        public byte[] Data { get; private set; }

        public List<dynamic> SelectLoginPassword (List<dynamic> User)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            var user_role = "";
            List<dynamic> UserGet = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"SELECT login, password, role FROM USERS where login='{User[0]}' and password='{User[1]}'";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        UserGet.Add("success");
                    }
                    while (read.Read())
                    {
                        SQLlite user = new SQLlite();
                        user.role = read.GetValue(2).ToString();
                        UserGet.Add(user.role);
                    }
                    Connect.Close();
                }
                
            }
            return UserGet;
        }

        public List<dynamic> SelectUser(string login)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            List<dynamic> User = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"select FIO, role from users where login = '{login}';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    while (read.Read())
                    {
                        SQLlite user = new SQLlite();
                        user.FIO = read.GetValue(0).ToString();
                        user.info = read.GetValue(1).ToString();
                        User.Add(user);
                    }
                    Connect.Close();
                }
                
            }
            return User;
        }
        public List<dynamic> SelectUsers()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            List<dynamic> Users = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"select FIO, login, password, role from users;";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    while (read.Read())
                    {
                        SQLlite user = new SQLlite();
                        user.FIO = read.GetValue(0).ToString();
                        user.login = read.GetValue(1).ToString();
                        user.password = read.GetValue(2).ToString();
                        user.role = read.GetValue(3).ToString();
                        Users.Add(user);
                    }
                    Connect.Close();
                }

            }
            return Users;
        }
        public int UpdateUsers(string FIO, string login, string password, string role)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            List<dynamic> Users = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"UPDATE users SET FIO={FIO}, login={login}, password={password}, role={role} WHERE login = '{login}';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                   

                    Connect.Close();
                }

            }
            return 1;
        }
        public int InsertUsers(string FIO, string login, string password, string role)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            List<dynamic> Users = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"Insert into users SET FIO={FIO}, login={login}, password={password}, role={role};";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();


                    Connect.Close();
                }

            }
            return 1;
        }
        public int DeleteUsers(string login)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            List<dynamic> Users = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"DELETE users cars_info WHERE id='{login}';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();


                    Connect.Close();
                }

            }
            return 1;
        }

        public List<dynamic> SelectCars()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            List<dynamic> Cars = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = @"select a1.id, a1.name, i1.id, models, quantity, i2.id, info, price, ImageData from cars_name a1 join cars_models i1 on i1.car_names = a1.id join cars_info i2 on i1.id=i2.id_models;";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    while (read.Read())
                    {
                        SQLlite cars = new SQLlite();
                        cars.id_car_name = read.GetValue(0).ToString();   
                        cars.car_name = read.GetValue(1).ToString();
                        cars.id_modelse = read.GetValue(2).ToString();
                        cars.models = read.GetValue(3).ToString();
                        cars.quantity = read.GetValue(4).ToString();
                        cars.id_info_models = read.GetValue(5).ToString();
                        cars.info = read.GetValue(6).ToString();
                        cars.price = read.GetValue(7).ToString();
                        if (read.GetValue(8) != System.DBNull.Value){
                            cars.Data = (byte[])read.GetValue(8);
                        }
                        Cars.Add(cars);
                    }
                    Connect.Close();
                }
            }
            return Cars;

        }

        public List<dynamic> SelectBoking()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            List<dynamic> Cars = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT FIO, employee, car_model, telephone, status FROM booking;";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    while (read.Read())
                    {
                        SQLlite cars = new SQLlite();
                        cars.FIO = read.GetValue(0).ToString();
                        cars.employee = read.GetValue(1).ToString();
                        cars.car_model = read.GetValue(2).ToString();
                        cars.telephone = read.GetValue(3).ToString();
                        cars.status = read.GetValue(4).ToString();
                        Cars.Add(cars);
                    }
                    Connect.Close();
                }

            }
            return Cars;
        }

        public void InsertBokingStatus(string status, string fio, string employee)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"UPDATE booking SET status='{status}', employee='{employee}' WHERE FIO='{fio}';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    
                    Connect.Close();
                }

            }
        }

        public string countBooking()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            string count_booking = "";
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = @"select count(*) from booking where status = 'В работе';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    while(read.Read())
                    {
                        count_booking = read[0].ToString();
                    }
                    
                    Connect.Close();
                }

            }
            return count_booking;
        }

        public List<dynamic> SelectTestDrive()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            List<dynamic> Cars = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT FIO, employee, cars_model, telephone, status FROM test_drive;";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    while (read.Read())
                    {
                        SQLlite cars = new SQLlite();
                        cars.FIO = read.GetValue(0).ToString();
                        cars.employee = read.GetValue(1).ToString();
                        cars.car_model = read.GetValue(2).ToString();
                        cars.telephone = read.GetValue(3).ToString();
                        cars.status = read.GetValue(4).ToString();
                        Cars.Add(cars);
                    }
                    Connect.Close();
                }

            }
            return Cars;
        }

        public void InsertTestDriverStatus(string status, string fio, string employee)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"UPDATE test_drive SET status='{status}', employee='{employee}' WHERE FIO='{fio}';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();

                    Connect.Close();
                }

            }
        }

        public string countTestDrive()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            string count_testdrive = "";
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = @"select count(*) from test_drive where status = 'В работе';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    while (read.Read())
                    {
                        count_testdrive = read[0].ToString();
                    }

                    Connect.Close();
                }

            }
            return count_testdrive;
        }
        public int InsertData(string car_name, string model_name, string quantity, string info, string price, string filename)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            // сначала считываем файл из файловой системы
            // массив для хранения бинарных данных файла
            byte[] imageData;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                imageData = new byte[fs.Length];
                fs.Read(imageData, 0, imageData.Length);
            }
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"Insert into cars_name name VALUES {car_name}';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();

                }
                using (SQLiteCommand fmd1 = Connect.CreateCommand())
                {
                    fmd1.CommandText = $@"Insert into cars_models models, quantity VALUES '{model_name}', '{quantity}';";
                    fmd1.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd1.ExecuteReader();

                }
                using (SQLiteCommand fmd2 = Connect.CreateCommand())
                {
                    fmd2.CommandText = $@"Insert into cars_info (info, price, ImageData) VALUES (@Info, @Price, @ImageData);";
                    fmd2.Parameters.Add(new SQLiteParameter("@Info", info));
                    fmd2.Parameters.Add(new SQLiteParameter("@Price", price));
                    fmd2.Parameters.Add(new SQLiteParameter("@ImageData", imageData));
                    int number = fmd2.ExecuteNonQuery();
                    Connect.Close();
                }

            }
            return 1;
        }
        public int UpdateData(string id_car, string car_name, string id_model, string model_name, string quantity, string id_ifo, string info, string price, string filename)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            // сначала считываем файл из файловой системы
            // массив для хранения бинарных данных файла
            byte[] imageData;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                imageData = new byte[fs.Length];
                fs.Read(imageData, 0, imageData.Length);
            }
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"UPDATE cars_name SET name='{car_name}' WHERE id='{id_car}';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();

                }
                using (SQLiteCommand fmd1 = Connect.CreateCommand())
                {
                    fmd1.CommandText = $@"UPDATE cars_models SET models='{model_name}', quantity='{quantity}' WHERE id='{id_model}';";
                    fmd1.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd1.ExecuteReader();

                }
                using (SQLiteCommand fmd2 = Connect.CreateCommand())
                {
                    fmd2.CommandText = @"UPDATE cars_info SET info=@Info, price=@Price, ImageData=@ImageData WHERE id=@ID_info;";
                    //fmd2.CommandType = CommandType.Text;
                    //SQLiteDataReader read = fmd2.ExecuteReader();
                    fmd2.Parameters.Add(new SQLiteParameter("@Info", info));
                    fmd2.Parameters.Add(new SQLiteParameter("@Price", price));
                    fmd2.Parameters.Add(new SQLiteParameter("@ImageData", imageData));
                    fmd2.Parameters.Add(new SQLiteParameter("@ID_info", id_ifo));
                    int number = fmd2.ExecuteNonQuery();
                    Connect.Close();
                }

            }
            return 1;
        }
        public int DelData(string id_info, string id_model, string id_car)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\data\\cars_manager.db");
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"DELETE FROM cars_info WHERE id='{id_info}';";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();

                }
                using (SQLiteCommand fmd1 = Connect.CreateCommand())
                {
                    fmd1.CommandText = $@"DELETE FROM cars_models WHERE id='{id_model}';";
                    fmd1.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd1.ExecuteReader();

                }
                using (SQLiteCommand fmd2 = Connect.CreateCommand())
                {
                    fmd2.CommandText = $@"DELETE FROM cars_name WHERE id='{id_car}';";
                    fmd2.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd2.ExecuteReader();

                    Connect.Close();
                }

            }
            return 1;
        }
    }
}
