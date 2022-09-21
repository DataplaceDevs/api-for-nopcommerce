using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Stores;
using Nop.Plugin.Api.DTO;
using Nop.Plugin.Api.DTO.Stores;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.MappingExtensions;
using Nop.Services.Authentication;
using Nop.Services.Directory;
using Nop.Services.Localization;

namespace Nop.Plugin.Api.Services
{
    public class CustomerDocumentsApiService : ICustomerDocumentsApiService
    {
        private readonly IStoreContext _storeContext;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ICurrencyService _currencyService;

        public CustomerDocumentsApiService(
            IStoreContext storeContext,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ICurrencyService currencyService)
        {
            _storeContext = storeContext;
            _languageService = languageService;
            _localizationService = localizationService;
            _currencyService = currencyService;
        }

        public async Task<IList<string>> GetCustomerDocumentTypeAsync()
        {
            IList<string> federalCustomerDocumentType = new List<string>();

            var store = _storeContext.GetCurrentStore();

            if (store == null)
            {
                return await Task.FromResult(new List<string> { null, null });
            }

            var storeDto = await PrepareStoreDTOAsync(store);

            var defaultLanguage = storeDto.DefaultLanguageId;
            var result = storeDto.Languages.Find(x => x.Id == defaultLanguage);
            if (result != null)
            {
                switch (result.UniqueSeoCode.Trim().ToUpper())
                {
                    case "PT":
                        federalCustomerDocumentType.Add(CustomerDocuments.CPFCNPJ_ATTRIBUTE_NAME);
                        federalCustomerDocumentType.Add(CustomerDocuments.RGIE_ATTRIBUTE_NAME);
                        break;
                    case "ES":
                        federalCustomerDocumentType.Add(CustomerDocuments.RFC_ATTRIBUTE_NAME);
                        federalCustomerDocumentType.Add(null);
                        break;
                    default:
                        federalCustomerDocumentType.Add(CustomerDocuments.CPFCNPJ_ATTRIBUTE_NAME);
                        federalCustomerDocumentType.Add(CustomerDocuments.RGIE_ATTRIBUTE_NAME);
                        break;
                }
                return await Task.FromResult(federalCustomerDocumentType);
            }
            return await Task.FromResult(new List<string> { null, null });
        }

        private async Task<StoreDto> PrepareStoreDTOAsync(Store store)
        {
            var storeDto = store.ToDto();
            storeDto.Languages = (await _languageService.GetAllLanguagesAsync(storeId: store.Id)).Select(x => x.ToDto()).ToList();
            return storeDto;
        }
    }
}
