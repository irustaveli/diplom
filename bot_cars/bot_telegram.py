import telebot
import ast
import time
from telebot import types
import sql_request
import configparser

config = configparser.ConfigParser()  # создаём объекта парсера
config.read("bot.ini")  # создаем файл, записываем в него токен и читаем конфиг
token = config["Bot"]["token"]

bot = telebot.TeleBot(token)
message_text = ""
notes=[]
chat_id = ""

@bot.message_handler(commands=['start'])
def button_message(message):
    markup=types.ReplyKeyboardMarkup(resize_keyboard=True)
    item1=types.KeyboardButton("Информация об автомобили")
    item2=types.KeyboardButton("Записаться на тест-драйв")
    item3=types.KeyboardButton("Забронировать автомобиль")
    markup.add(item1,item2,item3)
    bot.send_message(message.chat.id, 'Привет, я чат-бот по подбору автомобилей', reply_markup=markup)

# Получаем список автомобилей и выводим названия кнопок
def makeKeyboard(stringList):
    markup = types.InlineKeyboardMarkup()

    for value in stringList:

        markup.add(types.InlineKeyboardButton(text=value,
                                               callback_data="['value', '" + value + "']"))

    return markup

def makeKeyboard1(stringList):
    markup = types.InlineKeyboardMarkup()

    for value in stringList:
        markup.add(types.InlineKeyboardButton(text=value,
                                               callback_data="['info', '" + value + "']"))
    return markup

# Выводим меню в боте
@bot.message_handler(content_types=['text'])
def handle_command_adminwindow(message):
    global message_text
    global chat_id
    message_text = message.text
    chat_id=message.chat.id
    if (message_text == 'Информация об автомобили') or (message_text == 'Записаться на тест-драйв') or (message_text == 'Забронировать автомобиль'):
        stringList = sql_request.cars_name()
        bot.send_message(chat_id,
                        text="Выберите марку автомобиля:",
                        reply_markup=makeKeyboard(stringList),
                        parse_mode='HTML')
    
# Выводим список марок автомобилей
@bot.callback_query_handler(func=lambda call: True)
def handle_command_adminwindow(call):
    if (call.data.startswith("['value'")):
        print(f"call.data : {call.data} , type : {type(call.data)}")
        print(f"ast.literal_eval(call.data) : {ast.literal_eval(call.data)} , type : {type(ast.literal_eval(call.data))}")
        valueFromCallBack = ast.literal_eval(call.data)[1]
        stringList = sql_request.cars_models(valueFromCallBack)
        bot.send_message(chat_id,
                     text="Выберите модель:",
                     reply_markup=makeKeyboard1(stringList),
                     parse_mode='HTML')
    
    if (call.data.startswith("['key'")):
        keyFromCallBack = ast.literal_eval(call.data)[1]
        del stringList[keyFromCallBack]
        bot.edit_message_text(chat_id,
                              text="Here are the values of stringList",
                              message_id=call.message.message_id,
                              reply_markup=makeKeyboard(),
                              parse_mode='HTML')
    
    if message_text == 'Информация об автомобили':
        if (call.data.startswith("['info'")):
            print(f"call.data : {call.data} , type : {type(call.data)}")
            print(f"ast.literal_eval(call.data) : {ast.literal_eval(call.data)} , type : {type(ast.literal_eval(call.data))}")
            valueFromCallBack = ast.literal_eval(call.data)[1]
            stringList = sql_request.cars_info(valueFromCallBack)
            bot.send_message(chat_id, 
                            text=f"Цена {valueFromCallBack} *{stringList[0][1]} RUB*\n*Информация по автомобилю:*\n{stringList[0][0]}",
                            parse_mode= "Markdown")
    
    elif message_text == 'Записаться на тест-драйв':
        if (call.data.startswith("['info'")):
            print(f"call.data : {call.data} , type : {type(call.data)}")
            print(f"ast.literal_eval(call.data) : {ast.literal_eval(call.data)} , type : {type(ast.literal_eval(call.data))}")
            valueFromCallBack = ast.literal_eval(call.data)[1]
            notes.append(valueFromCallBack)
            t1 = bot.send_message(call.message.chat.id, "Введите ваше ФИО")
            bot.register_next_step_handler(t1, text_fio)
        if (call.data.startswith("['test_drive'")):
            print(f"call.data : {call.data} , type : {type(call.data)}")
            print(f"ast.literal_eval(call.data) : {ast.literal_eval(call.data)} , type : {type(ast.literal_eval(call.data))}")
            valueFromCallBack = ast.literal_eval(call.data)[1]
            res = sql_request.test_drive(notes)
            if res == 1:
                bot.answer_callback_query(callback_query_id=call.id,
                                        show_alert=True,
                                        text="Вы успешно забронировали автомобиль")
            else:
                bot.answer_callback_query(callback_query_id=call.id,
                                        show_alert=True,
                                        text="Произошла ошибка, попробуйте повторить позже")
    elif message_text == 'Забронировать автомобиль':
        if (call.data.startswith("['info'")):
            print(f"call.data : {call.data} , type : {type(call.data)}")
            print(f"ast.literal_eval(call.data) : {ast.literal_eval(call.data)} , type : {type(ast.literal_eval(call.data))}")
            valueFromCallBack = ast.literal_eval(call.data)[1]
            notes.append(valueFromCallBack)
            t1 = bot.send_message(call.message.chat.id, "Введите ваше ФИО")
            bot.register_next_step_handler(t1, text_fio)    
        if (call.data.startswith("['booking'")):
            print(f"call.data : {call.data} , type : {type(call.data)}")
            print(f"ast.literal_eval(call.data) : {ast.literal_eval(call.data)} , type : {type(ast.literal_eval(call.data))}")
            valueFromCallBack = ast.literal_eval(call.data)[1]
            res = sql_request.booking(notes)
            if res == 1:
                bot.answer_callback_query(callback_query_id=call.id,
                                        show_alert=True,
                                        text="Вы успешно забронировали автомобиль")
            else:
                bot.answer_callback_query(callback_query_id=call.id,
                                        show_alert=True,
                                        text="Произошла ошибка, попробуйте повторить позже")
