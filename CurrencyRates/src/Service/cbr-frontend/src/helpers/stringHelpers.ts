const CURRENCY_CODE_REGEXP: RegExp = new RegExp('^[a-zA-Z]{3}$')

export function isValidCurrencyCode(s: string): boolean {
    return CURRENCY_CODE_REGEXP.test(s)
}
