const CURRENCY_CODE_REGEXP: RegExp = new RegExp('^[a-zA-Z]{3}$')

const isCurrencyCode = (s: string): boolean => CURRENCY_CODE_REGEXP.test(s)


export { isCurrencyCode }