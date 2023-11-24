namespace EgrnPoddLib.PoddClient.Data;
public class MetaDataItem
{
    public string ColumnName { get; set; }
    public Type ColumnType { get; set; }
    public MetaDataItem(string columnName, Type columnType)
    {
        this.ColumnName = columnName;
        this.ColumnType = columnType;
    }
}
