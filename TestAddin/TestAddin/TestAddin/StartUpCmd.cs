using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace TestAddin
{
   [Transaction(TransactionMode.Manual)]
   [Regeneration(RegenerationOption.Manual)]
   public class StartUpCmd : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         TestDlg testDlg = new TestDlg();
         testDlg.ShowDialog();

         return Result.Succeeded;
      }
   }
}
