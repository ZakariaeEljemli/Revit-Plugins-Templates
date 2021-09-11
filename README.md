<p align="center"><img width=12.5% src="https://user-images.githubusercontent.com/69751645/132955379-f4514a30-815f-4701-bc45-6a957f64cf14.png"></p>
<h1 align="center">
  
</h1>

<h4 align="center">A Revit Plugin Template to reuse it</h4>

# Overview

This depository gives you some templates to reuse for your development within Revit using the API and C# language. Those templates can be used in all versions of Revit.

<p align="center"><img width=50% src="https://user-images.githubusercontent.com/69751645/132955771-0507fa50-bca2-48bb-a1d0-bdc520e46427.PNG"></p>


# Prerequisite

* Please install Visual Studio 2019 and Orca MSI Editor [https://visualstudio.microsoft.com/fr/vs/community/] [https://www.technipages.com/download-orca-msi-editor]
For comprehensive documentation on Visual Studio 2019, please see check out the link [https://docs.microsoft.com/fr-fr/visualstudio/windows/?view=vs-2019].

* Please if you want to contribute to the enhancement of this repository, follow these steps :
```bash
git clone https://github.com/ZakariaeEljemli/Revit-Plugins-Templates.git
cd Revit-Plugins-Templates
```
First, make sure to cd into your local repository. Once you’re in the right folder, execute
```bash
git branch <branch-name>
```
This will create a new branch. But before you start making changes to your code, you will have to first switch to the new branch you just created. To do that, run
```bash
git checkout <branch-name>
```
Many developers, especially when they’re just getting started, forget switching to the new branch. That’s why you can use this command that will create the new branch and immediately switch you to it:
```bash
git checkout -b <branch-name>
```
Once you’ve created a new branch and switched to it, you can start making changes in your code. However, all of these changes (including the new branch) is still only in your local machine.
To publish the new branch you created in GitHub and make it available for everyone in your team, run the following command:
```bash
git push -u <remote> <branch-name>
 ```
For more insights on Git Bash Command lines, check out the documentation : https://zepel.io/blog/how-to-create-a-new-branch-in-github/

# Preparation of the MSI/EXE file for your addin

This video gives the steps to follow in order to create your own executable file that will be installed as an addin within Revit :

[![IMAGE ALT TEXT HERE](https://user-images.githubusercontent.com/69751645/132956476-a802c6e7-de27-4eea-ac8d-0844e89abc9f.PNG)
](https://www.youtube.com/watch?v=W3NJR2BF2ds)

# Installation of the plugin template on Revit

<p align="center"><img width=50% src="https://user-images.githubusercontent.com/69751645/132955902-8f2b8ff3-3d2c-4d68-a047-b89f3529683b.PNG"></p>

Check out the exe file available in the debug folder : ./Revit-Plugins-Templates/TestAddinApp/TestAddinApp/Debug/TestAddinApp

# License (WIP)

This project is licensed under the [...] - see the [...](LICENSE) file for details

# Contact information

This software is an open-source project mostly maintained by myself, Zakariae ELJEMLI. If you have any questions or request, feel free to contact me at [zack.eljemli@gmail.com](mailto:zack.eljemli@gmail.com).
