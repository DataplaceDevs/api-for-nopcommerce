using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.DTO.Errors;
using Nop.Plugin.Api.DTO.StatesProvinces;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models.StatesProvincesParameters;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Plugin.Api.Controllers
{
    public class StatesProvincesController : BaseApiController
    {
        private readonly IDTOHelper _dtoHelper;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        public StatesProvincesController(
            IJsonFieldsSerializer jsonFieldsSerializer,
            IAclService aclService,
            ICustomerService customerService,
            IStoreMappingService storeMappingService,
            IStoreService storeService,
            IDiscountService discountService,
            ICustomerActivityService customerActivityService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            IDTOHelper dtoHelper,
            ICountryService countryService,
            IStateProvinceService stateProvinceService
            ) : base(jsonFieldsSerializer, aclService, customerService, storeMappingService, storeService, discountService, customerActivityService, localizationService, pictureService)
        {
            _stateProvinceService = stateProvinceService;
            _dtoHelper = dtoHelper;
            _countryService = countryService;
        }

        /// <summary>
        ///     Receive a list of all Orders
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("/api/states_provinces", Name = "GetStatesProvinces")]
        [ProducesResponseType(typeof(StateProvinceRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [GetRequestsErrorInterceptorActionFilter]
        public async Task<IActionResult> GetStatesProvinces([FromQuery] StateProvinceParametersModel parameters)
        {
            if (parameters.Abbreviation == string.Empty)
            {
                return Error(HttpStatusCode.BadRequest, "abbreviation", "invalid abbreviation code");
            }
            if (parameters.Country == string.Empty)
            {
                return Error(HttpStatusCode.BadRequest, "country", "invalid country code");
            }

            var country = await _countryService.GetCountryByTwoLetterIsoCodeAsync(parameters.Country);
            if (country == null)
            {
                return Error(HttpStatusCode.NotFound, "country", "not found");
            }

            var stateProvince = await _stateProvinceService.GetStateProvinceByAbbreviationAsync(parameters.Abbreviation, country.Id);
            if (stateProvince == null)
            {
                return Error(HttpStatusCode.NotFound, "stateProvince", "not found");
            }

            var statesProvincesRootObject = new StateProvinceRootObject();
            var stateProvinceDto = await _dtoHelper.PrepareStateProvinceDtoAsync(stateProvince);
            statesProvincesRootObject.StateProvince = stateProvinceDto;
            var json = JsonFieldsSerializer.Serialize(statesProvincesRootObject, string.Empty);
            return new RawJsonActionResult(json);
        }
    }
}
