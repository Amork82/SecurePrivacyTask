using Microsoft.AspNetCore.Mvc;
using SecurePrivacyTask.Server.Dto.Output;
using SecurePrivacyTask.Server.Services;

namespace SecurePrivacyTask.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private readonly BaseService _baseService;         
        public BaseController(IServiceProvider serviceProvider)
        {
            _baseService = serviceProvider.GetRequiredService<BaseService>();   
        }

        [HttpGet]
        public async Task<ActionResult<BinaryResult>> VerifyBinaryString(string binaryValue)
        {
            return Ok(await _baseService.VerifyBinaryString(binaryValue));
        }
    }
}
