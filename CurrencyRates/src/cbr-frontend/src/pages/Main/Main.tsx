import { Box, Container, TextField, Typography } from "@mui/material"
import { DatePicker } from "@mui/x-date-pickers"
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { ChangeEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import { isCurrencyCode } from "~/helpers/stringHelpers";
import PagesUrls from "~/pages";


const MainPage: React.FC = () => {
  const navigate = useNavigate()
  const [currency, setCurrency] = useState<string>("")
  const [currencyError, setCurrencyError] = useState<boolean>(false)
  
  function handleCurrencyChange(e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>): void {
    setCurrency(e.target.value)
    setCurrencyError(!isCurrencyCode(e.target.value))
  }

  return (
    <Container sx={{ justifyContent: 'center' }}>
      <Typography align="center" variant="h1">
        Курс валют
        </Typography>
      <Box 
        gap={7}
        sx={{ display: "flex", flexDirection: 'row' }}>
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
      </Box>
    </Container>
  )
}


export default MainPage