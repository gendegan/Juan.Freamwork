using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using EnvDTE80;

namespace JuanGenerate
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideKeyBindingTable(GuidList.guidJuanGenerateEditorFactoryString, 102)]
    [Guid(GuidList.guidJuanGeneratePkgString)]

    //[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed partial class JuanGeneratePackage : Package
    {
        /// <summary>
        /// JuanGeneratePackage GUID string.
        /// </summary>
        public const string PackageGuidString = "d429b5bd-c6d9-4368-bb77-370c4188e232";

        /// <summary>
        /// Initializes a new instance of the <see cref="JuanGeneratePackage"/> class.
        /// </summary>
        public JuanGeneratePackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        #region Package Members
        public static DTE2 ApplicationObject;
        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override void Initialize()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();


            // Add our command handlers for menu (commands must exist in the .vsct file)
            ApplicationObject = (DTE2)GetService(typeof(DTE));
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID ConnectConfigCommandID = new CommandID(GuidList.guidJuanGenerateCmdSet, (int)PkgCmdIDList.cmdidConnectConfigCommand);
                MenuCommand ConnectConfigMenu = new MenuCommand(ConnectConfigHandler, ConnectConfigCommandID);
                mcs.AddCommand(ConnectConfigMenu);

                CommandID ContextCommandID = new CommandID(GuidList.guidJuanGenerateCmdSet, (int)PkgCmdIDList.cmdidContextCommand);
                MenuCommand ContextMenu = new MenuCommand(ContextHandler, ContextCommandID);
                mcs.AddCommand(ContextMenu);
                CommandID UpgradeCommandID = new CommandID(GuidList.guidJuanGenerateCmdSet, (int)PkgCmdIDList.cmdidUpgradeCommand);
                MenuCommand UpgradeMenu = new MenuCommand(UpgradeContextHandler, UpgradeCommandID);
                mcs.AddCommand(UpgradeMenu);



                CommandID UpdateCodeCommandID = new CommandID(GuidList.guidJuanGenerateCmdSet, (int)PkgCmdIDList.cmdidUpdateCodeCommand);
                MenuCommand UpdateCodeMenu = new MenuCommand(UpdateContextHandler, UpdateCodeCommandID);
                mcs.AddCommand(UpdateCodeMenu);
            }
        }

        #endregion
    }
}
