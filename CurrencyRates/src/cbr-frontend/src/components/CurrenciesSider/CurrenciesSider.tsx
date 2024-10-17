import { Box, Stack } from "@mui/material"
import { useEffect, useState } from "react"
import { CurrencyService } from "~/core/currency/CurrencyService"
import { CurrencyRates } from "~/core/types"
import CurrencyCard from "../CurrencyCard/CurrencyCard"


interface CurrenciesSiderProps {
    date: string | null,
    onSelect?: (charCode: string) => void
}

const CurrenciesSider: React.FC<CurrenciesSiderProps> = (props) => {
    const [currencies, setCurrencies] = useState<CurrencyRates | null>(null)

    useEffect(() => {
        if (props.date) {
            CurrencyService.GetCurrencies(props.date)
                .then(r => setCurrencies(r))
        }
    })

    return (
        <Box sx={{ border: '2px black solid', width: '15dvw', height: '60dvh', overflow: 'scroll', p: 2 }}>
            <Stack spacing={10}>
                {currencies !== null &&
                    currencies.currencies.map(c => <CurrencyCard currency={c} />)
                }
            </Stack>
        </Box>
    )
}


export default CurrenciesSider