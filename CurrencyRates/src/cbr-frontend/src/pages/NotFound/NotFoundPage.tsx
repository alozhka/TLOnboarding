import { Link } from "react-router-dom"
import PagesUrls from "~/pages/consts"

const NotFoundPage: React.FC = () => 
    <>Страница не найдена. <Link to={PagesUrls.Index()}>На главную</Link></>


export default NotFoundPage