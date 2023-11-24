using EgrnPoddLib.EgrnClient.Data;
using EgrnPoddLib.EgrnClient.Data.RightWithHoldersRequest;
using EgrnPoddLib.EgrnClient.Processors;

namespace EgrnPoddLib.EgrnClient;
public class EgrnClient
{
    private PoddClient.PoddClient _poddClient;
    public EgrnClient(HttpClient client)
    {
        _poddClient = new PoddClient.PoddClient(client);
    }
    public EgrnClient(string endpointAddress)
    {
        _poddClient = new PoddClient.PoddClient(endpointAddress);
    }
    public EgrnClient()
    {
        _poddClient = new PoddClient.PoddClient();
    }
    public async Task<RightWithHoldersResult> GetRightWithHolders(string cadNumber)
    {
        var PoddResponse = await _poddClient.SendRequest($"SELECT * FROM egrn2.1.1.getrightwithholders(\"{cadNumber}\")");

        var RequestInfo = new SmevRequestInfo
        {
            CreatedAt = PoddResponse.CreatedAt,
            QueryId = PoddResponse.QueryId,
            IsSuccess = PoddResponse.IsSuccess,
            Error = PoddResponse.Error
        };
        var СadNumberFromResponse = (string?)PoddResponse.Rows[0]["realestates_cad_number"];

        var Rights = GetRightWithHoldersProcessor.GetRights(PoddResponse);

        var RightWithHoldersResult = new RightWithHoldersResult()
        {
            RequestInfo = RequestInfo,
            CadNumber = СadNumberFromResponse,
            Rights = Rights
        };
        return RightWithHoldersResult;
    }
}
