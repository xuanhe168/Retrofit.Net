namespace ExampleConsole.Models
{
    public class GameEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string GameName { get; set; }
        /// <summary>
        /// 游戏平台
        /// </summary>
        public int GamePlatform { get; set; }
        /// <summary>
        /// 支付回调地址
        /// </summary>
        public string CallbackUrl { get; set; }
        /// <summary>
        /// 游戏图标路径
        /// </summary>
        public string GameIcon { get; set; }
    }
}
