using StudioAgenda.Domain.Seguranca.Tokens;

namespace StudioAgenda.Api.Token;

public class HttpContextTokenProvider : IAccessTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    //recuperando o usuário da requisção
    public string PegarToken()
    {
        var accessToken = _httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString();
        return accessToken;
    }
}