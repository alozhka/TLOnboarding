import { Container, Stack, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { Link, useSearchParams } from "react-router-dom"
import CurrenciesSider from "~/components/CurrenciesSider/CurrenciesSider"
import ExchangeData from "~/components/ExchangeData/ExchangeData"
import { CurrencyService } from "~/core/currency/CurrencyService"
import { CurrencyRate, DayCurrencyRates } from "~/core/types"
import PagesUrls from "~/pages/consts"

const ShowCurrencyPage: React.FC = () => {
  const [params, setParams] = useSearchParams()
  const date = params.get('date')
  const [rates, setRates] = useState<DayCurrencyRates | null>(null)
  const [currency, setCurrency] = useState<CurrencyRate | null>(null)

  useEffect(() => {
    CurrencyService.getCurrencyRatesByDate(date)
      .then(r => {
        setRates(r)
        const curr = r.rates.find(c => c.currencyCode === params.get('currency')?.toUpperCase())
        setCurrency(curr ?? r.rates[0])
      })
  }, [])

  const onCurrencySelect = (charCode: string): void => {
    params.set('currency', charCode)
    setParams(params)
    setCurrency(rates?.rates.find(c => c.currencyCode == charCode)!)
  }

  return (
    <Container>
      <Link to={PagesUrls.Index()}><Typography variant="h1">Курс валют</Typography></Link>
      <Stack direction='row'>
        <CurrenciesSider date={date} onSelect={onCurrencySelect} />
        {currency && <ExchangeData currency={currency} />}
      </Stack>
    </Container>
  )
}


export default ShowCurrencyPage