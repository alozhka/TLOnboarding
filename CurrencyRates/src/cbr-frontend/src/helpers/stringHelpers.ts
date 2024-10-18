const CURRENCY_CODE_REGEXP: RegExp = new RegExp('^[a-zA-Z]{3}$')

const isValidCurrencyCode = (s: string): boolean => CURRENCY_CODE_REGEXP.test(s)


export { isValidCurrencyCode as isCurrencyCode }