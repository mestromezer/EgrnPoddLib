namespace EgrnPoddLib.PoddClient.Data;
public class Request
{
    public string Sql { get; set; }
    public Request(string sql) { Sql = sql; }
}
public class RequestFrom
{
    public Request Sql { get; set; }
    public RequestFrom(Request sql)
    {
        Sql = sql;
    }
}
/*
*{
*    "sql":{
*        "sql": "requestQuery"
*    }
*}
*/