# Записываемся на тест-драйв
@bot.message_handler(content_types=["text"])
def text_fio(message):
    if message_text == 'Записаться на тест-драйв':
        notes.append(message.text)
        t1 = bot.send_message(message.chat.id, "Дата записи (формат mm/dd/yy, например 05/21/23)")
        bot.register_next_step_handler(t1, text_date)
    elif message_text == 'Забронировать автомобиль':
        notes.append(message.text)
        t1 = bot.send_message(message.chat.id, "Введите номер телефона для обратной связи")
        bot.register_next_step_handler(t1, text_date)
@bot.message_handler(content_types=["text"])
def text_date(message):
    if message_text == 'Записаться на тест-драйв':
        notes.append(message.text)
        t1 = bot.send_message(message.chat.id, "Введите номер телефона для обратной связи")
        bot.register_next_step_handler(t1, text_tel)
    elif message_text == 'Забронировать автомобиль':
        notes.append(message.text)
        t1 = bot.send_message(message.chat.id, 
                        f"ФИО клиента: {notes[1]}, Автомобиль: {notes[0]}, Номер для обратной связи: {notes[2]}", 
                        reply_markup=button_booking_message(), parse_mode='HTML')
        bot.register_next_step_handler(t1, handle_command_send)
@bot.message_handler(content_types=["text"])
def text_tel(message):
    notes.append(message.text)
    t1 = bot.send_message(message.chat.id, 
                    f"ФИО клиента: {notes[1]}, Автомобиль: {notes[0]}, Дата посещения: {notes[2]}, Номер для обратной связи: {notes[3]}", 
                    reply_markup=button_testdrive_message(), parse_mode='HTML')
    bot.register_next_step_handler(t1, handle_command_send)
    
@bot.callback_query_handler(func=lambda call: call.data == 'test_drive')
def handle_command_send(call):
    if call.data == "test_drive":
        #res = sql_request.test_drive(notes)
        bot.answer_callback_query(chat_id,
                                    show_alert=True,
                                    text="Вы успешно записались на тест-драйв")
# Подтверждаем запись на тест-драйв в бд 
def button_testdrive_message():
    markup = types.InlineKeyboardMarkup()
    switch_button = types.InlineKeyboardButton(text='Отправить информацию?', callback_data="['test_drive', 'Отправить информацию?']")
    markup.add(switch_button)
    return markup

# Подтверждаем запись бронирования в бд 
def button_booking_message():
    markup = types.InlineKeyboardMarkup()
    switch_button = types.InlineKeyboardButton(text='Отправить информацию?', callback_data="['booking', 'Отправить информацию?']")
    markup.add(switch_button)
    return markup    

# Включаем постоянно бот
while True:
    try:
        print("Бот успешно запущен")
        bot.polling(none_stop=True, interval=0, timeout=0)
    except:
        time.sleep(10)