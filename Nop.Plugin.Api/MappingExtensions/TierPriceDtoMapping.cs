using Nop.Core.Domain.Catalog;
using Nop.Plugin.Api.AutoMapper;
using Nop.Plugin.Api.DTO.Products;

namespace Nop.Plugin.Api.MappingExtensions
{
    public static class TierPriceDtoMapping
    {
        public static TierPriceDto ToDto(this TierPrice tierPrice)
        {
            return tierPrice.MapTo<TierPrice, TierPriceDto>();
        }

        public static TierPrice ToEntity(this TierPriceDto tierPriceDto)
        {
            return tierPriceDto.MapTo<TierPriceDto, TierPrice>();
        }
    }
}
