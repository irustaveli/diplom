
import sqlite3

def cars_name():
    row_cars = []
    try:
        sqlite_connection = sqlite3.connect('sqlite.db')
        cursor = sqlite_connection.cursor()
        print("База данных подключена к SQLite")

        # with open('namecars.sql', 'r') as sqlite_file:
        #     sql_script = sqlite_file.read()

        # cursor.executescript(sql_script)
        for row in cursor.execute("SELECT name FROM cars_name"):
            row_cars.append(row[0])
        print("Скрипт SQLite успешно выполнен")
        cursor.close()
    except sqlite3.Error as error:
        print("Ошибка при подключении к sqlite", error)
    finally:
        if (sqlite_connection):
            sqlite_connection.close()
            print("Соединение с SQLite закрыто")
    return row_cars

def cars_models(model_name):
    row_models = []
    try:
        sqlite_connection = sqlite3.connect('sqlite.db')
        cursor = sqlite_connection.cursor()
        print("База данных подключена к SQLite")

        for row in cursor.execute(f"select i1.models from cars_name a join cars_models i1 on i1.car_names=a.id where a.name='{model_name}';"):
            row_models.append(row[0])
        print("Скрипт SQLite успешно выполнен")
        cursor.close()
    except sqlite3.Error as error:
        print("Ошибка при подключении к sqlite", error)
    finally:
        if (sqlite_connection):
            sqlite_connection.close()
            print("Соединение с SQLite закрыто")
    return row_models

def cars_info(model_info):
    row_info = []
    try:
        sqlite_connection = sqlite3.connect('sqlite.db')
        cursor = sqlite_connection.cursor()
        print("База данных подключена к SQLite")

        for row in cursor.execute(f"select i1.info, i1.price from cars_models a join cars_info i1 on i1.id_models=a.id where a.models='{model_info}';"):
            row_info.append((row[0], row[1]))
        print("Скрипт SQLite успешно выполнен")
        cursor.close()
    except sqlite3.Error as error:
        print("Ошибка при подключении к sqlite", error)
    finally:
        if (sqlite_connection):
            sqlite_connection.close()
            print("Соединение с SQLite закрыто")
    return row_info

def test_drive(model_name):
    try:
        sqlite_connection = sqlite3.connect('sqlite.db')
        cursor = sqlite_connection.cursor()
        print("База данных подключена к SQLite")
        print(model_name[1])
        cursor.execute(f"insert into test_drive (FIO, cars_model, date_record, telephone) values ('{model_name[1]}', '{model_name[0]}', '{model_name[2]}', '{model_name[3]}');")
        print("Скрипт SQLite успешно выполнен")
        sqlite_connection.commit()
        cursor.close()
    except sqlite3.Error as error:
        print("Ошибка при подключении к sqlite", error)
    finally:
        if (sqlite_connection):
            sqlite_connection.close()
            print("Соединение с SQLite закрыто")
    return 1

def booking(model_name):
    try:
        sqlite_connection = sqlite3.connect('sqlite.db')
        cursor = sqlite_connection.cursor()
        print("База данных подключена к SQLite")
        print(model_name[1])
        cursor.execute(f"insert into booking (FIO, car_model, telephone) values ('{model_name[1]}', '{model_name[0]}', '{model_name[2]}');")
        sqlite_connection.commit()
        print("Скрипт SQLite успешно выполнен")
        cursor.close()
    except sqlite3.Error as error:
        print("Ошибка при подключении к sqlite", error)
    finally:
        if (sqlite_connection):
            sqlite_connection.close()
            print("Соединение с SQLite закрыто")
    return 1