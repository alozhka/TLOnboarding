Функциональность: запуск иморта курсов валют из API ЦБ РФ по расписанию

# API ЦБРФ подменяется заглушкой

    @hangfire
    @time_travel
    Сценарий: автоматический импорт данных из API ЦБРФ
        Пусть по Москве было время "2021-09-21 8:30:00"

        Когда запускается импорт курсов валют из API ЦБ РФ

        Тогда за дату "2021-09-21" будут курсы:
          | CurrencyCode | CurrencyName        | ExchangeRate |
          | EGP          | Египетских фунтов   | 1,9082300000 |
          | HUF          | Венгерских форинтов | 0,2621020000 |
          | VND          | Вьетнамских донгов  | 0,0038340600 |