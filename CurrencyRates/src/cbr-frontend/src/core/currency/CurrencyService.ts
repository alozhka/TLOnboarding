import { Currency, CurrencyRates } from "~/core/types";
import { CurrencyRatesMock } from "../mock";

class CurrencyService {
  public static async GetCurrencies(date: string | null): Promise<CurrencyRates> {
    let currentRate = CurrencyRatesMock.find(r => r.date === date) ?? null

    if (currentRate) {
      return await Promise.resolve(currentRate)
    }

    let lastRate = CurrencyRatesMock.at(-1)
    if (lastRate)
      return await Promise.resolve(lastRate)

    return await Promise.reject('Отсутствуют данные')

  }

  public static async GetCurrencyChart(currencyCode: string): Promise<Currency> {
    let currency: Currency | null = CurrencyRatesMock.at(-1)?.currencies.find(c => c.charCode == currencyCode.toUpperCase()) ?? null

    if (currency === null)
      return Promise.reject('Валюта не найдена')

    return await Promise.resolve(currency)
  }
}


export { CurrencyService }