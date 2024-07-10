namespace FhirParser;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("example parsing with HL7.Fhir.R5");
        
        Console.WriteLine("documentation at https://docs.fire.ly/");

        var patient = new PatientResource();

        patient.Parse();

        var bundle = new BundleResource();

        bundle.Parse();
    }

}
