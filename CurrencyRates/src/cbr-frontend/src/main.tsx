import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import MainPage from '~/pages/Main/Main'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import NotFoundPage from '~/pages/NotFound/NotFound'
import ShowCurrencyPage from '~/pages/ShowCurrency/ShowCurrencyPage'

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />
  },
  {
    path: "/search",
    element: <ShowCurrencyPage />
  },
  {
    path: "*",
    element: <NotFoundPage />
  }
])

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
