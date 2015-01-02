_**IMPORTANT NOTE:**_ This project is currently in beta and the documentation is currently incomplete. Please bear with us while the documentation is being written.

####SuperScript offers a means of declaring assets in one part of a .NET web solution and have them emitted somewhere else.


When developing web solutions, assets such as JavaScript declarations or HTML templates are frequently written in a location that differs from their desired output location.

For example, all JavaScript declarations should ideally be emitted together just before the HTML document is closed. And if caching is preferred then these declarations should be in an external file with caching headers set.

This is the functionality offered by SuperScript.



##The JavaScriptContainer Control

This project contains one publicly-accessible class, `SuperScript.JavaScript.WebForms.Containers.JavaScriptContainer`.
This class derives from [`SuperScript.Container.WebForms.Container`](https://github.com/Supertext/SuperScript.Container.WebForms/blob/master/Container.cs)
(which itself derives from `System.Web.UI.WebControls.PlaceHolder`) and can be used in .NET _.aspx_ files for encapsulating 
a block of JavaScript.

For example

```HTML
<%@ Register TagPrefix="spx" Namespace="SuperScript.JavaScript.WebForms.Containers" Assembly="SuperScript.JavaScript.WebForms" %>
...
<spx:JavaScriptContainer runat="server" EmitterKey="javascript">

	<!-- The script tag is not required here, but it offers intellisense when used. -->
	<script type="text/javascript">
	
		function test(message, value) {
			console.log(message + " = " + value);
		}
		
	</script>
	
</spx:JavaScriptContainer>
```

The exposed properties are all contained on the [superclass](https://github.com/Supertext/SuperScript.Container.WebForms/blob/master/Container.cs) (_i.e._, `SuperScript.JavaScript.WebForms` does not add any properties).
* `AddLocationComments` [bool]

  Determines whether the emitted contents should contain comments indicating the original location when in debug-mode. The default value is `true`.

* `EmitterKey` [string]

  Indicates which instance of `IEmitter` the content should be added to. If not specified then the contents will be added to the default implementation of `IEmitter`.

* `InsertAt` [Nullable&lt;int&gt;]

  Gets or sets an index in the collection at which the contents are to be inserted.
  

##Dependencies
There are a variety of SuperScript projects, some being dependent upon others.

* [`SuperScript.Common`](https://github.com/Supertext/SuperScript.Common)

  This library contains the core classes which facilitate all other SuperScript modules but which won't produce any meaningful output on its own.

* [`SuperScript.Container.WebForms`](https://github.com/Supertext/SuperScript.Container.WebForms)

  This project does not wholly offer the functionality for working with, for example, JavaScript or HTML templates in _.aspx_ files. Rather, it offers the base functionality required by the related projects `SuperScript.JavaScript.Mvc` and `SuperScript.JavaScript.WebForms`.

* [`SuperScript.JavaScript`](https://github.com/Supertext/SuperScript.JavaScript)

  This library contains functionality for making JavaScript-specific declarations such as variables or function calls.

`SuperScript.JavaScript.WebForms` has been made available under the [MIT License](https://github.com/Supertext/SuperScript.JavaScript.WebForms/blob/master/LICENSE).
