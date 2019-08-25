using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace cloudscribeTemplate
{
    public class ProjectWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            
        }

        public void ProjectFinishedGenerating(Project project)
        {
           
            
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            
        }

        public void RunFinished()
        {
            // project hasn't actually been created here because the main
            // template wizard runs after this wizard replaces values in the dictionary
            // therefore added ShowReadMeWizard after that wizard
            
        }

        private DTE _dte;
        private string _projectDirectory = "";
        //private UserInputForm _inputForm;
        private string _dataStorage = "MSSQL";
        private string _simpleContentOption = "a";
        private string _multiTenantMode = "FolderName";
        private bool _useLogging = true;
        private bool _includeDynamicPolicy = true;
        //private bool _useSimpleContent = true;
        private bool _useContactForm = false;
        private bool _useKvpProfile = false;
        private bool _useIdentityServer = false;
        private string _nonRootPagesSegment = "p";
        private string _nonRootPagesTitle = "Articles";

        private bool _includeWebpack = false;
        private bool _includeReactSample = false;


        private bool _includeFormBuilder = false;
        private bool _includeNewsletter = false;
        private bool _includePaywall = false;

        private bool _includeCommentSystem = false;
        private bool _includeForum = false;


        private bool _exceptionOccurred = false;
        private ProjectOptionsDialog _dialog;

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            _dte = (DTE)automationObject; 
            _projectDirectory = replacementsDictionary["$destinationdirectory$"];

            try
            {
                _dialog = new ProjectOptionsDialog();
                var btn = _dialog.Controls["btnOk"] as Button;
                btn.Click += btnOk_Click;
                _dialog.ShowDialog();
                
                replacementsDictionary.Add("passthrough:DataStorage", _dataStorage);
                replacementsDictionary.Add("passthrough:MultiTenantMode", _multiTenantMode);
                replacementsDictionary.Add("passthrough:Logging", _useLogging.ToString().ToLowerInvariant());
                replacementsDictionary.Add("passthrough:SimpleContentConfig", _simpleContentOption);
                replacementsDictionary.Add("passthrough:ContactForm", _useContactForm.ToString().ToLowerInvariant());
                replacementsDictionary.Add("passthrough:KvpCustomRegistration", _useKvpProfile.ToString().ToLowerInvariant());
                replacementsDictionary.Add("passthrough:IdentityServer", _useIdentityServer.ToString().ToLowerInvariant());
                if(_simpleContentOption == "b")
                {
                    replacementsDictionary.Add("passthrough:NonRootPagesSegment", _nonRootPagesSegment.ToLowerInvariant());
                    replacementsDictionary.Add("passthrough:NonRootPagesTitle", _nonRootPagesTitle);
                }

                replacementsDictionary.Add("passthrough:Webpack", _includeWebpack.ToString().ToLowerInvariant());
                replacementsDictionary.Add("passthrough:ReactSample", _includeReactSample.ToString().ToLowerInvariant());
                replacementsDictionary.Add("passthrough:DynamicPolicy", _includeDynamicPolicy.ToString().ToLowerInvariant());

                //replacementsDictionary.Add("passthrough:FormBuilder", _includeFormBuilder.ToString().ToLowerInvariant());
                //replacementsDictionary.Add("passthrough:Newsletter", _includeNewsletter.ToString().ToLowerInvariant());
                //replacementsDictionary.Add("passthrough:Paywall", _includePaywall.ToString().ToLowerInvariant());


                //replacementsDictionary.Add("passthrough:CommentSystem", _includeCommentSystem.ToString().ToLowerInvariant());
                //replacementsDictionary.Add("passthrough:Forum", _includeForum.ToString().ToLowerInvariant());


            }
            catch (Exception ex)
            {
                _exceptionOccurred = true;
                // Clean up the template that was written to disk
                if (Directory.Exists(_projectDirectory))
                {
                    Directory.Delete(_projectDirectory, true);
                }

                MessageBox.Show(ex.ToString());
            }

        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(_dialog != null)
            {
                _dataStorage = (string)((ComboBox)_dialog.Controls["cbDataStorage"]).SelectedValue;
                _includeDynamicPolicy = ((CheckBox)_dialog.Controls["chkDynamicPolicy"]).Checked;
                _useLogging = ((CheckBox)_dialog.Controls["chkLogging"]).Checked;
                
                _useContactForm = ((CheckBox)_dialog.Controls["chkContactForm"]).Checked;
                _useKvpProfile = ((CheckBox)_dialog.Controls["chkKvpProfile"]).Checked;
                _useIdentityServer = ((CheckBox)_dialog.Controls["chkIdentityServer"]).Checked;

               
                var groupBox = _dialog.Controls["gbSimpleContentConfig"];

                var simpleContentOption = groupBox.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked == true);

                switch(simpleContentOption.Name)
                {
                    case "optionA":
                        _simpleContentOption = "a";
                        break;

                    case "optionB":
                        _simpleContentOption = "b";
                        break;

                    case "optionC":
                        _simpleContentOption = "c";
                        break;

                    case "optionD":
                        _simpleContentOption = "d";
                        break;

                    case "optionZ":
                        _simpleContentOption = "z";
                        break;
                }

                _multiTenantMode = (string)((ComboBox)_dialog.Controls["cbMultiTenancy"]).SelectedValue;

                var gbExpert = _dialog.Controls["gbExpert"];
                _includeWebpack = ((CheckBox)gbExpert.Controls["chkWebpack"]).Checked;
                _includeReactSample = ((CheckBox)gbExpert.Controls["chkReactSample"]).Checked;
                if (!_includeWebpack)
                {
                    _includeReactSample = false;
                }

                //var gbCommercial = _dialog.Controls["gbCommercial"];
                //_includeFormBuilder = ((CheckBox)gbCommercial.Controls["chkFormBuilder"]).Checked;
                //_includeNewsletter = ((CheckBox)gbCommercial.Controls["chkNewsletter"]).Checked;
                //_includePaywall = ((CheckBox)gbCommercial.Controls["chkPaywall"]).Checked;
                //_includeCommentSystem = ((CheckBox)gbCommercial.Controls["chkCommentSystem"]).Checked;
                //_includeForum = ((CheckBox)gbCommercial.Controls["chkForum"]).Checked;


                _dialog.Close();
            }
            
        }

    }

    

}
