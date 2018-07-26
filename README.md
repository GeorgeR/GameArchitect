# Game Architect #
Taking the boilerplate and guff out of game development, allowing you to concentrate on the important stuff.

A C# based code generation and deployment framework where you describe your the behavior of your entities and GA generates the necessary code to the platforms of your choice.

Further, validation is performed so that conflicting and invalid descriptions are detected (for example a property can't be both required and optional), and testing requirements can be inferred.

## GameArchitect.Tasks.* ##
Infrastructure required to create and run tasks, where a task takes a parameter object containing at least an options object.

## GameArchitect.Tasks.CodeGeneration.* ##
Code generation templates for a variety of languages.
- CSharp
- C++ (CXX)
- Unreal (C++)

### GameArchitect.Tasks.CodeGeneration.SpatialOS ###
SpatialOS schema generation.

### GameArchitect.Tasks.CodeGeneration.OpenAPI ###
Generation of Swagger/OpenAPI specification files for web APIs.

### GameArchitect.Tasks.CodeGeneration.Yuml ###
Generation of UML diagrams in yuml markup.

### GameArchitect.Tasks.CodeGeneration.JSONSchema ###
Generation of JSON Schema files.

## GameArchitect.Design.* ##
A series of attributes to be used in your entities to better describe their behavior.

- Tag  
A generic tag attribute containing one or more strings.

- Description  
A description that can then be emitted to generated code files.

- Required  
Indicates a property is required.

- Optional  
Indicates a property is optional.

- Range  
Specify a range for a numeric property or bind a minimum and maximum value to another property (by name).

- Default  
Specify a default value for this property.

- Note  
An internal development note that can be emitted to generated code, for example to remind you of an implementation detail.

- Async  
Marks a function as being asynchronous. Depending on the generated language, this could mean it returns a future/promise or provides a callback.

- Immutable  
The function does not alter it's parent object. 

- Deconstruct  
Deconstruct a specified parameter, property etc. to one or more of it's component parts. This can occur always, or only when the type isn't supported by the generated language or domain.

- Client*
    - ClientInclude
    - ClientExclude
    - ClientOnly

- Server*
    - ServerInclude
    - ServerExclude
    - ServerOnly

- Editor*
    - EditorReadWrite
    - EditorReadOnly
    - EditorInclude
    - EditorExclude
    - EditorOnly

- Runtime*
    - RuntimeReadWrite
    - RuntimeReadOnly

- Db*  
A series of attributes related to database storage.
    - DbTable  
    Optionally name the table that this entity belongs to.

    - DbKey  
    Any entity that needs to be persisted shouldnhave at least one DbKey property. You can specify multiple for composite keys.

    - DbReference  
    The attached property is a foreign key.

    - DbType  
    Optionally override the type with another.
- Net*  
A series of network related attributes.
    - NetProperty  
    The property should be persisted over the network.

    - NetEvent  
    The event or function should be multicast. Implies there is no return value.

    - NetCommand  
    An RPC.

    - NetType  
    Further information about how this type should be handled on the network.


### GameArchitect.Design.Unreal ###
Unreal specific attributes. Should be used only when the automatic behavior needs to be overridden. For example a struct in C# will automatically be emitted as an UnrealStruct.
- UnrealStruct
- UnrealComponent
- UnrealActor
- UnrealEnum
- UnrealProperty
- UnrealEvent
- UnrealFunction
- UnrealBaseClass

### GameArchitect.Design.SpatialOS ###
SpatialOS specific attributes.
- SpatialType
- SpatialComponent
- SpatialField
- SpatialEvent
- SpatialCommand

## GameArchitect.Support.* ##
Multiple vendor specific "helpers".

### GameArchitect.Support.JSON ###
A contract resolver for JSON.NET.

### GameArchitect.Support.EntityFramework ###
Entity mapping for EF so you can deploy to a database.

### GameArchitect.Support.Testing ###
Assists in the automatic creation of unit tests. Emits JSON description of unit test conditions and expectations. These can then be read back in by a code generation task to create language specific unit tests.