namespace EgrnPoddLib.PoddClient.Data;
public class MetaDataItem
{
    public string ColumnName { get; set; }
    public Type ColumnType { get; set; }
    public MetaDataItem(string ColumnName, Type ColumnType)
    {
        this.ColumnName = ColumnName;
        this.ColumnType = ColumnType;
    }
}
