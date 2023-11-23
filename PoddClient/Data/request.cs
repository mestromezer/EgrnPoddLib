namespace EgrnPoddLib.PoddClient.Data;
public class request
{
    public string sql { get; set; }
    public request(string sql) { this.sql = sql; }
}
public class requestForm
{
    public request sql { get; set; }
    public requestForm(request sql)
    {
        this.sql = sql;
    }
}
/*
*{
*    "sql":{
*        "sql": "requestQuery"
*    }
*}
*/
