import { Card, Stack, Typography } from "@mui/material"
import { CurrencyRate } from "~/core/types"

interface ExchangeDataProps {
  currency: CurrencyRate
}

const ExchangeData: React.FC<ExchangeDataProps> = ({ currency }) => {
  return (
    <Card>
      <Stack direction='row' spacing={2}>
        <Typography variant="h2">{currency.currencyCode}/RUB</Typography>
        <div>
          <Typography variant="h6">{currency.currencyName}</Typography>
          <Typography variant="h6">Российский рубль</Typography>
        </div>
      </Stack>
      <Typography variant="h3">{currency.exchangeRate}</Typography>
    </Card>
  )
}


export default ExchangeData