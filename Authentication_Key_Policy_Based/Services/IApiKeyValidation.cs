namespace Authentication_Key_Policy_Based.Services
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}
