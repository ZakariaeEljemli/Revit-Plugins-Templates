<p align="center"><img width=30% src="https://thumbs.dreamstime.com/z/les-hommes-d-affaires-et-caract%C3%A8res-minuscules-sur-la-pile-%C3%A9norme-des-fichiers-de-documentation-documents-papier-effort-occup%C3%A9-186942564.jpg"></p>

<h3 align="center">Documentation</h3>

 Please refer to the autodesk forum documentation for more details : https://knowledge.autodesk.com/support/revit

# Ribbon Panels and Controls
Revit provides API solutions to integrate custom ribbon panels and controls. These APIs are used with IExternalApplication. 
Custom ribbon panels can be added to the Add-Ins tab, the Analyze tab or to a new custom ribbon tab.
Panels can include buttons, both large and small, which can be either simple push buttons, pulldown buttons containing multiple commands, 
or split buttons which are pulldown buttons with a default push button attached. 
In addition to buttons, panels can include radio groups, combo boxes and text boxes. Panels can also include vertical 
separators to help separate commands into logical groups. 
Finally, panels can include a slide out control accessed by clicking on the bottom of the panel.
<p align="center"><img width=70% src="https://user-images.githubusercontent.com/69751645/132957209-912447e0-8e5b-4e09-bec1-a0490a059fd7.PNG"></p>

<h2 align="center">Create a New Ribbon Tab</h2>
Although ribbon panels can be added to the Add-Ins or Analyze tab, they can also be added to a new custom ribbon tab. This option should only be used if necessary. To ensure that the standard Revit ribbon tabs remain visible, a limit of 20 custom ribbon tabs is imposed.
The following image shows a new ribbon tab with one ribbon panel and a few simple controls.

```csharp
publicResult OnStartup(UIControlledApplication application)
{
// add a new panel to Revit Toolbar
                application.CreateRibbonTab("Help Test");
                
                // add a new ribbon associated to this panel
                RibbonPanel ArchiPanel = application.CreateRibbonPanel("Help Test", "Tester le help");

                // Create parameters associated to the button
                string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

                // Associated the call method when the button is triggered
                PushButtonData buttonData1 = new PushButtonData("Archi",
                   "Bouton Simple", thisAssemblyPath, "TestAddin.VCTCommand");

                // Create the button and associate it to the ribbon
                PushButton pushButton = ArchiPanel.AddItem(buttonData1) as PushButton;
                returnResult.Succeeded;
               }
```

<h2 align="center">Tooltips</h2>
Most controls can have a tooltip set (using the ToolTip property) which is displayed when the user moves the mouse over the control. When the user hovers the mouse over a control for an extended period of time, an extended tooltip will be displayed using the LongDescription and the ToolTipImage properties. If neither LongDescription nor ToolTipImage are set, the extended tooltip is not displayed. If no ToolTip is provided, then the text of the control (RibbonItem.ItemText) is displayed when the mouse moves over the control.

```csharp
pushButton.ToolTip = "It's the message that appears when the button is triggered !";
```

<h2 align="center">Contextual Help</h2>

Controls can have contextual help associated with them. When the user hovers the mouse over the control and hits F1, the contextual help is triggered. Contextual help options include linking to an external URL, launching a locally installed help (chm) file, or linking to a topic on the Autodesk help wiki. The ContextualHelp class is used to create a type of contextual help, and then RibbonItem.SetContextualHelp() (or RibbonItemData.SetContextualHelp()) associates it with a control. When a ContextualHelp instance is associated with a control, the text "Press F1 for more help" will appear below the tooltip when the mouse hovers over the control, as shown below.
The following example associates a new ContextualHelp with a push button control. Pressing F1 when hovered over the push button will open the Autodesk homepage in a new browser window.

```csharp
ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url,"http://www.google.fr");
pushButton.SetContextualHelp(contextHelp);
```

<h2 align="center">Ribbon Controls</h2>

<h3 align="left">Push Buttons</h3>
There are three types of buttons you can add to a panel: simple push buttons, drop-down buttons, and split buttons. The HelloWorld button in Figure 14 is a push button. When the button is pressed, the corresponding command is triggered.
In addition to the Enabled property, PushButton has the AvailabilityClassName property which can be used to set the name of an IExternalCommandAvailability interface that controls when the command is available.

```csharp
private void AddPushButton(RibbonPanel panel)
{
        // Create parameters associated to the button
              string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

        // Associated the call method when the button is triggered
              PushButtonData buttonData = new PushButtonData("Archi","Bouton Simple", thisAssemblyPath, "TestAddin.VCTCommand");

         // Create the button and associate it to the ribbon
               PushButton pushButton = panel.AddItem(buttonData) as PushButton;
         
         //Add an image to the button
               pushButton.LargeImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud32-32.png");
         //Add a separator between this button and the one that follows
               panel.AddSeparator();
}
```

<h3 align="left">Drop-down buttons</h3>
Drop-down buttons expand to display two or more commands in a drop-down menu. In the Revit API, drop-down buttons are referred to as PulldownButtons. Horizontal separators can be added between items in the drop-down menu.
Each command in a drop-down menu can also have an associated LargeImage as shown in the example above.

