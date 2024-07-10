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

            var practitioners = bundle.Entry.FindAll(e =>
                    e.Resource.TypeName == "Practitioner"
                );

            foreach (var entryComponent in practitioners) {

                foreach (var child in entryComponent.Children) {

                    Practitioner? practitioner = child as Practitioner;

                    if (practitioner != null) {

                    // HumanName nameList = practitioner.Name;

                    // var resourceMap = child.ToDictionary();

                    // var nameList = resourceMap["name"] as HumanName[];

                        var providerName = String.Empty;

                        foreach (var name in practitioner.Name) {
                            if (name.Use == HumanName.NameUse.Official) 
                                providerName = $"{name?.Family}, {name?.GivenElement[0]}";
                        }
    
                    }
                }
            }


            var observations = bundle.Entry.FindAll(e => 
                    e.Resource.TypeName == "Observation"
                );

            foreach (var observation in observations) {

                foreach (var child in observation.Children) {
                
                    var resourceMap = child.ToDictionary();

                    CodeableConcept? code = resourceMap["code"] as CodeableConcept;

                    if (code != null) {

                        if (code.Text == "weight") {
                            
                            Quantity? valueQuantity = resourceMap["value"] as Quantity;

                            var unit = valueQuantity?.Unit;
                            var val = valueQuantity?.Value; 
                        }

                      if (code.Text == "height") {
                            
                            Quantity? valueQuantity = resourceMap["value"] as Quantity;

                            var unit = valueQuantity?.Unit;
                            var val = valueQuantity?.Value; 
                        }

                        if (code.Text == "oxygenSaturation") {
                            
                            Quantity? valueQuantity = resourceMap["value"] as Quantity;

                            var unit = valueQuantity?.Unit;
                            var val = valueQuantity?.Value; 
                        }

                        if (code.Text == "heartrate") {
                            
                            Quantity? valueQuantity = resourceMap["value"] as Quantity;

                            var unit = valueQuantity?.Unit;
                            var val = valueQuantity?.Value; 
                        }

                    }
                }
            }    
        }
    }
}