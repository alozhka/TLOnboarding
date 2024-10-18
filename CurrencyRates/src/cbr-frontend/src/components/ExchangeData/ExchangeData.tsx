import { Card, Stack, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { CurrencyService } from "~/core/currency/CurrencyService"
import { Currency } from "~/core/types"

interface ExchangeDataProps {
  selectedCurrencyCode: string | null
}

const ExchangeData: React.FC<ExchangeDataProps> = (props) => {
  const [currency, setCurrency] = useState<Currency | null>(null)

  useEffect(() => {
    if (props.selectedCurrencyCode) {
      CurrencyService.GetCurrencyChart(props.selectedCurrencyCode)
        .then(r => setCurrency(r))
    }
  }, [])

  return (
    <>
      {currency &&
        <Card>
          <Stack direction='row' spacing={2}>
            <Typography variant="h2">{currency.charCode}/RUB</Typography>
            <div>
              <Typography variant="h6">{currency.name}</Typography>
              <Typography variant="h6">Российский рубль</Typography>
            </div>
          </Stack>
          <Typography variant="h3">{currency.vUnitRate}</Typography>
        </Card>
      }
    </>
  )
}


export default ExchangeData