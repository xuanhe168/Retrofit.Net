namespace Retrofit.Net.Core.Test.Models;

public class GetMiniSchemeCodeOutParam
{
    /// <summary>
    /// 错误码
    /// </summary>
    public int? errcode { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? errmsg { get; set; }

    /// <summary>
    /// 生成的小程序 scheme 码
    /// </summary>
    public string? openlink { get; set; }
}
