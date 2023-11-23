using EgrnPoddLib.PoddClient.Data;
using Newtonsoft.Json;

var q = File.ReadAllText(@"C:\Users\globa\Downloads\data.json");
var response = JsonConvert.DeserializeObject<SmevResponse>(q);
Console.WriteLine(response.Rows[1]["realestates_address_readable_address"]);