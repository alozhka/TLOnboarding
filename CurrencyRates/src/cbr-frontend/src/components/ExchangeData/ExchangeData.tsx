import { Card, Stack, Typography } from "@mui/material"
import { Currency } from "~/core/types"

interface ExchangeDataProps {
  currency: Currency
}

const ExchangeData: React.FC<ExchangeDataProps> = ({ currency }) => {
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