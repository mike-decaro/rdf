using Semiodesk.Trinity;
using System;
using System.Collections.ObjectModel;

namespace TrinityStardogExample
{
    [RdfClass(IMO.Concept)]
    public class Concept : Resource
    {

        [RdfProperty(IMO.snomedSameAs)]
        public ObservableCollection<Concept> SnomedSameAs { get; set; }

        [RdfProperty(IMO.snomedNarrowerThan)]
        public ObservableCollection<Concept> SnomedNarrowerThan { get; set; }

        public Concept(Uri uri) : base(uri) { }

    }
}
