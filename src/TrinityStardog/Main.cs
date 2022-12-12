
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using Semiodesk.Trinity;


namespace TrinityStardogExample
{
    class Program
    {
        /// The model we are working on
        static IModel Model { get; set; }

        static void Main()
        {
            // Some generic stuff I dont understand
            OntologyDiscovery.AddAssembly(Assembly.GetExecutingAssembly());
            MappingDiscovery.RegisterCallingAssembly();


            // new SparqlEndpointStoreProvider()



            NetworkCredential networkCredential = new NetworkCredential("un", "pw");
            var store = StoreFactory.CreateSparqlEndpointStore(new Uri("https://stardog.domain/db/query"), null, networkCredential);

            var model = store.GetModel(new Uri("to replace"));

            // Concept con = model.GetResource<Concept>(new Uri("http://www.imohealth.com/data/concept/_1000162095"));

            SparqlQuery query = new SparqlQuery(@"select
                            ?con ?sno
                        from imo:concept
                        from sct:900000000000207008
                        where {
                            ?con a imo:Problem .
                            ?con imo:snomedNarrowerThan ?sno
                        }");
            ISparqlQueryResult sparqlQueryResult = model.ExecuteQuery(query);

            var queryResultBindings = sparqlQueryResult.GetBindings();

            Console.WriteLine(queryResultBindings.Count());

            //sparqlQueryResult.GetAnwser();
        }
    }

}
