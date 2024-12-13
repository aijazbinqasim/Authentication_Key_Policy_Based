using Authentication_Key_Policy_Based.Config;
using Authentication_Key_Policy_Based.Services;
using Microsoft.AspNetCore.Authorization;

namespace Authentication_Key_Policy_Based.Security
{
    public class ApiKeyHandler(IApiKeyValidation apiKeyValidation, IHttpContextAccessor httpContextAccessor) : AuthorizationHandler<ApiKeyRequirement>
    {
        private readonly IApiKeyValidation _apiKeyValidation = apiKeyValidation;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            string? apiKey = _httpContextAccessor?.HttpContext?.Request.Headers[Constant.ApiKeyHeaderName].ToString();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            if (!_apiKeyValidation.IsValidApiKey(apiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
