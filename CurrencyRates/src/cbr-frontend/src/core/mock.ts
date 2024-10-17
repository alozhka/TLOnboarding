import { CurrencyRates } from "./types"

const CurrencyRatesMock: CurrencyRates = {
    date: '2024-10-08',
    currencies: [
        {
            charCode: 'EUR',
            name: 'Евро',
            vUnitRate: 105.3069
        },
        {
            charCode: 'USD',
            name: 'Доллар США',
            vUnitRate: 96.0649
        },
        {
            charCode: 'GPB',
            name: 'Фунт стерлингов Соединенного королевства',
            vUnitRate: 125.9699
        },
        {
            charCode: 'BGN',
            name: 'Болгарский лев',
            vUnitRate: 54.1714
        },
        {
            charCode: 'AED',
            name: 'Дирхам ОАЭ',
            vUnitRate: 26.1579
        },
        {
            charCode: 'INR',
            name: 'Индийских рупий',
            vUnitRate: 1.1440
        },
        {
            charCode: 'CNY',
            name: 'Китайский юань',
            vUnitRate: 13.5537
        },
        {
            charCode: 'HKD',
            name: 'Гонконгский доллар',
            vUnitRate: 12.3907
        },
    ]
}


export { CurrencyRatesMock }