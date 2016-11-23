using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using com.bjss.generator.Model;
using com.bjss.generator.Templates;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MahApps.Metro.Controls;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private BackgroundWorker _backgroundWorker;

        public MainWindow()
        {
            InitializeComponent();

            ApplySpecGenSyntaxHighlightingToEditor(UserStoryEntryBox);
            GeneratedOutput.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
            GeneratedOutput.Document = new TextDocument();
            UsingsBlock.Text = string.Join("\n", StoryDocument.DefaultUsings);
            Namespace.Text = StoryDocument.DefaultNamespace;
            Target.Text = StoryDocument.DefaultTarget;
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
            KickOffBackgroundWorker();
        }

        private async void KickOffBackgroundWorker()
        {
            _backgroundWorker?.CancelAsync();

            while (_backgroundWorker?.CancellationPending ?? false)
            {
                await Task.Delay(500);
            }

            _backgroundWorker?.Dispose();

            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerCompleted;

            try
            {
                _backgroundWorker.RunWorkerAsync();
            }
            catch (Exception)
            {
            }
        }

        private void BackgroundWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_backgroundWorker == null)
            {
                return;
            }

            _backgroundWorker.DoWork -= BackgroundWorkerOnDoWork;
            _backgroundWorker.ProgressChanged -= BackgroundWorkerOnProgressChanged;
            _backgroundWorker.RunWorkerCompleted -= BackgroundWorkerCompleted;
            _backgroundWorker.Dispose();
            _backgroundWorker = null;
        }
        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            GeneratedOutput.Document.Text = (string)e.UserState;
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            var result = string.Empty;

            while (!worker.CancellationPending)
            {
                try
                {

                    var usings = string.Empty;
                    var namespaceText = string.Empty;
                    var target = string.Empty;
                    var userstory = string.Empty;

                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { usings = UsingsBlock.Text; });
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { namespaceText = Namespace.Text; });
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { target = Target.Text; });
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { userstory = UserStoryEntryBox.Text; });

                    var doc = new StoryDocument(userstory)
                    {
                        Namespace = namespaceText,
                        Target = target
                    };


                    foreach (var usingNs in usings.Split('\n'))
                    {
                        doc.Usings.Add(usingNs);
                    }

                    if (doc.Errors.Any())
                    {
                        var sb = new StringBuilder();
                        foreach (var docError in doc.Errors)
                        {
                            sb.Append($"Line: {docError.Location.Line}, {docError.Severity} - {docError.Message}");
                        }

                        result = sb.ToString();
                    }
                    else
                    {
                        var ttTemplate = new SpecGenTemplate
                        {
                            Session = new Dictionary<string, object>
                                            {
                                                { "Story", doc.Story },
                                                { "StoryDoc", doc }
                                            }
                        };

                        ttTemplate.Initialize();

                        result = ttTemplate.TransformText();
                    }

                }
                catch (Exception ex)
                {
                    result = ex.ToString();
                }


                //Do your stuff here
                worker.ReportProgress(100, result);
                worker.CancelAsync();
            }
            
            worker.Dispose();
        }

        private void UserStoryEntryBox_OnTextChanged(object sender, EventArgs e)
        {
            KickOffBackgroundWorker();
        }
    }
}
