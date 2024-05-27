namespace Math4FunBackedn.DTO
{
    public class PageResult<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
}
