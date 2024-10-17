import { Card } from "@mui/material"
import { Currency } from "~/core/types"

interface CurrencyCardProps {
  currency: Currency
}

const CurrencyCard: React.FC<CurrencyCardProps> = (props) => {
  return (
    <Card sx={{ cursor: 'pointer', ":hover": { background: 'lightgray' }, ":active": {background: 'grey'} }}>
      <h5>{props.currency.charCode}/RUB</h5>
      <strong>{props.currency.vUnitRate}</strong>
    </Card>
  )
}


export default CurrencyCard