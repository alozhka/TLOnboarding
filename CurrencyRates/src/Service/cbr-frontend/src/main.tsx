import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import IndexPage from '~/pages/Index/IndexPage'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import NotFoundPage from '~/pages/NotFound/NotFoundPage'
import ShowCurrencyPage from '~/pages/ShowCurrency/ShowCurrencyPage'

const router = createBrowserRouter([
  {
    path: "/",
    element: <IndexPage />
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
