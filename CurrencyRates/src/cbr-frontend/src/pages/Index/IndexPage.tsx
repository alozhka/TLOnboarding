import { Container, Stack, TextField, Typography } from "@mui/material"
import { DatePicker } from "@mui/x-date-pickers"
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { ChangeEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import { isValidCurrencyCode } from "~/helpers/stringHelpers";
import PagesUrls from "~/pages";


const IndexPage: React.FC = () => {
  const navigate = useNavigate()
  const [currency, setCurrency] = useState<string>("")
  const [currencyError, setCurrencyError] = useState<boolean>(false)
  
  const handleCurrencyChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>): void => {
    setCurrency(e.target.value)
    setCurrencyError(!isValidCurrencyCode(e.target.value))
  }

  return (
    <Container sx={{ justifyContent: 'center', width: '100dvw' }}>
      <Typography align="center" variant="h2">Курс валют</Typography>
      <Stack direction="row" spacing={2} alignItems='center'>
        <p>Найдите нужный курс по дате:</p>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DatePicker 
            onAccept={(value) => navigate(PagesUrls.Search(`date=${value!.format('YYYY-MM-DD')}`))}
            />
        </LocalizationProvider>
        <p>или по коду валюты:</p>
        <TextField 
          value={currency}
          error={currencyError}
          onChange={handleCurrencyChange}
          onKeyDown={(e) => (e.key == 'Enter' && !currencyError) && navigate(PagesUrls.Search(`currency=${currency}`))}
          helperText="Код валюты должен быть из 3 символов"/>
      </Stack>
    </Container>
  )
}


export default IndexPage