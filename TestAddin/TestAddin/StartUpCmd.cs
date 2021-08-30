using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System.Windows.Media.Imaging;
using System.Windows.Media;



namespace TestAddin
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class StartUpCmd : IExternalApplication
    {
        // Méthodes obligatoires pour création d'un plugin
        static void ProcessText(object sender, Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs args)
        {
            // cast sender as TextBox to retrieve text value
            TextBox textBox = sender as TextBox;
            string strText = textBox.Value as string;
            TaskDialog.Show("Msg", strText);
        }

        //transform an image to a bitmap (a reusable function afterwards)
        static public ImageSource GetResourceImage(Assembly assembly, string imageName)
        {
            try
            {
                // bitmap stream to construct bitmap frame
                Stream resource = assembly.GetManifestResourceStream(imageName);
                // return image data
                return BitmapFrame.Create(resource);
            }
            catch
            {
                return null;
            }
        }
        static void AddRibbonPanel(UIControlledApplication application)
        {
            ///Créer un bouton simple avec un F1 Help
                // Ajoute un nouvel onglet à Revit
                application.CreateRibbonTab("Help Test");

                // Ajoute un ruban associé à l'onglet
                RibbonPanel ArchiPanel = application.CreateRibbonPanel("Help Test", "Tester le help");

                // Création des paramètres du bouton associé aux différentes macros
                string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

                // associé à "VCTCommand" --> La méthode a appelé lors du click bouton
                PushButtonData buttonData1 = new PushButtonData("Archi",
                   "Bouton Simple", thisAssemblyPath, "TestAddin.VCTCommand");


                // On crée le bouton et on l'ajout au ruban
                PushButton pushButton = ArchiPanel.AddItem(buttonData1) as PushButton;

                // On définit le texte affiché au survol
                pushButton.ToolTip = "Bonjour notre Archi !";

                // Ajouter une zone d'aide qui incite l'envoi vers un site externe

                ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url,"http://www.google.fr");
                pushButton.SetContextualHelp(contextHelp);

            // Icone du bouton
    
            pushButton.LargeImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud32-32.png");
            ArchiPanel.AddSeparator();

            ///Créer un menu de boutons avec un F1 Help
                // associé à "VCTCommand" --> La méthode a appelé lors du click bouton
                PushButtonData buttonData2 = new PushButtonData("Option1",
                   "Option A", thisAssemblyPath, "TestAddin.VCTCommand");
                PushButtonData buttonData3 = new PushButtonData("Option2",
               "Option B", thisAssemblyPath, "TestAddin.VCTCommand");

                // Icone du bouton 2
                buttonData2.LargeImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud32-32.png"); ;

                // Icone du bouton 3
                buttonData3.LargeImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud32-32.png"); ;

                buttonData2.ToolTip = "Option 1";
                buttonData3.ToolTip = "Option 2";
                buttonData2.SetContextualHelp(contextHelp);
                buttonData3.SetContextualHelp(contextHelp);

                //Créer un menu de boutons
                SplitButtonData sb1 = new SplitButtonData("splitButton1", "Split");
                SplitButton sb = ArchiPanel.AddItem(sb1) as SplitButton;
                sb.AddPushButton(buttonData2);
                sb.AddSeparator();
                sb.AddPushButton(buttonData3);
            ArchiPanel.AddSeparator();
            ///Créer des boutons Stack : text + comboBox
            ComboBoxData cbData = new ComboBoxData("comboBox");

            TextBoxData textData = new TextBoxData("Text Box");
            textData.Image = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");
            textData.Name = "Text Box";
            textData.ToolTip = "Enter some text here";
            textData.LongDescription = "This is text that will appear next to the image"
                    + "when the user hovers the mouse over the control";
            textData.ToolTipImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");

            IList<RibbonItem> stackedItems = ArchiPanel.AddStackedItems(textData, cbData);
            if (stackedItems.Count > 1)
            {
                TextBox tBox = stackedItems[0] as TextBox;
                if (tBox != null)
                {
                    tBox.PromptText = "Enter a comment";
                    tBox.ShowImageAsButton = true;
                    tBox.ToolTip = "Enter some text";
                    // Register event handler ProcessText
                    tBox.EnterPressed += new EventHandler<Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs>(ProcessText);
                    
                }

                ComboBox cBox = stackedItems[1] as ComboBox;
                if (cBox != null)
                {
                    cBox.ItemText = "ComboBox";
                    cBox.ToolTip = "Select an Option";
                    cBox.LongDescription = "Select a number or letter";

                    ComboBoxMemberData cboxMemDataA = new ComboBoxMemberData("A", "Option A");
                    cboxMemDataA.Image =
                            GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");
                    cboxMemDataA.ToolTip = "Tester le bouton";
                    cboxMemDataA.SetContextualHelp(contextHelp);
                    cboxMemDataA.GroupName = "Letters";
                    cBox.AddItem(cboxMemDataA);

                    ComboBoxMemberData cboxMemDataB = new ComboBoxMemberData("B", "Option B");
                    cboxMemDataB.Image =
                            GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");
                    cboxMemDataB.GroupName = "Letters";
                    cboxMemDataB.ToolTip = "Tester le bouton";
                    cboxMemDataB.SetContextualHelp(contextHelp);
                    cBox.AddItem(cboxMemDataB);

                    ComboBoxMemberData cboxMemData = new ComboBoxMemberData("One", "Option 1");
                    cboxMemData.Image =
                            GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");
                    cboxMemData.GroupName = "Numbers";
                    cboxMemData.ToolTip = "Tester le bouton";
                    cboxMemData.SetContextualHelp(contextHelp);
                    cBox.AddItem(cboxMemData);
                    ComboBoxMemberData cboxMemData2 = new ComboBoxMemberData("Two", "Option 2");
                    cboxMemData2.Image =
                            GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");
                    cboxMemData2.GroupName = "Numbers";
                    cboxMemData2.ToolTip = "Tester le bouton";
                    cboxMemData2.SetContextualHelp(contextHelp);
                    cBox.AddItem(cboxMemData2);
                    ComboBoxMemberData cboxMemData3 = new ComboBoxMemberData("Three", "Option 3");
                    cboxMemData3.Image =
                            GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");
                    cboxMemData3.GroupName = "Numbers";
                    cboxMemData3.ToolTip = "Tester le bouton";
                    cboxMemData3.SetContextualHelp(contextHelp);
                    cBox.AddItem(cboxMemData3);                 
                    
                    
                }
            }
            ArchiPanel.AddSeparator();

            //Create the register and show buttons
            
            PushButton pushButton4 = ArchiPanel.AddItem(new PushButtonData("Register",
                   "registerPanel", thisAssemblyPath, "TestAddin.registerPanel")) as PushButton;
            //Accessibility check of the registration step
            pushButton4.AvailabilityClassName = "TestAddin.CommandAvailability";
            pushButton4.ToolTip = "Register dockable window at the zero document state.";
            pushButton4.LargeImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.register32.png");

            PushButton pushButton5 = ArchiPanel.AddItem(new PushButtonData("Show",
                   "showPanel", thisAssemblyPath, "TestAddin.showPanel")) as PushButton;
            pushButton5.ToolTip = "Show the registered dockable window.";
            pushButton5.LargeImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.show32.png");


        }
        
        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonPanel(application);
            // Obligatoire ici
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            // nothing to clean up in this simple case
            return Result.Succeeded;
        }

        
        

    }
    //Creata two classes : show (for showing the dockable panel) and register (to save changes in the panel)
    [Transaction(TransactionMode.Manual)]
    public class registerPanel : IExternalCommand
    {
        DockablePanel dockableWindow = null;
        ExternalCommandData eData = null;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DockablePanel dock = new DockablePanel();
            dockableWindow = dock;
            eData = commandData;
            DockablePaneId id = new DockablePaneId(new Guid("{5756Df00-16FB-40F0-B7CE-74CCB62C1AFF}"));
            try
            {
                // register dockable pane
                commandData.Application.RegisterDockablePane(id, "Test Dockable Panel",
                        dockableWindow as IDockablePaneProvider);
                TaskDialog.Show("Info Message", "Dockable window have been registered!");
                // subscribe document opened event
                commandData.Application.Application.DocumentOpened += new EventHandler<Autodesk.Revit.DB.Events.DocumentOpenedEventArgs>(Application_DocumentOpened);
                // subscribe view activated event
                commandData.Application.ViewActivated += new EventHandler<ViewActivatedEventArgs>(Application_ViewActivated);
                
            }
            catch (Exception ex)
            {
                // show error info dialog
                TaskDialog.Show("Info Message", ex.Message);
            }

            // return result
            return Result.Succeeded;

        }
        public void Application_ViewActivated(object sender, ViewActivatedEventArgs e)
        {
            // provide ExternalCommandData object to dockable page
            dockableWindow.CustomInitiator(eData);

        }
        // document opened event
        private void Application_DocumentOpened(object sender, Autodesk.Revit.DB.Events.DocumentOpenedEventArgs e)
        {
            // provide ExternalCommandData object to dockable page
            dockableWindow.CustomInitiator(eData);
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class showPanel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // dockable window id
                DockablePaneId id = new DockablePaneId(new Guid("{5756Df00-16FB-40F0-B7CE-74CCB62C1AFF}"));
                DockablePane dockableWindow = commandData.Application.GetDockablePane(id);
                dockableWindow.Show();
            }
            catch (Exception ex)
            {
                // show error info dialog
                TaskDialog.Show("Info Message", ex.Message);
            }
            // return result
            return Result.Succeeded;
        }
    }

    // external command availability to check the availability of a document
    public class CommandAvailability : IExternalCommandAvailability
    {
        // interface member method
        public bool IsCommandAvailable(UIApplication app, CategorySet cate)
        {
            // zero doc state
            if (app.ActiveUIDocument == null)
            {
                // disable register btn
                return true;
            }
            // enable register btn
            return false;
        }
    }

    // Début de la classe "VCT" associé au bouton précédent
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class VCTCommand : IExternalCommand
    {
        // Code exécuté en cas de clic
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {

            TaskDialog.Show("Msg", "En cours de construction");
            return Autodesk.Revit.UI.Result.Succeeded;
        }
        

    }
}