using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.DTO.CustomerAttributes;
using Nop.Plugin.Api.DTO.Errors;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models.CustomerAttributesParameters;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Plugin.Api.Controllers
{
    public class CustomerAttributesController : BaseApiController
    {
        private readonly IDTOHelper _dtoHelper;
        private readonly ICustomerAttributeService _customerAttributeService;

        public CustomerAttributesController(
            IJsonFieldsSerializer jsonFieldsSerializer,
            IAclService aclService,
            ICustomerService customerService,
            IStoreMappingService storeMappingService,
            IStoreService storeService,
            IDiscountService discountService,
            ICustomerActivityService customerActivityService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            ICustomerAttributeService customerAttributeService,
            IDTOHelper dtoHelper
            ) : base(jsonFieldsSerializer, aclService, customerService, storeMappingService, storeService, discountService, customerActivityService, localizationService, pictureService)
        {
            _customerAttributeService = customerAttributeService;
            _dtoHelper = dtoHelper;
        }

        /// <summary>
        ///     Receive data from customer attributes
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("/api/customer_attributes", Name = "GetCustomerAttributes")]
        [ProducesResponseType(typeof(CustomerAttributesRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [GetRequestsErrorInterceptorActionFilter]
        public async Task<IActionResult> GetCustomerAttributes([FromQuery] CustomerAttributesParametersModel parameters)
        {
            if (parameters.Name == string.Empty)
            {
                return Error(HttpStatusCode.BadRequest, "name", "invalid attribute name");
            }

            var attributes = await _customerAttributeService.GetAllCustomerAttributesAsync();
            var customerAttribute = attributes.FirstOrDefault(x => x.Name.Equals(parameters.Name.Trim(), StringComparison.InvariantCultureIgnoreCase) == true);
            if (customerAttribute == null)
            {
                return Error(HttpStatusCode.NotFound, "customer_attributes", "not found");
            }

            var customerAttributesRootObject = new CustomerAttributesRootObject();
            var customerAttributeDto = await _dtoHelper.PrepareCustomerAttributesDtoAsync(customerAttribute);
            customerAttributesRootObject.CustomerAttributes = customerAttributeDto;
            var json = JsonFieldsSerializer.Serialize(customerAttributesRootObject, string.Empty);
            return new RawJsonActionResult(json);
        }
    }
}
