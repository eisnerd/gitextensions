﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GitCommands;

namespace GitUI
{
    public partial class FormCherryPickCommitSmall : GitExtensionsForm
    {
        public FormCherryPickCommitSmall(GitRevision Revision)
        {
            this.Revision = Revision;

            InitializeComponent(); Translate();
            this.AutoCommit.Checked = true;
            this.Pick.Focus();
        }

        public GitRevision Revision { get; set; }

        private void FormCherryPickCommitSmall_Load(object sender, EventArgs e)
        {
            Commit.Text = string.Format("Commit: {0}", Revision.Guid);
            Author.Text = string.Format("Author: {0}", Revision.Author);
            Date.Text = string.Format("Commit date: {0}", Revision.CommitDate);
            Message.Text = string.Format("Message: {0}", Revision.Message);
        }

        private void Revert_Click(object sender, EventArgs e)
        {
            new FormProcess(GitCommands.GitCommands.CherryPickCmd(Revision.Guid, AutoCommit.Checked)).ShowDialog();

            MergeConflictHandler.HandleMergeConflicts();

            Close();
        }
    }
}
