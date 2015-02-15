using System;
using System.IO;
using System.Text;
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
        #region Global Constants and Variables

        private const string ScriptNewLine = "\r\n";
        private const string ScriptTab = "\t";

        #endregion


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
	    /// Generates a JavaScript multi-line comment block containing a highlighted comment.
	    /// </summary>
	    /// <param name="comment">The comment which should appear highlighted inside the multi-line comment.</param>
	    /// <param name="startOnNewLine">Indicates whether a new line should be added before the start of the comment block.</param>
	    /// <returns>A string containing the specified comment inside a multi-line JavaScript comment block.</returns>
	    protected override string GenerateComment(string comment, bool startOnNewLine = true)
	    {
	        var messageLength = comment.Length + 2;
	        var padding = new StringBuilder(messageLength);
	        for (var i = 0; i < messageLength; i++)
	        {
	            padding.Append("*");
	        }

	        var startLine = string.Empty;
	        if (startOnNewLine)
	        {
	            startLine = string.Format("{0}{1}{1}{1}",
	                                      ScriptNewLine,
	                                      ScriptTab);
	        }
	        return string.Format("{0}/*{3}*/{1}{2}{2}{2}/* {4} */{1}{2}{2}{2}/*{3}*/{1}{2}{2}{2}",
	                             startLine,
	                             ScriptNewLine,
	                             ScriptTab,
	                             padding,
	                             comment);
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