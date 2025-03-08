namespace Entities.Dto
{
    public class ReminderFilterDto
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "CreatedAt";
        public string SortDirection { get; set; } = "ASC";
    }
}
