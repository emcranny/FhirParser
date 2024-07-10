using System.Runtime.CompilerServices;
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

        Bundle bundle = parser.Parse<Bundle>(jsonResource);

        if (bundle is not null)
        {
            var entryCount = bundle.Entry.Count;

            var observations = bundle.Entry.FindAll(e => 
                    e.Resource.TypeName == "Observation"
                );

            foreach (var observation in observations) {

                foreach (var child in observation.Children) {
                
                    var testDict = child.ToDictionary();

                    CodeableConcept testItem = testDict["code"] as CodeableConcept;

                    if (testItem != null) {
                        var test = testItem.Text;

                        if (test == "weight") {
                        
                        Valu vq = testDict["valueQuantity"] as 
                        }
                    }
                }
                // var weightObject = observation.Resource
                //     .ToList()
                //     .Find(item => 
                //         item.Key == "code" &&
                //         item.Value.ToString() == "weight"
                // );

                // var testList = observation.Resource.ToList();

                // foreach (var item in testList) {
                //     var test0 = item.Key;
                //     var test1 = item.Value.ToString();
            
                // }
            //     var test2 = obsResource.ToList();

            //     var test2a = test2[0].Key;
            //     var test2b = test2[0].Value;
            //     var test2c = test2[1].Key;
            //     var test2d = test2[1].Value;

            //     var test3 = test2[0];
            }    


            // foreach(var item in bundle.Entry) {

            //     var test1 = item.GetType().Name;
            //     var test2 = item.Resource.Id;

            //     //var name = item.Resource;
                
            // }
           // var test = bundle.Entry.Find(item => item.Resource.Id == Hl7.Fhir.Model.ResourceType.Observation);
        }
    }
}