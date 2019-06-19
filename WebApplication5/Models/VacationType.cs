namespace WebApplication5.Models
{
    public enum VacType { Annual = 1, Sick, Sudden }
    public class VacationType
    {
        public int Id { get; set; }
        public string LeaveType { get; set; }
    }
}