import { Divider } from "@mui/material"
import { CurrencyRate } from "~/core/types"

interface CurrencyCardProps {
  currency: CurrencyRate
}

const CurrencyCard: React.FC<CurrencyCardProps> = (props) => {
  return (
    <>
      <h5>{props.currency.currencyCode}/RUB</h5>
      <Divider>
        <strong>{props.currency.exchangeRate}</strong>
      </Divider>
    </>
  )
}


export default CurrencyCard