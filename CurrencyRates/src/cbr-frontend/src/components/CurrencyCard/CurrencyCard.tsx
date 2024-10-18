import { Button, Divider } from "@mui/material"
import { CurrencyRate } from "~/core/types"

interface CurrencyCardProps {
  currency: CurrencyRate
}

const CurrencyCard: React.FC<CurrencyCardProps> = (props) => {
  return (
    <Button sx={{ border: '1px black solid', p: '1px', width: '100%' }}>
      <h5>{props.currency.currencyCode}/RUB</h5>
      <Divider>
        <strong>{props.currency.exchangeRate}</strong>
      </Divider>
    </Button>
  )
}


export default CurrencyCard