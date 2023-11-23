namespace EgrnPoddLib.EgrnClient.Data
{
    public class SmevRequestInfo
    {
        public DateTime? CreatedAt { set; get; }
        public string? QueryId { set; get; }
        public bool? IsSuccess { set; get; }
        public string? Error { set; get; }
    }
}
