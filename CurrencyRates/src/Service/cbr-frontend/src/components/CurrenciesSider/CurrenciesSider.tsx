import { Card, List, ListItemButton } from "@mui/material"
import { DayCurrencyRates } from "~/core/types"
import CurrencyCard from "../CurrencyCard/CurrencyCard"


interface CurrenciesSiderProps {
  dayRates: DayCurrencyRates | null
  onSelect?: (charCode: string) => void
}

const CurrenciesSider: React.FC<CurrenciesSiderProps> = (props) => {
  return (
    <Card>
      <List>
        {props.dayRates !== null && props.dayRates.rates.map(c =>
          <ListItemButton onClick={() => { props.onSelect && props.onSelect(c.currencyCode) }}>
            <CurrencyCard key={c.currencyCode} currency={c} />
          </ListItemButton>)
        }
      </List>
    </Card>
  )
}


export default CurrenciesSider