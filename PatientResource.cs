using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

public class PatientResource
{
    const string OmpSystemId = "http://spmid.arborian-health.com/coding-system/pcc-organization-master-patient";
    const string SocSecSystemId = "http://hl7.org/fhir/sid/us-ssn";

    public PatientResource() {}

    public void Parse() {

        var path = Environment.CurrentDirectory;

        string jsonResource = File.ReadAllText("patientresource.json");

        var parser = new FhirJsonParser();

        Patient patient = parser.Parse<Patient>(jsonResource);

        if (patient is not null)
        {
            // get identifier
            foreach (var item in patient.Identifier)
            {
                var use = item.Use;
                var coding = item.Type?.Coding;
                var code = coding?[0].Code;
                var system = item.System;
            }

            // get OmpId
            var ompIdentifier = patient.Identifier.Find(item => 
                item.System == OmpSystemId
                );
            
            var ompId = ompIdentifier?.Value;
            var ompIdDesc = ompIdentifier?.Type.Text;

            // get ssn
            var socSecIdentifier = patient.Identifier.Find(item => 
                item.System == SocSecSystemId
                );
            
            var ssnId = socSecIdentifier?.Value;
            var ssnIdDesc = socSecIdentifier?.Type.Text;

            var gender = patient.Gender;
            var dob = patient.BirthDate;

            // get the ehr system id
            var extensions = patient.Extension;
            foreach(var item in extensions) {
                var id = item.ElementId;
                var url = item.Url;
                var val = item.Value?.ToString();
            }

            // get full name
            var humanName = patient.Name.Find(item => 
                item.Use == HumanName.NameUse.Official
                );

            var patientName = humanName?.Given.FirstOrDefault() + " " + humanName?.Family;    

            var patientFacility = patient.ManagingOrganization.Identifier.Value;

        }

    }
}