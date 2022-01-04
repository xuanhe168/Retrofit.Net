namespace ExampleConsole.Models
{
    public class ResponsePage<T>
    {
        public IList<T> Collection { get; set; }
        /// <summary>
        /// 页号
        /// </summary>
        public int PageNo { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
