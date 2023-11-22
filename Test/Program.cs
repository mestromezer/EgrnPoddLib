using EgrnPoddLib;
using EgrnPoddLib.Data;
using Newtonsoft.Json;

var client = new PoddClient(null,null);
PoddResponse result = client.SendRequestAsync("select 1").Result;
Console.WriteLine(result.QueryId);
Console.WriteLine(result.Rows[0]["EXPR$0"]);