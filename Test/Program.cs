using EgrnPoddLib;
using EgrnPoddLib.Data;

var client = new PoddClient(null,null);
PoddResponse result = client.SendRequestAsync("select 1").Result;
//string data = result.getContentAsStringAsync().Result;

//Console.WriteLine(data);