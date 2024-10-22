import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { CurrencyService } from "~/core/currency/CurrencyService";
import { CurrencyRate, DayCurrencyRates } from "~/core/types";

export function useDayCurrencyRatesAndSelect() {
  const [params, setParams] = useSearchParams()
  const date = params.get('date')
  const [rates, setRates] = useState<DayCurrencyRates | null>(null)
  const [currency, setCurrency] = useState<CurrencyRate | null>(null)
  const [error, setError] = useState<string>('')

  useEffect(() => {
    CurrencyService.getCurrencyRatesByDate(date)
      .then(r => {
        setRates(r)
        const curr = r.rates.find(c => c.currencyCode === params.get('currency')?.toUpperCase())
        setCurrency(curr ?? r.rates[0])
      })
      .catch(e => {
        setError(e)
      })
  }, [])

  const onCurrencySelect = (charCode: string): void => {
    params.set('currency', charCode)
    setParams(params)
    setCurrency(rates?.rates.find(c => c.currencyCode == charCode)!)
  }

  return {error, rates, currency, onCurrencySelect}
}
