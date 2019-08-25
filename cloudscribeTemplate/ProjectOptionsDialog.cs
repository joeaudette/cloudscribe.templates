using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cloudscribeTemplate
{
    public partial class ProjectOptionsDialog : Form
    {
        public ProjectOptionsDialog()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            var cbDataStorage = this.Controls["cbDataStorage"] as ComboBox;
            cbDataStorage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDataStorage.DataSource = new ComboItem[] {
                new ComboItem{ Key = "NoDb", Text = "Use NoDb (no database) file system storage" },
                new ComboItem{ Key = "SQLite", Text = "Use SQLite" },
                new ComboItem{ Key = "MSSQL", Text = "Use Microsoft SqlServer" },
                new ComboItem{ Key = "MySql", Text = "Use MySql" },
                new ComboItem{ Key = "pgsql", Text = "Use PostgreSql" }
                
            };
            cbDataStorage.ValueMember = "Key";
            cbDataStorage.DisplayMember = "Text";
            cbDataStorage.SelectedValueChanged += CbDataStorage_SelectedValueChanged;

            var cbMultiTenancy = this.Controls["cbMultiTenancy"] as ComboBox;
            cbMultiTenancy.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMultiTenancy.DataSource = new ComboItem[] {
                new ComboItem{ Key = "FolderName", Text = "Use FolderName Multitenancy" },
                new ComboItem{ Key = "HostName", Text = "Use HostName Multitenancy - additional tenants require DNS and web server settings" },
                new ComboItem{ Key = "None", Text = "Use a single tenant/site" }
            };
            cbMultiTenancy.ValueMember = "Key";
            cbMultiTenancy.DisplayMember = "Text";

            chkDynamicPolicy.Checked = true;
            chkLogging.Checked = true;

            //chkPaywall.Enabled = false;
            //chkNewsletter.Enabled = false;

            txtNonRootPagesSegment.CharacterCasing = CharacterCasing.Lower;
            txtNonRootPagesSegment.MaxLength = 30;
            txtNonRootPagesSegment.KeyPress += TxtNonRootPagesSegment_KeyPress;
            txtNonRootPagesSegment.TextChanged += TxtNonRootPagesSegment_TextChanged;

            txtNonRootPagesTitle.MaxLength = 50;
            txtNonRootPagesTitle.KeyPress += TxtNonRootPagesTitle_KeyPress;
            txtNonRootPagesTitle.TextChanged += TxtNonRootPagesTitle_TextChanged;

            //LinkLabel.Link chatLink = new LinkLabel.Link();
            //chatLink.LinkData = "https://www.cloudscribe.com/forum?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            //lnkChat.Links.Add(chatLink);

            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://www.cloudscribe.com/docs/advanced-client-side-development-with-webpack?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            lnkWebpack.Links.Add(link);


            //LinkLabel.Link formsLink = new LinkLabel.Link();
            //formsLink.LinkData = "https://www.cloudscribe.com/products/cloudscribe-forms-and-surveys-solution?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            //lnkFormProduct.Links.Add(formsLink);


            //LinkLabel.Link newsLetterLink = new LinkLabel.Link();
            //newsLetterLink.LinkData = "https://www.cloudscribe.com/products/cloudscribe-newsletter-solution?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            //lnkNewsletter.Links.Add(newsLetterLink);


            //LinkLabel.Link paywallLink = new LinkLabel.Link();
            //paywallLink.LinkData = "https://www.cloudscribe.com/products/cloudscribe-membership-paywall?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            //lnkPaywall.Links.Add(paywallLink);


            //LinkLabel.Link stripeLink = new LinkLabel.Link();
            //stripeLink.LinkData = "https://www.cloudscribe.com/products/cloudscribe-stripe-integration?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            //lnkStripeIntegration.Links.Add(stripeLink);


            //LinkLabel.Link commentSystemLink = new LinkLabel.Link();
            //commentSystemLink.LinkData = "https://www.cloudscribe.com/products/cloudscribe-talkabout-comment-system?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            //lnkCommentSystem.Links.Add(commentSystemLink);

            //LinkLabel.Link forumLink = new LinkLabel.Link();
            //forumLink.LinkData = "https://www.cloudscribe.com/products/cloudscribe-talkabout-forums?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            //lnkForum.Links.Add(forumLink);

            LinkLabel.Link dynAuthLink = new LinkLabel.Link();
            dynAuthLink.LinkData = "https://www.cloudscribe.com/dynamic-authorization-policies?utm_source=projecttemplate&utm_medium=referral&utm_campaign=newproject-vsix";
            lnkDynamicAuth.Links.Add(dynAuthLink);

        }

        private void CbDataStorage_SelectedValueChanged(object sender, EventArgs e)
        {
            //var db = (string)cbDataStorage.SelectedValue;
            //if(db == "NoDb" || db == "SQLite")
            //{
            //    chkPaywall.Enabled = false;
            //    chkPaywall.Checked = false;
            //    chkNewsletter.Enabled = false;
            //    chkNewsletter.Checked = false;
            //}
            //else
            //{
            //    chkPaywall.Enabled = true;
            //    chkNewsletter.Enabled = true;
               
            //}
        }

        private void lnkWebpack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void TxtNonRootPagesTitle_TextChanged(object sender, EventArgs e)
        {
            //in case text is pasted in instead of keyed
            var currentValue = txtNonRootPagesSegment.Text;
            var filtered = new string(currentValue.Where(c => Char.IsLetter(c)).ToArray());
            if (currentValue != filtered)
            {
                txtNonRootPagesSegment.Text = filtered;
            }
        }

        private void TxtNonRootPagesTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only aloow letters
            if (e.KeyChar < 65 || e.KeyChar > 122)
            {
                e.Handled = true;
            }
        }

        private void TxtNonRootPagesSegment_TextChanged(object sender, EventArgs e)
        {
            //in case text is pasted in instead of keyed
            var currentValue = txtNonRootPagesSegment.Text;
            var filtered = new string(currentValue.Where(c => Char.IsLetter(c)).ToArray());
            if(currentValue != filtered)
            {
                txtNonRootPagesSegment.Text = filtered;
            }
            
        }

        private void TxtNonRootPagesSegment_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only aloow letters
            if (e.KeyChar < 65 || e.KeyChar > 122)
            {
                e.Handled = true;
            }
        }

        private void optionB_CheckedChanged(object sender, EventArgs e)
        {
            txtNonRootPagesSegment.Enabled = optionB.Checked;
            txtNonRootPagesTitle.Enabled = optionB.Checked;
        }

        private void chkWebpack_CheckedChanged(object sender, EventArgs e)
        {
            if(!chkWebpack.Checked)
            {
                chkReactSample.Checked = false;
            }
        }

        private void chkReactSample_CheckedChanged(object sender, EventArgs e)
        {
            if(chkReactSample.Checked)
            {
                chkWebpack.Checked = true;
            }
        }

        private void lnkFormProduct_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void lnkNewsletter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void lnkPaywall_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void lnkStripeIntegration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        //private void lnkDynamicPolicy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    Process.Start(e.Link.LinkData as string);
        //}

        private void lnkChat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lnkCommentSystem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void lnkForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void lnkDynamicAuth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }



    class ComboItem
    {
        public string Key { get; set; }
        public string Text { get; set; }
    }
}
