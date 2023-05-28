using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System.Collections.ObjectModel;

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
        public string employee { get; private set; }
        public string car_model { get; private set; }
        public string telephone { get; private set; }
        public string status { get; private set; }

        public bool SelectLoginPassword(List<dynamic> User)
        {
            var UserGet = false;
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = currentDirectory + @"\data\cars_manager.db";
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = $@"SELECT login, password FROM USERS where login='{User[0]}' and password='{User[1]}'";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader read = fmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        UserGet = true;
                    }
                    Connect.Close();
                }
                
            }
            return UserGet;
        }

        public List<dynamic> SelectUser(string login)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = currentDirectory + @"\data\cars_manager.db";
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
        public List<dynamic> SelectCars()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = currentDirectory + @"\data\cars_manager.db";
            List<dynamic> Cars = new List<dynamic>();
            using (SQLiteConnection Connect = new SQLiteConnection($@"Data Source={path}; Version=3;"))
            {
                Connect.Open();
                using (SQLiteCommand fmd = Connect.CreateCommand())
                {
                    fmd.CommandText = @"select a1.id, a1.name, i1.id, models, quantity, i2.id, info, price from cars_name a1 join cars_models i1 on i1.car_names = a1.id join cars_info i2 on i1.id=i2.id_models;";
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
            var path = currentDirectory + @"\data\cars_manager.db";
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
            var path = currentDirectory + @"\data\cars_manager.db";
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
            var path = currentDirectory + @"\data\cars_manager.db";
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
            var path = currentDirectory + @"\data\cars_manager.db";
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
            var path = currentDirectory + @"\data\cars_manager.db";
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
            var path = currentDirectory + @"\data\cars_manager.db";
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
    }
}
