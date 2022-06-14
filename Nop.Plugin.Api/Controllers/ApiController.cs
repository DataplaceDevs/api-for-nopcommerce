using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Plugin.Api.Controllers
{
    public class ApiController : BaseApiController
    {
        public readonly IPluginService _pluginService;

        public ApiController(IJsonFieldsSerializer jsonFieldsSerializer,
                             IAclService aclService,
                             ICustomerService customerService,
                             IStoreMappingService storeMappingService,
                             IStoreService storeService,
                             IDiscountService discountService,
                             ICustomerActivityService customerActivityService,
                             ILocalizationService localizationService,
                             IPictureService pictureService,
                             IPluginService pluginService) : base(jsonFieldsSerializer, aclService, customerService,
                                                                    storeMappingService, storeService, discountService,
                                                                    customerActivityService, localizationService,
                                                                    pictureService)
        {
            _pluginService = pluginService;
        }

        /// <summary>
        ///     Retrieve all languages
        /// </summary>
        /// <param name="fields">Fields from the language you want your json to contain</param>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("/api/version", Name = "GetApiVersion")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPluginVersion()
        {
            var pluginDescriptor = await _pluginService.GetPluginDescriptorBySystemNameAsync<IPlugin>("Nop.Plugin.Api");
            if (pluginDescriptor is null)
                return NotFound();
            
            return Ok(pluginDescriptor.Version);
        }

    }
}
