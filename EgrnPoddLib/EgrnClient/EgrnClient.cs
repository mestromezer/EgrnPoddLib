using EgrnPoddLib.EgrnClient.Data;
using EgrnPoddLib.EgrnClient.Data.RightWithHoldersRequest;
using EgrnPoddLib.EgrnClient.Processors;
using EgrnPoddLib.PoddClient.Data;
using System.Net.Http;

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
    public async Task<RightWithHoldersResult> GetRightWithHolders(string CadNumber)
    {
        var poddResponse = await _poddClient.SendRequest($"SELECT * FROM egrn2.1.1.getrightwithholders(\"{CadNumber}\")");

        var requestInfo = new SmevRequestInfo
        {
            CreatedAt = poddResponse.CreatedAt,
            QueryId = poddResponse.QueryId,
            IsSuccess = poddResponse.IsSuccess,
            Error = poddResponse.Error
        };
        var cadNumber = (string?)poddResponse.Rows[0]["realestates_cad_number"];

        var rights = GetRightWithHoldersProcessor.GetRights(poddResponse);

        var rightWithHoldersResult = new RightWithHoldersResult()
        {
            RequestInfo = requestInfo,
            CadNumber = cadNumber,
            Rights = rights
        };
        return rightWithHoldersResult;
    }
}
