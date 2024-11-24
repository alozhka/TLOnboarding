import { Card, Paper, Typography } from "@mui/material"
import { CurrencyRate } from "~/core/types"

interface ExchangeDataProps {
  currency: CurrencyRate
}

const ExchangeData: React.FC<ExchangeDataProps> = ({ currency }) => {
  return (
    <Card sx={{ p: '20px' }}>
      <Typography variant="h3">1 {currency.currencyCode} = {currency.exchangeRate} RUB</Typography>
      <Paper sx={{p: '10px'}}>
        <p>Источник: ЦБ РФ</p>
        <p>1 {currency.currencyName} = {currency.exchangeRate} Российского рубля</p>
      </Paper>
    </Card>
  )
}


export default ExchangeData