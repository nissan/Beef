﻿# Template structure

The templates are defined using XML that follows a prescribed structure. There are some basic language capabilities such as looping, conditions, variables, etc. to facilitate the generation output.

A template can result in none, one or many, generated output artefacts.

## XML node types

The following XML node types are supported:
- Element - these specifically control the code generation logic.
- CDATA - these contain the actual *output-text* for the generated artefact.
- Comment - these are supported to aid XML readibility.

***

# XML elements

XML elements, and their hierarchy drive the code-generation; some elements are reserved to perform a specific function.

## Reserved elements

The following XML elements represent the language statements used by the code generator; these are further [detailed](#Language-constructs-detailed):
- `If` - enables if/then/else conditional statements.
- `Set` - sets a named variable with a value.
- `Increment` - increments a named variable up or down.
- `Exception`- throws an expection to terminate generation.
- `ForEachList` - iterates a list (being a string separated by a specified character).

The following XML elements support specific data elements:
- `Config` - accesses the root element for the XML data source; includiny any name/value pairs passed via the command line; contains reserved attribute:
	- `Config.OutputGenDirName` - being an additional directory name used for all artefact outputs.
- `System` - accesses a system-wide XML data source; contains reserved attribute:
  - `System.Index` - being the zero-based index where looping an entity.

The following XML elements support specific outputs:
- `Header` - standard header content for all files generated from a template.
- `Footer` - standard footer content for all files generated from a template.

## Data source elements

Any other element name is used to reference the same named element in the data source. The code generator will start searching down through the children until it finds a same named element. Where not found, it will the start searching from the element up through its parents until found. Finally, if still not found an appropriate exception will be thrown.

```xml
<Element Condition="condition" Not="true|false">
  <Statement>
</Element>
```

See the [If element](#If-element) below for further information regarding the `Condition` and `Not` attributes.

Following is an example:
```xml
<Entity Condition="Entity.Name != null">
  <Operation>
    <![CDATA[public int {{Entity.Name}}_{{Operation.Name}} { get; set; }]]>
  <Operation>
</Entity>
```

***

# Var-Value

There are two main means to obtain a var-value (variable value):
- Element attribute value using dot-based notation.
- Declared/constant value; where a string is enclosed in single quotes.

## Var-Value: element attribute value

To reference any XML element attribue value for a data source element, including `Config` or `System`, a dot-based notation is used; i.e: `Element.Attribute`. For example, if an *element* is named `Entity` and the *attribute* is named `Name`, then the dot-based notation would be `Entity.Name`.

A *var-value* can also produce *output-text* by prefixing the value with a `^` symbol; e.g. `^The Entity Name is {{Entity.Name}}.` would result in the `{{Entity.Name}}` being replaced with the actual `Entity.Name` *var-value*.

The *var-value* types supported are: `string`, `bool` (true/false) and `float` (number) generally inferred from its underlying usage; at a minium all values are treated as a `string`.

String-based values can be further transformed, using one of the following:
- `ToLowerCase` - converts to 'lowercase'.
- `ToUpperCase` - converts to 'UPPERCASE.'
- `ToPrivateCase` - converts to '_privateCase'.
- `ToArgumentCase` - converts to 'argumentCase'.
- `ToPascalCase` - converts to 'PascalCase'.
- `ToCamelCase` - converts to 'camelCase'.
- `ToSentenceCase` - converts to 'Sentence Case'.
- `ToPlural` - converts to pluralised name.
- `ToComments` - converts to comments where any embedded `{{xyz}}` string will be converted to C# see comments (as per ToSeeComments). 
- `ToSeeComments` - converts to C# see comments; e.g. `List<int>` would become `<see cref="List{int}"/>` respectively.
- `ConvertMustacheComments` - converts mustache value to C# see comments; e.g. `{{xxx}}` would become `<see cref='xxx'/>`.

The above transformations suffix the existing dot-based notation (note that the transform must also be prefixed by a colon); e.g. `Entity.Name:ToPascalCase`.

## Var-value: declared/constant value

A *var-value* can be either determined using the dot-based notation desribed above, or using one of the supported value declarations:
- `null` - null value;
- `true` or `false` - Boolean value;
- `'xxx'` - quotes denote a string-based constant;
- numeric - converted to a decimal value;
- any other value will result in en exception being thrown.

***

# Output-text

Any *output-text* also supports moustache formatting to enable templated value replacement. This enables a *var-value* to appear in a text string. Moustache formatting is where the *var-value* is prefixed by {{ and suffixed by }}; hence the moustache naming.

For example, `'The Entity Name is {{Entity.Name}}.'` would result in the `{{Entity.Name}}` being replaced with the actual `Entity.Name` *var-value*.

***

# Language constructs detailed

As logic statements are required to control the output the following language-like constructs are provided.

## `If` element

Enables if/then/else condition statements.

```xml
<If Condition="condition" Not="true|false">
  <Then></Then>
  <Else></Else>
</If>
```

The **condition** is used to compare two values being `lval operator rval`, being a left and right *var-value* and a corresponding comparison operator:
- `==` (lval is *equal* to rval)
- `!=` (lval is *not equal* to rval)
- `>` (lval is *greater* than to rval)
- `<` (lval is *less than* the rval)
- `>=` (lval is *greather than or equal to* the rval)
- `<=` (lval is *less than or equal to* the rval)
- `contains` (lval *contains* the rval; both must be strings)

The **Condition** attribute allows for multiple checks (there is no support for brackets) using:
- And (`and` + `&&`)
- Or (`or` + `||`)

The **Not** attribute is optional, and reverses the conditional true/false outcome.

The **Then** and **Else** child elements are optional; where neither are specified then **true** is assumed for the specified child statement.

Following are some examples:
```xml
<If Condition="Entity.Abstract == true">
  <![CDATA[xyz]]>
</If>

<If Condition="Entity.ConstType == 'int'">
  <Then><![CDATA[true]]></Then>
  <Else><![CDATA[false]]></Else>
</If>

<If Condition="Property.Inherited == false and Property.RefDataMapping != null" Not='true'>
  <![CDATA[abc]]>
</If>
```

## `Set` element

Sets a named variable (using dot-based notation) with a specified *var-value*.

```xml
<Set Name="element.attribute" Condition="condition" Not="true|false" Value="var-value" Otherwise="var-value" />
```

The used attributes are:
- `Name` - element attribute using the dot-based notation.
- `Condition` - compare two values being `lval operator rval` and sets the value when true (optional).
- `Not` - reverses the conditional true/false outcome (optional).
- `Value` - *var-value* to be used for the set where condition equals true.
- `Otherwise` - *var-value* to be used for the set where condition equals false (optional). Where not specified no set will occur when the condition is false.

Following are some examples:
```xml
<Set Name="System.EntityName" Value="Entity.Name" />
<Set Name="System.ReturnType" Condition="Entity.ReturnType == null" Value="'void'" />
<Set Name="System.EntityExclude" Value="false" />
```

## `Increment` element

Increments a named variable (using dot-based notation) up or down with an optional *var-value*; i.e. adds or subtracts where negative.

```xml
<Increment Name="element.attribute" Condition="condition" Not="true|false" Value="var-value" />
```

The used attributes are:
- `Name` - element attribute using the dot-based notation to update.
- `Condition` - compare two values being `lval operator rval` and increments the value when true (optional).
- `Not` - reverses the conditional true/false outcome (optional).
- `Value` - optional *var-value* to add; defaults to 1 where not specified (optional).

Following are some examples:
```xml
<Increment Name="System.Counter"  />
<Increment Name="System.CountDown" Condition="System.CountDown > 0" Value="-1" />
```

## `Exception` element

Throws an expection to terminate generation.

```xml
<Exception Message="output-text" Condition="condition" Not="true|false" />
```

The used attributes are:
- `Message` - the message *output-text*.
- `Condition` - compare two values being `lval operator rval` and throw an exception when the value when true (optional).
- `Not` - reverses the conditional true/false outcome (optional).

Following is an example:
```xml
<If Condition="Entity.Type == null">
  <Exception Message="Entity '{{Entity.Name}}' must have a Type specified." />
</If>

<Exception Message="Entity '{{Entity.Name}}' must have a Type specified." Condition="Entity.Type == null" />
```

## `ForEachList` element

Iterates a list (being a string separated by a specified character).

```xml
<ForEachList Name="element.attribute" Condition="condition" Not="true|false" Separator="char" />
```

The used attributes are:
- `Name` - element attribute using the dot-based notation to iterate (must be a string).
- `Condition` - compare two values being `lval operator rval` and throw an exception when the value when true (optional).
- `Not` - reverses the conditional true/false outcome (optional).
- `Separator` - specifies the separator character (defaults to `,`).

The following attributes are updated:
- `System.Value` - the iterated value.
- `System.Index` - the zero-based index for the value.

Following is an example:
```xml
<ForEachList Name="StoredProcedure.MergeOverrideIdentityColumns">
  <![CDATA[{{System.Value}}>
</ForEachList>
```

***

# Controlling artefact generation

There are the following reserved attributes that when specified on an `Element` control the artefact generation output:
- `OutputFileName` - specifies the output file/artefact name; uses *output-text* formatting.
- `OutputDirName` - specifies the output directory name; uses *output-text* formatting (optional).
- `IsOutputNewOnly` - indicates to output the file where new only; i.e. do not override existing (optional).

When the closing `Element` is found then the artefact file is considered complete, and the code generation is ready to output a new artefact. An exception will be throw if a new artefact is created without completing (closing) the prior.

Following is an example where a new file is created for each `Entity` element defined within the data source:

```xml
<Entity Condition="Entity.ExcludeEntity == false and Entity.EntityScope == Config.EntityScope" OutputFileName="{{Entity.Name}}.cs" OutputDirName="{{Entity.Namespace}}">
  <![CDATA[xxx]]>
<Entity>
```