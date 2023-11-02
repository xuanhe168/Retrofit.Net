using Retrofit.Net.Core.Attributes.Methods;
using Retrofit.Net.Core.Attributes.Parameters;
using Retrofit.Net.Core.Models;
using Retrofit.Net.Core.Test.Models;

namespace Retrofit.Net.Core.Test;

/// <summary>
/// 微信接口服务
/// </summary>
public interface IWechatService
{
    /// <summary>
    /// 获取接口调用凭据
    /// <see href="https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/mp-access-token/getAccessToken.html"/>
    /// </summary>
    /// <param name="appId">小程序唯一凭证，即 AppID</param>
    /// <param name="secret">小程序唯一凭证密钥，即 AppSecret，获取方式同 appid</param>
    /// <returns></returns>
    [HttpGet("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&")]
    Task<Response<dynamic>> GetMiniAccessToken([FromQuery]string appId,[FromQuery]string secret);

    /// <summary>
    /// 获取小程序scheme码
    /// <see href="https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/qrcode-link/url-scheme/generateScheme.html"/>
    /// </summary>
    /// <param name="access_token">接口调用凭证</param>
    /// <param name="param"></param>
    /// <returns></returns>
    [HttpPost("https://api.weixin.qq.com/wxa/generatescheme")]
    Task<GetMiniSchemeCodeOutParam> GetMiniSchemeCode([FromQuery]string access_token,[FromBody]GetMiniSchemeCodeInParam param);
}
