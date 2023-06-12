
import sqlite3
import os

# Получаем список автомобилей
def cars_name():
    row_cars = []
    try:
        path = '/'.join((os.path.abspath(__file__).replace('\\', '/')).split('/')[:-1])
        sqlite_connection = sqlite3.connect(os.path.join(path,'cars_manager.db'))
        cursor = sqlite_connection.cursor()
        print("База данных подключена к SQLite")
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

# Получаем список марок автомобилей
def cars_models(model_name):
    row_models = []
    try:
        path = '/'.join((os.path.abspath(__file__).replace('\\', '/')).split('/')[:-1])
        sqlite_connection = sqlite3.connect(os.path.join(path,'cars_manager.db'))
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
# Получаем информацию по выброному автомобилю
def cars_info(model_info):
    row_info = []
    try:
        path = '/'.join((os.path.abspath(__file__).replace('\\', '/')).split('/')[:-1])
        sqlite_connection = sqlite3.connect(os.path.join(path,'cars_manager.db'))
        cursor = sqlite_connection.cursor()
        print("База данных подключена к SQLite")

        for row in cursor.execute(f"select i1.info, i1.price, i1.ImageData from cars_models a join cars_info i1 on i1.id_models=a.id where a.models='{model_info}';"):
            row_info.append((row[0], row[1], row[2]))
        print("Скрипт SQLite успешно выполнен")
        cursor.close()
    except sqlite3.Error as error:
        print("Ошибка при подключении к sqlite", error)
    finally:
        if (sqlite_connection):
            sqlite_connection.close()
            print("Соединение с SQLite закрыто")
    return row_info

# Запись на тест-драйв  
def test_drive(model_name):
    try:
        path = '/'.join((os.path.abspath(__file__).replace('\\', '/')).split('/')[:-1])
        sqlite_connection = sqlite3.connect(os.path.join(path,'cars_manager.db'))
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

# Запись бронирования автомобиля
def booking(model_name):
    try:
        path = '/'.join((os.path.abspath(__file__).replace('\\', '/')).split('/')[:-1])
        sqlite_connection = sqlite3.connect(os.path.join(path,'cars_manager.db'))
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