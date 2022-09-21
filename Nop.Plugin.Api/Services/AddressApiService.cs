using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Data;
using Nop.Plugin.Api.DTO;
using Nop.Plugin.Api.MappingExtensions;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;

using Nop.Plugin.Api.Helpers;
using static Nop.Plugin.Api.Services.CustomerDocumentsEnums;
using Nop.Core;

namespace Nop.Plugin.Api.Services
{
	public class AddressApiService : IAddressApiService
	{
        private readonly IStaticCacheManager _cacheManager;
		private readonly ICountryService _countryService;        
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<CustomerAddressMapping> _customerAddressMappingRepository;

        private readonly IGenericAttributeService _genericAttributeService;        
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerDocumentsApiService _customerDocumentsApiService;

        public AddressApiService(
            IRepository<Address> addressRepository,
            IRepository<CustomerAddressMapping> customerAddressMappingRepository,
            IStaticCacheManager staticCacheManager,
            ICountryService countryService,
            IGenericAttributeService genericAttributeService,
            ICustomerAttributeService customerAttributeService,
            ICustomerDocumentsApiService customerDocumentsApiService
            )
        {
            _addressRepository = addressRepository;
            _customerAddressMappingRepository = customerAddressMappingRepository;
            _cacheManager = staticCacheManager;
			_countryService = countryService;
            _genericAttributeService = genericAttributeService;
            _customerAttributeService = customerAttributeService;
            _customerDocumentsApiService = customerDocumentsApiService;
        }

        /// <summary>
        /// Gets a list of addresses mapped to customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public async Task<IList<AddressDto>> GetAddressesByCustomerIdAsync(int customerId)
        {
            var query = from address in _addressRepository.Table
                        join cam in _customerAddressMappingRepository.Table on address.Id equals cam.AddressId
                        where cam.CustomerId == customerId
                        select address;

            var key = _cacheManager.PrepareKeyForShortTermCache(NopCustomerServicesDefaults.CustomerAddressesCacheKey, customerId);
            var addresses = await _cacheManager.GetAsync(key, async () => await query.ToListAsync());

            //OS 233245
            var addressesDTO = addresses.Select(a => a.ToDto()).ToList();
            addressesDTO.ForEach(x => x.CountryName = _countryService.GetCountryByIdAsync(x.CountryId ?? 0).Result.TwoLetterIsoCode);

            return addressesDTO;
        }

		/// <summary>
		/// Gets a address mapped to customer
		/// </summary>
		/// <param name="customerId">Customer identifier</param>
		/// <param name="addressId">Address identifier</param>
		/// <returns>
		/// A task that represents the asynchronous operation
		/// The task result contains the result
		/// </returns>
		public async Task<AddressDto> GetCustomerAddressAsync(int customerId, int addressId)
        {
            var query = from address in _addressRepository.Table
                        join cam in _customerAddressMappingRepository.Table on address.Id equals cam.AddressId                        
                        where cam.CustomerId == customerId && address.Id == addressId
                        select address;

            var key = _cacheManager.PrepareKeyForShortTermCache(NopCustomerServicesDefaults.CustomerAddressCacheKey, customerId, addressId);

            var addressEntity = await _cacheManager.GetAsync(key, async () => await query.FirstOrDefaultAsync());

            var addressDTO = addressEntity?.ToDto();
            addressDTO.CountryName = _countryService.GetCountryByIdAsync(addressDTO.CountryId ?? 0).Result.TwoLetterIsoCode;
            addressDTO.CustomAttributes = _genericAttributeService.GetAttributesForEntityAsync(customerId, "Customer").Result.FirstOrDefault(x => x.Key == "CustomCustomerAttributes").Value;
            addressDTO = await PrepareSpecificAttributeValuesAsync(addressDTO.CustomAttributes, addressDTO);
            
            
            //addressDTO = await PrepareSpecificAttributeValuesAsync(addressDTO.CustomAttributes, addressDTO);
            //var a = _genericAttributeService.GetAttributesForEntityAsync(customerId, "Customer").Result.FirstOrDefault(x => x.Key== "CustomCustomerAttributes");
            //var b = _customerAttributeParser.ParseValues(a.Value, customerId);
            //var b = _genericAttributeService.GetAttributeAsync<string>(addressEntity, "CustomCustomerAttributes");
            //var selectedProvider = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.SelectedMultiFactorAuthenticationProviderAttribute);
            //_genericAttributeService

            return addressDTO;
        }

		public async Task<IList<CountryDto>> GetAllCountriesAsync(bool mustAllowBilling = false, bool mustAllowShipping = false)
		{
            IEnumerable<Country> countries = await _countryService.GetAllCountriesAsync();
            if (mustAllowBilling)
                countries = countries.Where(c => c.AllowsBilling);
            if (mustAllowShipping)
                countries = countries.Where(c => c.AllowsShipping);
            return countries.Select(c => c.ToDto()).ToList();
        }

		public async Task<AddressDto> GetAddressByIdAsync(int addressId)
		{
            var query = from address in _addressRepository.Table
                        where address.Id == addressId
                        select address;
            var addressEntity = await query.FirstOrDefaultAsync();
            return addressEntity?.ToDto();
        }
               
        public async Task<AddressDto> PrepareSpecificAttributeValuesAsync(string attributesXml, AddressDto addressDTO)
        {
            
            if (string.IsNullOrEmpty(attributesXml))
                return addressDTO;
 
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(attributesXml);

            var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/CustomerAttribute");
            foreach (XmlNode node1 in nodeList1)
            {
                if (node1.Attributes?["ID"] == null)
                    continue;

                var str1 = node1.Attributes["ID"].InnerText.Trim();

                if (!int.TryParse(str1, out var id))
                    continue;

                var nodeList2 = node1.SelectNodes(@"CustomerAttributeValue/Value");

                var customerAttributeName = (await _customerAttributeService.GetCustomerAttributeByIdAsync(id))?.Name.Trim().ToUpper();

                if (customerAttributeName != null)
                {
                    var customerDocumentType = _customerDocumentsApiService.GetCustomerDocumentTypeAsync().Result;
                    if (customerDocumentType[ETypeDocument.InscriFed.GetEnumValue()] != null)
                    {
                        if (customerAttributeName.Equals(customerDocumentType[ETypeDocument.InscriFed.GetEnumValue()], StringComparison.InvariantCultureIgnoreCase))
                            addressDTO.InscriFed = nodeList2[0].InnerText.Trim();
                    }
                    else
                    if (customerDocumentType[ETypeDocument.InscriEst.GetEnumValue()] != null)
                    {
                        if (customerAttributeName.Equals(customerDocumentType[CustomerDocumentsEnums.ETypeDocument.InscriEst.GetEnumValue()], StringComparison.InvariantCultureIgnoreCase))
                            addressDTO.InscriEst = nodeList2[0].InnerText.Trim();
                    }
                    //if (customerAttributeName.Equals(ECustomerDocuments.CPF_CNPJ.GetEnumDescription(), StringComparison.InvariantCultureIgnoreCase))
                    //    addressDTO.InscriFed = nodeList2[0].InnerText.Trim();

                    //else if (customerAttributeName.Equals(ECustomerDocuments.RG_IE.GetEnumDescription(), StringComparison.InvariantCultureIgnoreCase))
                    //    addressDTO.InscriEst = nodeList2[0].InnerText.Trim();
                }
            }
            return addressDTO;
        }

        

    }
}
