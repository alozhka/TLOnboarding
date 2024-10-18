import { Button } from "@mui/material"
import { Currency } from "~/core/types"

interface CurrencyCardProps {
  currency: Currency,
  onCLick?: (charCode: string) => void
}

const CurrencyCard: React.FC<CurrencyCardProps> = (props) => {
  const onClick = () => {
    if (props.onCLick) {
      props.onCLick(props.currency.charCode)
    }
  }

  return (
    <Button onClick={onClick} sx={{ border: '1px black solid' }}>
      <h5>{props.currency.charCode}/RUB</h5>
      <strong>{props.currency.vUnitRate}</strong>
    </Button>
  )
}


export default CurrencyCard