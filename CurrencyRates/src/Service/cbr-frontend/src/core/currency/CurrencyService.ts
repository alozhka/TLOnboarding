import { CurrencyRate, DayCurrencyRates } from "~/core/types";
import { CurrencyRatesMock } from "../mock";
import { axiosClassic } from "../requests/axios";
import dayjs from "dayjs";

class CurrencyService {
  public static async getCurrencyRatesByDate(date: string | null): Promise<DayCurrencyRates> {
    let requestDate: string;

    if (date !== null) {
      requestDate = dayjs(date).format("YYYY/MM/DD")
    }
    else {
      requestDate = dayjs(Date.now()).format("YYYY/MM/DD")
    }

    return axiosClassic.get<DayCurrencyRates>('cbr/daily-rates?requestDate=' + requestDate)
      .then(r => r.data)
      .catch(e => {
        if (e.status === 404) {
          return Promise.reject(`Курс валют за дату ${requestDate} не найден`)
        }

        return Promise.reject(e.message)
      })
  }

  public static async getCurrencyRate(currencyCode: string): Promise<CurrencyRate> {
    const currency: CurrencyRate | null = CurrencyRatesMock.at(-1)?.rates.find(c => c.currencyCode == currencyCode.toUpperCase()) ?? null

    if (currency === null)
      return Promise.reject('Валюта не найдена')

    return await Promise.resolve(currency)
  }
}


export { CurrencyService }

/*
let response = await axiosClassic.get<DayCurrencyRates>('currency-rates?date=' + date)

if(response.status === 404) {
  return await Promise.reject('Отсутствуют данные')
}
return Promise.resolve(response.data)
*/