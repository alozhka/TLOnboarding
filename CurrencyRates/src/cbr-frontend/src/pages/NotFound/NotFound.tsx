import { Link } from "react-router-dom"
import PagesUrls from "~/pages"

const NotFoundPage: React.FC = () => 
    <>Страница не найдена. <Link to={PagesUrls.Main}>На главную</Link></>


export default NotFoundPage