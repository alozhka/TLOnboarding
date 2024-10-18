import { Box, Card, List, ListItem, Stack } from "@mui/material"
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
          <ListItem onClick={() => { props.onSelect && props.onSelect(c.currencyCode) }}>
            <CurrencyCard key={c.currencyCode} currency={c} />
          </ListItem>)
        }
      </List>
    </Card>
  )
}


export default CurrenciesSider