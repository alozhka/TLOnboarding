import { useSearchParams } from "react-router-dom"
import CurrenciesSider from "~/components/CurrenciesSider/CurrenciesSider"

const ShowCurrencyPage: React.FC = () => {
    const [params] = useSearchParams()
    const date = params.get('date')
    const currency = params.get('currency')

    return (
        <>
        <CurrenciesSider date={date}/>
        </>
    )
}


export default ShowCurrencyPage