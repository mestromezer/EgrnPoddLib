using EgrnPoddLib.EgrnClient.Data.RightWithHolders.Holders;

namespace EgrnPoddLib.EgrnClient.Data.RightWithHoldersRequest;
public class RightWithHolders
{
    private Dictionary<string, string> _rightTypes = new Dictionary<string, string>()
    {
        { "001001000000", "Собственность" },
        { "001002000000", "Общая долевая собственность" },
        { "001003000000", "Общая совместная собственность" },
        { "001004000000", "Хозяйственное ведение" },
        { "001005000000", "Оперативное управление" },
        { "001006000000", "Пожизненное наследуемое владение" },
        { "001007000000", "Постоянное (бессрочное) пользование" },
        { "001008000000", "Сервитут (право)" },
        { "001009000000", "Право владения, пользования и распоряжения имуществом Банка России" },
        { "001099000000", "Иные права" },
    };
    public List<RightHolderIndividual> Individuals { get; set; } = new();
    public List<RightHolderLegacyEntity> LegacyEntities { get; set; } = new();
    public List<RightHolderPublicFormation> PublicFormations { get; set; } = new();
    public string? RightNumber { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public long? RegisteredDateTimeOffset { get; set; }
    public string? RightTypeValue { get; set; }
    public string? RightTypeValueCode
    {
        get
        {
            if (RightNumber == null) return null;
            return (from el in _rightTypes where el.Value == RightTypeValue select el.Key).First();
        }
    }
    public long? ShareNumerator { get; set; }
    public long? ShareDenominator { get; set; }
    public string? ShareUnknownDescription { get; set; }
    public string? ShareDescription { get; set; }
    public DateTime? CancelDate { get; set; }
    public long? RegisteredCancelDateTimeOffset { get; set; }
    public string? RightRecordNumber { get; set; }
}