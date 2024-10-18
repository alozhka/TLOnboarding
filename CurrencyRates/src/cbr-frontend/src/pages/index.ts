/**
 * Общий префикс для всех страниц
 */
const prefixUrl = ''

const PagesUrls = {
    Main: prefixUrl + '/',
    Search: (query: string) => prefixUrl + '/search/?' + query
}


export default PagesUrls