<h3 align="left">Split buttons</h3>
Split buttons are drop-down buttons with a default push button attached. The top half of the button works like a push button while the bottom half functions as a drop-down button. The image below shows a split button :
<p align="center"><img width=70% src="https://user-images.githubusercontent.com/69751645/133909047-d1225575-4809-4d12-80f7-1783e99084d1.png"></p>

Initially, the push button will be the top item in the drop-down list. However, by using the IsSynchronizedWithCurrentItem property, the default command (which is displayed as the push button top half of the split button) can be synchronized with the last used command. By default it will be synched. 

```csharp
PushButtonData buttonData2 = new PushButtonData("Option1", "Option A", thisAssemblyPath, "TestAddin.VCTCommand");
PushButtonData buttonData3 = new PushButtonData("Option2","Option B", thisAssemblyPath, "TestAddin.VCTCommand");           
buttonData2.LargeImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud32-32.png"); ;
buttonData3.LargeImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud32-32.png"); ;
buttonData2.ToolTip = "Option 1";
buttonData3.ToolTip = "Option 2";
buttonData2.SetContextualHelp(contextHelp);
buttonData3.SetContextualHelp(contextHelp);
SplitButtonData sb1 = new SplitButtonData("splitButton1", "Split");
SplitButton sb = ArchiPanel.AddItem(sb1) as SplitButton;
sb.AddPushButton(buttonData2);
sb.AddSeparator();
sb.AddPushButton(buttonData3);
```

<h3 align="left">Text box</h3>

A text box is an input control for users to enter text. The image for a text box can be used as a clickable button by setting the ShowImageAsButton property to true. The default is false. The image is displayed to the left of the text box when ShowImageAsButton is false, and displayed at the right end of the text box when it acts as a button.

The text entered in the text box is only accepted if the user hits the Enter key or if they click the associated image when the image is shown as a button. Otherwise, the text will revert to its previous value.
<p align="center"><img width=70% src="https://user-images.githubusercontent.com/69751645/133909019-523c6688-a10a-47fa-97b6-1dbefc0f67a5.PNG"></p>

In addition to providing a tooltip for a text box, the PromptText property can be used to indicate to the user what type of information to enter in the text box. Prompt text is displayed when the text box is empty and does not have keyboard focus. This text is displayed in italics. The text box in the Figure has the prompt text "Enter a comment".

The width of the text box can be set using the Width property. The default is 200 device-independent units.

The TextBox.EnterPressed event is triggered when the user presses enter, or when they click on the associated image for the text box when ShowImageAsButton is set to true. When implementing an EnterPressed event handler, cast the sender object to TextBox to get the value the user has entered as shown in the following example.

```csharp
TextBoxData textData = new TextBoxData("Text Box");
textData.Image = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");
textData.Name = "Text Box";
textData.ToolTip = "Enter some text here";
textData.LongDescription = "This is text that will appear next to the image"+ "when the user hovers the mouse over the control";
textData.ToolTipImage = GetResourceImage(Assembly.GetExecutingAssembly(), "TestAddin.Resources.cloud16-16.png");
```

The inherited ItemText property has no effect for TextBox. The user-entered text can be obtained from the Value property, which must be converted to a string.

Refer to the Stacked Panel Items to see an example of adding a TextBox to a ribbon panel, including how to register the event above.

<h3 align="left">Combo box</h3>

A combo box is a pulldown with a set of selectable items. After adding a ComboBox to a panel, use the AddItem() or AddItems() methods to add ComboBoxMembers to the list.

Separators can also be added to separate items in the list or members can be optionally grouped using the ComboBoxMember.GroupName property. All members with the same GroupName will be grouped together with a header that shows the group name. Any items not assigned a GroupName will be placed at the top of the list. Note that when grouping items, separators should not be used as they will be placed at the end of the group rather than in the order they are added.

ComboBox has three events:
- CurrentChanged - triggered when the current item of the ComboBox is changed
- DropDownClosed - triggered when the drop-down of the ComboBox is closed
- DropDownClosed - triggered when the drop-down of the ComboBox is opened

```csharp
ComboBoxData cbData = new ComboBoxData("comboBox");
```
Refer to the Stacked Panel Items to see an example of adding a ComboBox to a ribbon panel.


<h3 align="left">Stacked Panel Items</h3>

To conserve panel space, you can add small panel items in stacks of two or three. Each item in the stack can be a push button, a drop-down button, a combo box or a text box. Radio button groups and split buttons cannot be stacked. Stacked buttons should have an image associated through their Image property, rather than LargeImage. A 16×16 image is ideal for small stacked buttons.
<p align="center"><img width=70% src="https://user-images.githubusercontent.com/69751645/133909039-a486776c-b8b4-41c3-8e4d-f7c2136e9b74.png"></p>

```csharp
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
```

<h3 align="left">Dockable Panel</h3>

[A WIP]


