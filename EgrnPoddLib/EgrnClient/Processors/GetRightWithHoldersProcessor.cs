using EgrnPoddLib.EgrnClient.Data.RightWithHolders.Holders;
using EgrnPoddLib.EgrnClient.Data.RightWithHoldersRequest;
using EgrnPoddLib.PoddClient.Data;

namespace EgrnPoddLib.EgrnClient.Processors;
public static class GetRightWithHoldersProcessor
{
    public static RightHolderIndividual GetHolderIndividual(Dictionary<string, object?> row)
        => new RightHolderIndividual()
        {
            Surname = (string?)row["right_holder_individuals_surname"],
            FristName = (string?)row["right_holder_individuals_first_name"],
            Patronymic = (string?)row["right_holder_individuals_patronymic"],
            BirthDate = (DateTime?)row["right_holder_individuals_birth_date"],
            Snils = (string?)row["right_holder_individuals_snils"],
            DocumentCodeValue = (string?)row["right_holder_individuals_document_code_value"],
            DocumentName = (string?)row["right_holder_individuals_document_name"],
            DocumentSeries = (string?)row["right_holder_individuals_document_series"],
            DocumentNumber = (string?)row["right_holder_individuals_document_number"],
            DocumentDate = (DateTime?)row["right_holder_individuals_document_date"],
            DocumentIssuer = (string?)row["right_holder_individuals_document_issuer"]
        };
    public static RightHolderLegacyEntity GetHolderLegacyEntity(Dictionary<string, object?> row)
        => new RightHolderLegacyEntity()
        {
            FullName = (string?)row["right_holder_legacy_entities_full_name"],
            Inn = (string?)row["right_holder_legacy_entities_inn"],
            Ogrn = (string?)row["right_holder_legacy_entities_ogrn"]
        };
    public static RightHolderPublicFormation GetHolderRightHolderPublicFormation(Dictionary<string, object?> row)
        => new RightHolderPublicFormation()
        {
            MunicipalityName = (string?)row["right_holder_public_formations_municipality_name"],
            UnionStateName = (string?)row["right_holder_public_formations_union_state_name"],
            ForeignPublicName = (string?)row["right_holder_public_formations_foreign_public_name"],
            RussiaName = (string?)row["right_holder_public_formations_russia_name"],
            SubjectOfRfName = (string?)row["right_holder_public_formations_subject_of_rf_name"]

        };
    public static List<RightWithHolders> GetRights(SmevResponse response)
    {
        var rights = new List<RightWithHolders>();
        foreach (var row in response.Rows)
        {
            // Keys to identify the right
            var RightNumber = (string?)row["rights_right_number"];
            var RegistrationDate = (DateTime?)row["rights_registration_date"];
            var RightTypeValue = (string?)row["rights_right_type_value"];

            // Seraching if a element's like right was already created
            var foundRights = rights.Where( // Re-write as LINQ query
                r => r.RightNumber == RightNumber
                && r.RegistrationDate == RegistrationDate
                && r.RightTypeValue == RightTypeValue
                );

            RightWithHolders right;
            if (foundRights.Count() != 0) // Second entry of current record
            {
                right = rights.First(r => r.RightNumber == RightNumber // Find in the way to add holders from current element
                && r.RegistrationDate == RegistrationDate
                && r.RightTypeValue == RightTypeValue);
            }
            else // First entry of current element
            {
                right = new RightWithHolders // Create new right and initialize right's data
                {
                    RightNumber = RightNumber,
                    RegistrationDate = RegistrationDate,
                    RegisteredDateTimeOffset = (long?)row["rights_registered_date_time_offset"],
                    RightTypeValue = RightTypeValue,
                    ShareNumerator = (long?)row["rights_share_numerator"],
                    ShareDenominator = (long?)row["rights_share_denominator"],
                    ShareUnknownDescription = (string?)row["rights_share_unknown_description"],
                    ShareDescription = (string?)row["rights_share_description"],
                    CancelDate = (DateTime?)row["rights_cancel_date"],
                    RegisteredCancelDateTimeOffset = (long?)row["rights_registered_cancel_date_time_offset"],
                    RightRecordNumber = (string?)row["rights_right_record_number"],
                };
                rights.Add(right);
            }
            right.Individuals.Add(GetHolderIndividual(row));
            right.LegacyEntities.Add(GetHolderLegacyEntity(row));
            right.PublicFormations.Add(GetHolderRightHolderPublicFormation(row));
        }
        return rights;
    }
}
