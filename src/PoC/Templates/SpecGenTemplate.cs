﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace com.bjss.generator.Templates
{
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using Microsoft.CSharp;
    using System.Globalization;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class SpecGenTemplate : SpecGenTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing BddfySpecGen.ATM;\r\nusing Specify.Stories;\r\nusing TestStack.B" +
                    "DDfy;\r\n\r\nnamespace BddfySpecGen\r\n{\r\n    public class ");
            
            #line 18 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetFormatedName(this.Story.Title.Text, "")));
            
            #line default
            #line hidden
            this.Write(" : UserStory\r\n    {\r\n        public ");
            
            #line 20 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetFormatedName(this.Story.Title.Text, "")));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n            AsA = \"");
            
            #line 22 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Story.AsA.Text));
            
            #line default
            #line hidden
            this.Write("\";\r\n            IWant = \"");
            
            #line 23 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Story.IWant.Text));
            
            #line default
            #line hidden
            this.Write("\";\r\n            SoThat = \"");
            
            #line 24 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Story.SoThat.Text));
            
            #line default
            #line hidden
            this.Write("\";\r\n        }\r\n    }\r\n");
            
            #line 27 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"

    foreach (var scenario in this.Story.Scenarios)
    {

            
            #line default
            #line hidden
            this.Write("    public class ");
            
            #line 31 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetFormatedName(scenario.Title.Text, "")));
            
            #line default
            #line hidden
            this.Write(" : ScenarioFor<object,  ");
            
            #line 31 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetFormatedName(this.Story.Title.Text, "")));
            
            #line default
            #line hidden
            this.Write(">\r\n    {\r\n");
            
            #line 33 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"

    foreach (var step in scenario.Steps)
    {

            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 37 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetAttribute(step.Text)));
            
            #line default
            #line hidden
            this.Write("public void ");
            
            #line 37 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetFormatedName(step.Text, "_")));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n            throw new NotImplementedException();\r\n        }\r\n");
            
            #line 41 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
        
    }

            
            #line default
            #line hidden
            this.Write("    }\r\n\r\n");
            
            #line 46 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"
        
    }

            
            #line default
            #line hidden
            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 50 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"

	private string GetFormatedName(string name , string replaceSpacesWith)
    {
		TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        return GenerateClassName(textInfo.ToTitleCase(name), replaceSpacesWith);
    }

	private string GetAttribute(string text)
	{
	    string prefix = String.Empty;
        if (text.StartsWith("Given"))
        {
            prefix = "Given";
        }
        else if (text.StartsWith("When"))
        {
            prefix = "When";
        }
        else if (text.StartsWith("Then"))
        {
            prefix = "Then";
        }

	    return !string.IsNullOrEmpty(prefix)
	        ? string.Empty
	        : string.Format("[{0}(\"{1}\")]\r\n        ", prefix, text);
    }

	private static string GenerateClassName(string value, string replaceSpacesWith)
	{
		var className = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value).Replace("$", "Dollars ").Replace("£ ", "Pounds");
		var ret = className;
		bool isValid = Microsoft.CSharp.CSharpCodeProvider.CreateProvider("C#").IsValidIdentifier(className);

		if (!isValid)
		{ 
			// File name contains invalid chars, remove them
			Regex regex = new Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]");
			ret = regex.Replace(className, "");

			// Class name doesn't begin with a letter, insert an underscore
			if (!char.IsLetter(ret, 0))
			{
				ret = ret.Insert(0, "_");
			}
		}

		return ret.Replace(" ", replaceSpacesWith);
	}

        
        #line default
        #line hidden
        
        #line 1 "C:\Dev\GitHub\SpecGen\src\PoC\Templates\SpecGenTemplate.tt"

private global::com.bjss.generator.Model.StoryNode _StoryField;

/// <summary>
/// Access the Story parameter of the template.
/// </summary>
private global::com.bjss.generator.Model.StoryNode Story
{
    get
    {
        return this._StoryField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool StoryValueAcquired = false;
if (this.Session.ContainsKey("Story"))
{
    this._StoryField = ((global::com.bjss.generator.Model.StoryNode)(this.Session["Story"]));
    StoryValueAcquired = true;
}
if ((StoryValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Story");
    if ((data != null))
    {
        this._StoryField = ((global::com.bjss.generator.Model.StoryNode)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class SpecGenTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
