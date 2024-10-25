Функциональность: курс валют ЦБ РФ
	Как пользователь
	Я хочу просматривать валютные пары
	Чтобы быть в курсе актуальных и прошедших котировок


@positive
Сценарий: импорт из файла, просмотр всех валютных пар к рублю за указанную дату
	Пусть я импортировал курсы из файла за дату "2008-08-26"
	Когда я запрашиваю курсы за дату "2008-08-26"
	Тогда курсы имеют дату "2008-08-26"
	И получено курсов в количестве 18
	И элемент №1 курсов имеет код "AUD" с названием "Австралийский доллар" и обменом 21,1568
	И элемент №2 курсов имеет код "BYR" с названием "Белорусских рублей" и обменом 0,0115714

@positive
Сценарий: импорт из памяти, просмотр всех валютных пар к рублю за указанную дату
	Пусть я импортировал курсы из памяти за дату "2024-10-11"
	Когда я запрашиваю курсы за дату "2024-10-11"
	Тогда курсы имеют дату "2024-10-11"
	И получено курсов в количестве 2
	И элемент №1 курсов имеет код "HKD" с названием "Гонконгский доллар" и обменом 12,3907
	И элемент №2 курсов имеет код "JPY" с названием "Японских иен" и обменом 0,647076
