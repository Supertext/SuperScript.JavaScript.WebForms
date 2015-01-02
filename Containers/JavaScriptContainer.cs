using System;
using System.IO;
using System.Web.UI;
using SuperScript.ExtensionMethods;
using SuperScript.JavaScript.Declarables;

namespace SuperScript.JavaScript.WebForms.Containers
{
	/// <summary>
	/// This control can be used to relocate and emit its contents in a common location.
	/// </summary>
    public class JavaScriptContainer : Container.WebForms.Container
	{
		protected override void __PreRender(object sender, EventArgs e)
		{
			// extract just the contents that we want to append
			var contents = GetContents();
			if (String.IsNullOrWhiteSpace(contents))
			{
				return;
			}

			var declaration = new CollectedScript
				                  {
					                  EmitterKey = EmitterKey,
					                  Value = contents
                                  };

            if (Configuration.Settings.Instance.AddLocationComments.IsCurrentlyEmittable())
            {
                declaration.WrapInLocationComment(GetFileName());
            }

			SuperScript.Declarations.AddDeclaration<CollectedScript>(declaration, InsertAt);

			// now remove the contained JavaScript from its design-time location
			Controls.Clear();
		}


		/// <summary>
		/// Obtains the JavaScript contents that have been passed into this <see cref="JavaScriptContainer" /> control.
		/// </summary>
		/// <returns>A string containing the JavaScript that was declared inside the current instance of this control.</returns>
		protected override string GetContents()
		{
			using (var stringWriter = new StringWriter())
			using (var writer = new HtmlTextWriter(stringWriter))
			{
				base.Render(writer);

				writer.Flush();

				// this StringBuilder contains the text we wish to write into the InjectInto control
			    return InternalLogic.EnsureStripppedContents(stringWriter.GetStringBuilder().ToString()).ToString();
			}
		}
	}
}