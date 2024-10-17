type Currency = {
    name: string,
    charCode: string,
    vUnitRate: number
}

type CurrencyRates = {
    date: string,
    currencies: Currency[]
}


export type { Currency, CurrencyRates }