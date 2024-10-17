import { CurrencyRates } from "~/core/types";
import { CurrencyRatesMock } from "../mock";

class CurrencyService {
    public static async GetCurrencies(date: string): Promise<CurrencyRates> {
        return await Promise.resolve(CurrencyRatesMock)
    }
}


export { CurrencyService }