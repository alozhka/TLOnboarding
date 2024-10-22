class PagesUrls {
    /**
     * Общий префикс для всех страниц
     */
    private static readonly prefixUrl: string = ''

    public static Index(): string {
        return this.prefixUrl + '/'
    }
    public static Search(query: string): string {
        return this.prefixUrl + '/search/?' + query
    }
}


export default PagesUrls