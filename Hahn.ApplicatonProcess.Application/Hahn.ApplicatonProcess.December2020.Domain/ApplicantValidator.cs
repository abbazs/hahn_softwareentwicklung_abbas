using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public class ApplicantValidator : AbstractValidator<IApplicant>
    {
        public static Uri BaseUri = new Uri(@"https://restcountries.eu/rest/v2/name/");
        public ApplicantValidator()
        {
            RuleFor(x => x.Name).MinimumLength(5);
            RuleFor(x => x.FamilyName).MinimumLength(5);
            RuleFor(x => x.Address).MinimumLength(10);
            RuleFor(x => x.EMailAddress).EmailAddress();
            RuleFor(x => x.Age).GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
            RuleFor(x => x.Hired).NotNull();
            RuleFor(x => x.CountryOfOrigin).MustAsync((x, cancellation) => ValidCountry(x)).WithMessage("Please specify a valid country.");
        }

        private async Task<bool> ValidCountry(string country)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = BaseUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"{country}?fullText=true");
            return response.IsSuccessStatusCode;
        }
    }
}
