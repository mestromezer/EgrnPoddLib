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

