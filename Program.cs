namespace FhirParser;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var patient = new PatientResource();

        patient.Parse();
    }

}
