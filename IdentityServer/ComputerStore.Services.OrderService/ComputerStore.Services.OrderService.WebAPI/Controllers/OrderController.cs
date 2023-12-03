using ComputerStore.Services.OrderService.WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Services.OrderService.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route(ApiConstants.Orders)]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        [HttpGet(ApiConstants.Id)]
        public async Task<ProductDto> GetProductAsync(Guid id)
        {
            var model = await _productsService.GetProductAsync(id);
            return _mapper.Map<ProductDto>(model);
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProductsAsync([FromQuery] ProductSearchDto searchDto)
        {
            var models = await _productsService.GetProductsAsync(searchDto.Title);
            return _mapper.Map<IEnumerable<ProductDto>>(models);
        }

        [HttpPost]
        public async Task<ProductDto> CreateProductAsync(ProductDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);

            var model = await _productsService.CreateProductAsync(_mapper.Map<ProductModel>(dto));
            return _mapper.Map<ProductDto>(model);
        }

        [HttpPut(ApiConstants.Id)]
        public async Task<ProductDto> UpdateProductAsync(Guid id, ProductDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);

            var model = await _productsService.UpdateProductAsync(id, _mapper.Map<ProductModel>(dto));
            return _mapper.Map<ProductDto>(model);
        }

        [HttpDelete(ApiConstants.Id)]
        public async Task DeleteProductAsync(Guid id)
        {
            await _productsService.DeleteProductAsync(id);
        }
    }
}

