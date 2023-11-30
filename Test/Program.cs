using EgrnPoddLib.EgrnClient;

var q = File.ReadAllText(@"C:\Users\globa\OneDrive\Документы\Самараинфоспутник\Смев4\response.json");
var EgrnClient = new EgrnClient();
var result = EgrnClient.TESTGetRightWithHolders(q);
Console.Write("CreatedAt: "+result.RequestInfo.CreatedAt +"\n");
Console.Write("QueryId: "+result.RequestInfo.QueryId + "\n");
foreach (var right in result.Rights)
{
    Console.WriteLine($"Право: {right.RightNumber}");
    foreach (var ind in right.Individuals)
    {
        Console.WriteLine("Individual:");
        Console.Write(ind.FullName + ' ' + ind.BirthDate + ' ' + ind.Snils + ' ' + ind.DocumentCodeValue + ' ' + ind.DocumentName
        + ' ' + ind.DocumentSeries + ' ' + ind.DocumentNumber + ' ' + ind.DocumentDate + ' ' + ind.DocumentIssuer + '\n');
    }
    foreach (var le in right.LegacyEntities)
    {
        Console.WriteLine("LegacyEntitie:");
        Console.Write(le.FullName + ' ' + le.Inn + ' ' + le.Ogrn + '\n');
    }
    foreach (var pf in right.PublicFormations)
    {
        Console.WriteLine("PublicFormation:");
        Console.Write(pf.MunicipalityName + ' ' + pf.ForeignPublicName + ' ' + pf.UnionStateName + ' ' + pf.RussiaName
        + ' ' + pf.SubjectOfRfName + '\n');
    }
    Console.WriteLine(right.RightNumber);
    Console.WriteLine(right.RegistrationDate);
    Console.WriteLine(right.RegisteredDateTimeOffset);
    Console.WriteLine(right.RightTypeValue);
    Console.WriteLine(right.RightTypeValueCode);
    Console.WriteLine(right.ShareNumerator);
    Console.WriteLine(right.ShareDenominator);
    Console.WriteLine(right.ShareUnknownDescription);
    Console.WriteLine(right.ShareDescription);
    Console.WriteLine(right.CancelDate);
    Console.WriteLine(right.RegisteredCancelDateTimeOffset);
    Console.WriteLine(right.RightRecordNumber);
}