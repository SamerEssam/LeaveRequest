namespace WebApplication5.Models
{
    public enum ReqStatus : byte { Pending = 1, Approved , Rejected}
        
    public class RequestStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }

    }
}




