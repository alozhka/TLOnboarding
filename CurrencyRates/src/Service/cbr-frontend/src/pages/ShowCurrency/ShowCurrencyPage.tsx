import { Container, Stack, Typography } from "@mui/material"
import { Link } from "react-router-dom"
import CurrenciesSider from "~/components/CurrenciesSider/CurrenciesSider"
import ExchangeData from "~/components/ExchangeData/ExchangeData"
import PagesUrls from "~/pages/consts"
import { useDayCurrencyRatesAndSelect } from "./requestsHooks"

const ShowCurrencyPage: React.FC = () => {
  const {error, rates, currency, onCurrencySelect} = useDayCurrencyRatesAndSelect()

  return (
    <Container>
      <Link to={PagesUrls.Index()}><Typography variant="h2">Курс валют</Typography></Link>
      {error !== "" ?
        <p>Произошла ошибка: {error}</p>
      :
        <Stack direction='row' spacing={2}>
          <CurrenciesSider dayRates={rates} onSelect={onCurrencySelect} />
          {currency && <ExchangeData currency={currency} />}
        </Stack>
      }
    </Container>
  )
}


export default ShowCurrencyPage