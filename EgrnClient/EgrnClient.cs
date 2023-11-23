using EgrnPoddLib.PoddClient.Data;

namespace EgrnPoddLib.EgrnClient
{
    public class EgrnClient
    {
        private PoddClient.PoddClient _poddClient;
        public List<PoddResponse> PoddResponses { get; set; } = new();
        public EgrnClient()
        {
            _poddClient = new PoddClient.PoddClient();
        }
        // SELECT * FROM egrn2.1.1.getrightwithholders('Укажите_значение_egrn2.1.8.Realestates.cad_number_в_формате_VARCHAR')
        public async Task GetRightWithHolders(string CadNumber)
        {

        }
    }
}
