using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries;
using PCStore.Application.Services.CouponService;
using PCStore.Application.Services.CouponService.CouponServiceCommands;
using System.Security.Claims;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController(ICouponService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody]CreateCouponCommand request, CancellationToken cancellation = default) 
        {
            var result = await service.CreateCoupon(request,cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("deactive/{id}")]
        public async Task<IActionResult> DeactiveCoupon([FromRoute] int id, CancellationToken cancellation = default) 
        {
            var request = new DeactiveCouponCommand { CouponId = id };
            var result = await service.DeactiveCoupon(request,cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("active/{id}")]
        public async Task<IActionResult> ActiveCoupon([FromRoute] int id, CancellationToken cancellation = default)
        {
            var request = new ActiveCouponCommand { Id = id };
            var result = await service.ActiveCoupon(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/{id}")]
        public async Task<IActionResult> AdminGetCouponById([FromRoute] int id, CancellationToken cancellation = default) 
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var request = new AdminGetCouponByIdQuery { Id = id };
            var result = await service.AdminGetCouponById(request,cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> AdminGetAllCoupons([FromQuery]AdminGetAllCouponsQuery request, CancellationToken cancellation = default) 
        {
            var result = await service.AdminGetAllCoupons(request,cancellation);
            return StatusCode(result.StatusCode,result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoupon([FromBody]ServiceUpdateCouponCommand request, CancellationToken cancellation = default) 
        {
            if(request.CouponInformation.CouponTargetType is not null) 
            {
                switch (request.CouponInformation.CouponTargetType) 
                {
                    case Domain.Enum.CouponTargetType.AllProducts:
                        break;
                    case Domain.Enum.CouponTargetType.ProductTypes:
                        if (request.TypeInformation is null)
                            return BadRequest("Type information is null!");
                        break;
                    case Domain.Enum.CouponTargetType.Categories:
                        if (request.CategoryInformation is null)
                            return BadRequest("Category information is null!");
                        break;
                    case Domain.Enum.CouponTargetType.Brands:
                        if (request.BrandInformation is null)
                            return BadRequest("Brand information is null!");
                        break;
                    case Domain.Enum.CouponTargetType.SpecificProducts:
                        if (request.ProductListInformation is null)
                            return BadRequest("ProductList information is null!");
                        break;
                }
            }
            var result = await service.UpdateCoupon(request, cancellation);
            return StatusCode(result.StatusCode,result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{couponId}/isValid")]
        public async Task<IActionResult> CouponIsValid(int couponId, CancellationToken cancellation = default) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return Forbid();
            var request = new IsCouponValidQuery { CouponId = couponId, UserId = userId };
            var result = await service.CouponIsValid(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("usage")]
        public async Task<IActionResult> GetAllCouponUsages([FromQuery] GetAllCouponUsagesQuery request, CancellationToken cancellation = default)
        {
            var result = await service.GetAllCouponUsages(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }
    }
}
