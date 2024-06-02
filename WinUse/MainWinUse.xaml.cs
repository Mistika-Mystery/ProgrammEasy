using ProgrammEasy.PageUse.Lesson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq;

namespace ProgrammEasy.WinUse
{
    /// <summary>
    /// Логика взаимодействия для MainWinUse.xaml
    /// </summary>
    public partial class MainWinUse : Window
    {

        private ScriptState<object> scriptState = null;

        public MainWinUse()
        {
            InitializeComponent();
        }

        private async void RunCodeButton_Click(object sender, RoutedEventArgs e)
        {
            var code = CodeInputTextBox.Text;
            try
            {
                if (scriptState == null)
                {
                    scriptState = await CSharpScript.RunAsync(code, ScriptOptions.Default.WithReferences(AppDomain.CurrentDomain.GetAssemblies()));
                }
                else
                {
                    scriptState = await scriptState.ContinueWithAsync(code);
                }

                var result = scriptState.ReturnValue;
                CodeOutputTextBox.Text = FormatResult(result);
            }
            catch (Exception ex)
            {
                CodeOutputTextBox.Text = $"Error: {ex.Message}";
            }
        }

        private string FormatResult(object result)
        {
            if (result == null)
            {
                return "No result";
            }
            else if (result is Array array)
            {
                return string.Join(" ", array.Cast<object>());
            }
            else
            {
                return result.ToString();
            }
        }
    }
}
