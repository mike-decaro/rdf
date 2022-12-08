# rdf

## Trinity

## First Steps Project

### Setup

We need to have the package `Semiodesk.Trinity` configured in our .csproj file:

```xml
<PackageReference Include="Semiodesk.Trinity" Version="1.0.3.70" />
```

### Ontologies

Ontologies are the basically the schemas for graph databases, containing descriptions and semantics of classes and
properties of a domain.  In contrast to relational databases, we are not restricted to one schema.
The more ontologies you use, and the more you classify your data as instances of different ontologies,
the more precise the understanding of what your entities really are.

In this example, we use the 'schema.org' ontology.

The setup is in the [configuration file](./src/ontologies.config).

```xml
  <ontologies namespace="TrinityExample" >
...
    <ontology uri="http://schema.org/" prefix="schema">
      <filesource location="Ontologies/schema.ttl"/>
    </ontology>
```

In the above, we tell the framework

- where the ontology resides
- which base URI it has
- the namespace prefix (URI shorthand) we want to use.

Now, build the project.  C# representations of the ontologies are created in the background.
The code for this resides in ontologies/Ontologies.g.cs (also where you set the namespace of the generated code).

