namespace ExampleConsole.Models
{
    public class RequestPage
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public RequestPage(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
