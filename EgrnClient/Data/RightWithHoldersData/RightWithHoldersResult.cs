namespace EgrnPoddLib.EgrnClient.Data.RightWithHoldersRequest;

public class RightWithHoldersResult
{
    public SmevRequestInfo RequestInfo { get; set; } = new();
    public string? CadNumber { get; set; }
    public List<RightWithHolders> Rights { get; set; } = new();
}
