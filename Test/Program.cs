using EgrnPoddLib.EgrnClient;
using EgrnPoddLib.Fatcories;
using EgrnPoddLib.PoddClient;
using EgrnPoddLib.PoddClient.Data;
using Newtonsoft.Json;
using System.Net.WebSockets;

/* Тест для PoddResponseJsonConverter
var q = File.ReadAllText(@"C:\Users\globa\Downloads\data.json");
var response = JsonConvert.DeserializeObject<SmevResponse>(q);
Console.WriteLine(response.Rows[1]["realestates_address_readable_address"]);
*/

/*Тест работы с PoddHttpClientFactory*/
/*var factory = new PoddHttpClientFactory();
var poddClient = new PoddClient(factory.CreateClient("default"));
var response = await poddClient.SendRequest("select 1");
Console.WriteLine(response.IsSuccess);
Console.WriteLine(response.Rows[0]["EXPR$0"]);*/

/*var q = File.ReadAllText(@"C:\Users\globa\Desktop\response.json");
var EgrnClient = new EgrnClient();
var result = EgrnClient.TESTGetRightWithHolders(q);
//Console.WriteLine(result.RequestInfo.IsSuccess);
//Console.WriteLine(result.CadNumber);
//Console.WriteLine(result.Rights.Count());
//Console.WriteLine(result.Rights[1].RightTypeValue);
foreach (var right in result.Rights)
{
    Console.Write(right.RightNumber + ' ' + right.RegistrationDate + ' ' + right.RightTypeValue +'\n');
    foreach (var ind in right.Individuals)
    {
        Console.Write(ind.FristName+' ');
        Console.Write(ind.Surname+' ');
        Console.WriteLine(ind.Patronymic + ' ');
    }
    foreach (var pf in right.PublicFormations)
    {
        Console.Write(pf.RussiaName + ' ');
        Console.WriteLine(pf.MunicipalityName + ' ');
    }
    foreach (var le in right.LegacyEntities)
    {
        Console.Write(le.FullName + ' ');
    }
}*/
