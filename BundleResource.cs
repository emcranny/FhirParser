
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

public record VitalSigns
{
    public string? Code { get; set; }    
    public decimal? Value { get; set; }
    public string? Unit { get; set; }
}

public class BundleResource
{
    public BundleResource() {}

    public void Parse() {

        var path = Environment.CurrentDirectory;

        string jsonResource = File.ReadAllText("bundleresource.json");

        var parser = new FhirJsonParser();

        Bundle bundle = parser.Parse<Bundle>(jsonResource);

        if (bundle is not null)
        {
            var entryCount = bundle.Entry.Count;

            // Practitioners
            var practitioners = bundle.Entry.FindAll(e =>
                    e.Resource.TypeName == "Practitioner"
                );

            foreach (var entryComponent in practitioners) {

                foreach (Practitioner practitioner in entryComponent.Children) {

                    if (practitioner != null) {

                        var providerName = String.Empty;

                        foreach (var name in practitioner.Name) {
                            if (name.Use == HumanName.NameUse.Official) 
                                providerName = $"{name?.Family}, {name?.GivenElement[0]}";
                        }
                    }
                }
            }

            // Observations
            var observations = bundle.Entry.FindAll(e => 
                    e.Resource.TypeName == "Observation"
                );

            List<VitalSigns> vitalSigns = [];

            foreach (var entryComponent in observations) {

                foreach (Observation observation in entryComponent.Children) {
                
                    VitalSigns vitals = getVitalSigns(observation);

                    if (vitals.Code != null)
                        vitalSigns.Add(vitals);


                    // if (observation.Code.Text == "weight")
                    // {
                    //     Quantity? valueQuantity = observation.Value as Quantity;
                    //     var unit = valueQuantity?.Unit;
                    //     var val = valueQuantity?.Value; 
                    // }

                    // if (observation.Code.Text == "oxygenSaturation")
                    // {
                    //     Quantity? valueQuantity = observation.Value as Quantity;
                    //     var unit = valueQuantity?.Unit;
                    //     var val = valueQuantity?.Value; 
                    // }

                }
            }  

            foreach(var vital in vitalSigns) 
            {
                Console.WriteLine(vital);
            }  
        }
    }

    public VitalSigns getVitalSigns (Observation observation)
    {
        VitalSigns vitals = new();

        var code = observation.Code;

        if (code != null) 
        {
            Quantity? valueQuantity = observation.Value as Quantity;

            vitals = new()
            {
                Code = code.Text,
                Unit = valueQuantity?.Unit,
                Value = valueQuantity?.Value,
            };
        }

        return vitals;
    }
}