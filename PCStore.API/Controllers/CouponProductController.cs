using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using PCStore.Application.Services.CouponProductService;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponProductController : ControllerBase
    {
    }
}
