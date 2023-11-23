using EgrnPoddLib.EgrnClient.Data.RightWithHolders.Holders;

namespace EgrnPoddLib.EgrnClient.Data.RightWithHoldersRequest
{
    public class RightWithHolders
    {
        public List<RightHolderIndividual> Individuals { get; set; }
        public List<RightHolderLegacyEntity> LegacyEntities { get; set; }
        public List<RightHolderPublicFormation> PublicFormations { get; set; }
        public string? RightNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public long? RegisteredDateTimeOffset { get; set; }
        public string? RightTypeValue { get; set; }
        public long? ShareNumerator { get; set; }
        public long? ShareDenominator { get; set; }
        public string? ShareUnknownDescription { get; set; }
        public string? ShareDescription { get; set; }
        public DateTime? CancelDate { get; set; }
        public long? RegisteredCancelDateTimeOffset { get; set; }
        public string? RightRecordNumber { get; set; }
    }
}
