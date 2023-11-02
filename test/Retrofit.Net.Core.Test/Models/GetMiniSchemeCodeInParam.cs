namespace Retrofit.Net.Core.Test.Models;

public class GetMiniSchemeCodeInParam
{
    /// <summary>
    /// 跳转到的目标小程序信息。
    /// </summary>
    public GetMiniSchemeCodeInParamJumpWxa jump_wxa { get; set; }

    /// <summary>
    /// 到期失效的 scheme 码的失效时间，为 Unix 时间戳。生成的到期失效 scheme 码在该时间前有效。
    /// 最长有效期为30天。is_expire 为 true 且 expire_type 为 0 时必填
    /// </summary>
    public int? expire_time { get; set; }

    /// <summary>
    /// 默认值0，到期失效的 scheme 码失效类型，失效时间：0，失效间隔天数：1
    /// </summary>
    public int? expire_type { get; set; }

    /// <summary>
    /// 到期失效的 scheme 码的失效间隔天数。生成的到期失效 scheme 码在该间隔时间到达前有效。
    /// 最长间隔天数为30天。is_expire 为 true 且 expire_type 为 1 时必填
    /// </summary>
    public int? expire_interval { get; set; }
}

public class GetMiniSchemeCodeInParamJumpWxa
{
    /// <summary>
    /// 通过 scheme 码进入的小程序页面路径，
    /// 必须是已经发布的小程序存在的页面，不可携带 query。
    /// path 为空时会跳转小程序主页。	
    /// </summary>
    public string? path { get; set; }

    /// <summary>
    /// 通过 scheme 码进入小程序时的 query，最大1024个字符，
    /// 只支持数字，大小写英文以及部分特殊字符：`!#$&'()*+,/:;=?@-._~%``
    /// </summary>
    public string? query { get; set; }

    /// <summary>
    /// 默认值"release"。要打开的小程序版本。
    /// 正式版为"release"，体验版为"trial"，开发版为"develop"，仅在微信外打开时生效。
    /// </summary>
    public string? env_version { get; set; }
}