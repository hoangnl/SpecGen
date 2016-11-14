using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xml;
using com.bjss.generator.Model;
using com.bjss.generator.Templates;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ApplySpecGenSyntaxHighlightingToEditor(UserStoryEntryBox);
            GeneratedOutput.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
        }

        private void ApplySpecGenSyntaxHighlightingToEditor(TextEditor editor)
        {

            try
            {
                var filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Syntaxs\SpecGen.xshd");
                using (var reader = new XmlTextReader(filepath))
                {
                    editor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            catch (Exception)
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GeneratedOutput.Document == null)
            {
                GeneratedOutput.Document = new TextDocument();
            }
            try
            {
                var doc = new StoryDocument(UserStoryEntryBox.Text);
                if (doc.Errors.Any())
                {
                    var sb = new StringBuilder();
                    foreach (var docError in doc.Errors)
                    {
                        sb.Append($"Line: {docError.Location.Line}, {docError.Severity} - {docError.Message}");
                    }

                    GeneratedOutput.Document.Text = sb.ToString();
                    return;
                }

                var ttTemplate = new SpecGenTemplate
                {
                    Session = new Dictionary<string, object> {{"Story", doc.Story}}
                };

                ttTemplate.Initialize();

                GeneratedOutput.Document.Text = ttTemplate.TransformText();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
