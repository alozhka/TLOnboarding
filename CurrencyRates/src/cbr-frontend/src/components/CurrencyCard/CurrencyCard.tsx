import { Button } from "@mui/material"
import { CurrencyRate } from "~/core/types"

interface CurrencyCardProps {
  currency: CurrencyRate,
  onCLick?: (charCode: string) => void
}

const CurrencyCard: React.FC<CurrencyCardProps> = (props) => {
  const onClick = () => {
    if (props.onCLick) {
      props.onCLick(props.currency.currencyCode)
    }
  }

  return (
    <Button onClick={onClick} sx={{ border: '1px black solid', p: '1px' }}>
      <h5>{props.currency.currencyCode}/RUB</h5>
      <strong>{props.currency.exchangeRate}</strong>
    </Button>
  )
}


export default CurrencyCard