import { Container, Stack, Typography } from "@mui/material"
import { useState } from "react"
import { Link, useSearchParams } from "react-router-dom"
import CurrenciesSider from "~/components/CurrenciesSider/CurrenciesSider"
import ExchangeData from "~/components/ExchangeData/ExchangeData"
import PagesUrls from "~/pages"

const ShowCurrencyPage: React.FC = () => {
    const [params, setParams] = useSearchParams()
    const date = params.get('date')
    const [currency, setCurrency] = useState<string | null>(params.get('currency'))

    const onCurrencySelect = (charCode: string): void => {
        params.set('currency', charCode)
        setParams(params)
        setCurrency(params.get('currency'))
    }

    return (
        <Container>
            <Link to={PagesUrls.Main}><Typography variant="h1">Курс валют</Typography></Link>
            <Stack direction='row'>
                <CurrenciesSider date={date} onSelect={onCurrencySelect} />
                <ExchangeData selectedCurrencyCode={currency} />
            </Stack>
        </Container>
    )
}


export default ShowCurrencyPage