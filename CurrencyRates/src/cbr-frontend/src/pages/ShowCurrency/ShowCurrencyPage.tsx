import { Container, Stack, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { Link, useSearchParams } from "react-router-dom"
import CurrenciesSider from "~/components/CurrenciesSider/CurrenciesSider"
import ExchangeData from "~/components/ExchangeData/ExchangeData"
import { CurrencyService } from "~/core/currency/CurrencyService"
import { Currency, CurrencyRates } from "~/core/types"
import PagesUrls from "~/pages"

const ShowCurrencyPage: React.FC = () => {
  const [params, setParams] = useSearchParams()
  const date = params.get('date')
  const [rates, setRates] = useState<CurrencyRates | null>(null)
  const [currency, setCurrency] = useState<Currency | null>(null)

  useEffect(() => {
    CurrencyService.GetCurrencies(date)
      .then(r => {
        setRates(r)
        const curr = r.currencies.find(c => c.charCode === params.get('currency'))
        if (curr)
          setCurrency(curr)
      })
  }, [])

  const onCurrencySelect = (charCode: string): void => {
    params.set('currency', charCode)
    setParams(params)
    setCurrency(rates?.currencies.find(c => c.charCode == charCode)!)
  }

  return (
    <Container>
      <Link to={PagesUrls.Main}><Typography variant="h1">Курс валют</Typography></Link>
      <Stack direction='row'>
        <CurrenciesSider date={date} onSelect={onCurrencySelect} />
        {currency && <ExchangeData currency={currency} />}
      </Stack>
    </Container>
  )
}


export default ShowCurrencyPage