﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GitUI;

namespace Gerrit
{
    public partial class FormGerritChangeSubmitted : GitExtensionsForm
    {
        public FormGerritChangeSubmitted()
        {
            InitializeComponent();
            Translate();
        }

        public static void ShowSubmitted(IWin32Window owner, string change)
        {
            var form = new FormGerritChangeSubmitted();

            form._NO_TRANSLATE_TargetLabel.Text = change;
            form._NO_TRANSLATE_TargetLabel.Click += (s, e) => Process.Start(change);

            form.ShowDialog(owner);
        }
    }
}
