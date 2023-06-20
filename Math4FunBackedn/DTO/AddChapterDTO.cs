namespace Math4FunBackedn.DTO
{
    public class AddChapterDTO
    {
        public string? Title { get; set; }
        public string? SubTile { get; set; }
        public string? Instruction { get; set; }
        public Guid CourseId { get; set; }
    }
}
