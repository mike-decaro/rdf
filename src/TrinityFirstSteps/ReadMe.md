# Trinity First Steps Project

Note: for this project, we don't connect to a database.  This is all done locally in-memory.

## Setup

We need to have the package `Semiodesk.Trinity` configured in our .csproj file:

```xml
<PackageReference Include="Semiodesk.Trinity" Version="1.0.3.70" />
```

## Ontologies

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

## Domain Model

We want to create a domain model that contains an abstract [Thing](./src/ObjectModel/Thing.cs)
and a [Person class](./src/ObjectModel/Person.cs).

We derive the `Thing` class from Resource, which we do by decorating the class and the properties with the RDF classes
and properties from the schema ontology.  There is a distinction between the generated `schema` class and the upper-case
`SCHEMA` class. The upper case version provides the string representation of the ontology elements and can be used in
decorators and attributes. The lower case variant provides Class and Property instances.

The `Person` class is derived from `Thing` and has a property that models the relationship between people.

## Building the Application

In [program.cs](./src/Program.cs), we need to tell the framework know where to look for ontologies and our mapping.

The "add" and "register" assembly is code that came with the sample.
Then we load a DotNet RDF memory store from the StoreFactory.

```C#
IStore store = StoreFactory.CreateStore("provider=dotnetrdf");
```

Then we get a handle to a model from the memory store.  To the name of the model we give a URI.
(Note: A model in a graph database is a container for a set of resources that belong together logically.)

```C#
Context = store.GetModel(new Uri("http://example.com/model"));
```

Then we add our mapped objects to the model.

First let the model create a new resource of type Person.  In our example we are going for a readable URI
`http://example.com/person/john`.
After adding values to the resource, we need to commit it to the model by calling the Commit() method.

```C#
Person john = Model.CreateResource<Person>(new Uri("http://example.com/person/john"));
john.FirstName = "John";
john.LastName = "Doe";
john.BirthDate = new DateTime(2010, 1, 1);
john.Commit();
```

We do similarly for an "Alice" resource of type Person as well.  We also make an "Alice2" and a "John2" resource with
no information about them other than that they know each other.

To access the resources directly, we call the `Model.GetResource()` method.
If you know the response structure, we can provide a type in the place.  If we omit the type, it will return the object,
but we have to cast it manually.

The rest of [our program](./src/Program.cs) collects all members of the "doe" family, so "Alice" and "John." It does
this by querying from the model, building a LINQ query via the `AsQueryable()` method.  Then, we print these people's
metadata to console and delete them, via `Model.DeleteResource()`, from our model.

```txt
> src % dotnet run

Starting ontology generator in /path//src

Name: John Doe Birthdate: 1/1/2010 12:00:00 AM
Name: Alice Doe Birthdate: 1/1/2000 12:00:00 AM
Empty: True
```
