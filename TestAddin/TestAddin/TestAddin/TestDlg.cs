using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAddin
{
   public partial class TestDlg : Form
   {
      public TestDlg()
      {
         InitializeComponent();
      }

      private void OnHelpRequested(object sender, HelpEventArgs hlpevent)
      {
         Help.ShowPopup(this, "test help", new Point(Top, Left));
      }
   }
}
