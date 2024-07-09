using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath.Expressions;

public class BundleResource
{
    const string OmpSystemId = "http://spmid.arborian-health.com/coding-system/pcc-organization-master-patient";
    const string SocSecSystemId = "http://hl7.org/fhir/sid/us-ssn";

    public BundleResource() {}

    public void Parse() {

        var path = Environment.CurrentDirectory;

        string jsonResource = File.ReadAllText("bundleresource.json");

        var parser = new FhirJsonParser();

        Hl7.Fhir.Model.Bundle bundle = parser.Parse<Hl7.Fhir.Model.Bundle>(jsonResource);

        if (bundle is not null)
        {
            var entryCount = bundle.Entry.Count;

            var observations = bundle.Entry.FindAll(e => 
                    e.Resource.TypeName == "Observation"
                );

            foreach(var obs in observations) {
                var obsResource = obs.Resource;

                var test2 = obsResource.ToList();

                var test3 = test2[0];

            }    


            foreach(var item in bundle.Entry) {

                var test1 = item.GetType().Name;
                var test2 = item.Resource.Id;

                //var name = item.Resource;
                
            }
           // var test = bundle.Entry.Find(item => item.Resource.Id == Hl7.Fhir.Model.ResourceType.Observation);
        }

    }
}