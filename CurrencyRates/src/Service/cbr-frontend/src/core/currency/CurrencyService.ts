import { CurrencyRate, DayCurrencyRates } from "~/core/types";
import { CurrencyRatesMock } from "../mock";

class CurrencyService {
  public static async getCurrencyRatesByDate(date: string | null): Promise<DayCurrencyRates> {
    const currentRate = CurrencyRatesMock.find(r => r.date === date) ?? null

    if (currentRate) {
      return await Promise.resolve(currentRate)
    }

    const lastRate = CurrencyRatesMock.at(-1)
    if (lastRate)
      return await Promise.resolve(lastRate)

    return await Promise.reject('Отсутствуют данные')

  }

  public static async getCurrencyRate(currencyCode: string): Promise<CurrencyRate> {
    const currency: CurrencyRate | null = CurrencyRatesMock.at(-1)?.rates.find(c => c.currencyCode == currencyCode.toUpperCase()) ?? null

    if (currency === null)
      return Promise.reject('Валюта не найдена')

    return await Promise.resolve(currency)
  }
}


export { CurrencyService }