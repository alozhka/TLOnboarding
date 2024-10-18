export type CurrencyRate = {
    currencyName: string,
    currencyCode: string,
    exchangeRate: number
}

export type DayCurrencyRates = {
    date: string,
    rates: CurrencyRate[]
}
