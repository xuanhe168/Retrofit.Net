namespace Retrofit.Net.Core.Test.Models;

public class GetAccessTokenOutParam
{
    /// <summary>
    /// 获取到的凭证
    /// </summary>
    public string access_token { get; set; }

    /// <summary>
    /// 凭证有效时间，单位：秒。目前是7200秒之内的值。
    /// </summary>
    public string expires_in { get; set; }
}
