namespace DataSharing
{
    public class GetTotalPage
    {
        public static int Get(int total,int pageSize)
        {
            var totalPage = total % pageSize == 0 ? total / pageSize : total / pageSize + 1;
            return totalPage;
        }
    }
}
