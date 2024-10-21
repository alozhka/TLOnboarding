using Cbr.Application.Abstractions;
using Cbr.Application.Errors;
using Cbr.Infrastructure.Database.Repository;

namespace Cbr.Application.UseCases.CurrencyRates.GetDay;

public class GetDayCurrencyRatesCommandHandler(CurrencyRatesRepository currencyRatesRepository)
{
    private readonly CurrencyRatesRepository _currencyRatesRepository = currencyRatesRepository;

    public async Task<Result<Domain.Entity.CurrencyRates>> Handle(GetDayCurrencyRatesCommand command, CancellationToken ct)
    {
        Domain.Entity.CurrencyRates? rates = await _currencyRatesRepository.GetByDate(command.Date, ct);

        if (rates is null)
        {
            return CurrencyRatesErrors._notFound;
        }

        return rates;
    }
}
