using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Security;
using System.Runtime.InteropServices.ComTypes;

namespace Juan.Core
{
    [SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        public static Guid IID_IViewObject = new Guid("{0000010d-0000-0000-C000-000000000046}");

        [ComImport, Guid("0000010d-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IViewObject
        {
            [PreserveSig]
            int Draw([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, [In] NativeMethods.COMRECT lprcBounds, [In] NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, [In] int dwContinue);
            [PreserveSig]
            int GetColorSet([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, [Out] NativeMethods.tagLOGPALETTE ppColorSet);
            [PreserveSig]
            int Freeze([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);
            [PreserveSig]
            int Unfreeze([In, MarshalAs(UnmanagedType.U4)] int dwFreeze);
            void SetAdvise([In, MarshalAs(UnmanagedType.U4)] int aspects, [In, MarshalAs(UnmanagedType.U4)] int advf, [In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink);
            void GetAdvise([In, Out, MarshalAs(UnmanagedType.LPArray)] int[] paspects, [In, Out, MarshalAs(UnmanagedType.LPArray)] int[] advf, [In, Out, MarshalAs(UnmanagedType.LPArray)] IAdviseSink[] pAdvSink);
        }
    }

    /// <summary>
    /// Platform Invocation methods used to support Tracer.
    /// </summary>
    public sealed class NativeMethods
    {
        private NativeMethods()
        {
        }

        // Constants for use with GetSecurityInfo
        internal const uint OWNER_SECURITY_INFORMATION = 0x00000001;
        internal const uint GROUP_SECURITY_INFORMATION = 0x00000002;
        internal const uint DACL_SECURITY_INFORMATION = 0x00000004;
        internal const uint SACL_SECURITY_INFORMATION = 0x00000008;

        // HRESULTS
        internal const int CONTEXT_E_NOCONTEXT = unchecked((int)(0x8004E004));
        internal const int E_NOINTERFACE = unchecked((int)(0x80004002));



        [DllImport("kernel32.dll")]
        internal static extern int QueryPerformanceCounter(out Int64 lpPerformanceCount);

        [DllImport("kernel32.dll")]
        internal static extern int QueryPerformanceFrequency(out Int64 lpPerformanceCount);

        [DllImport("mtxex.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetObjectContext([Out]
        [MarshalAs(UnmanagedType.Interface)] out IObjectContext pCtx);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetCurrentProcess();

        /// <summary>
        /// Made public for testing purposes.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentProcessId();

        /// <summary>
        /// Made public for testing purposes.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [PreserveSig]
        public static extern int GetModuleFileName([In] IntPtr hModule, [Out] StringBuilder lpFilename, [In]
        [MarshalAs(UnmanagedType.U4)] int nSize);

        /// <summary>
        /// Made public for testing purposes.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("secur32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetUserNameExW", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool GetUserNameEx([In] ExtendedNameFormat nameFormat, StringBuilder nameBuffer, ref uint size);

        [DllImport("advapi32.dll")]
        internal static extern int GetSecurityInfo(IntPtr handle, SE_OBJECT_TYPE objectType, uint securityInformation, ref IntPtr ppSidOwner, ref IntPtr ppSidGroup, ref IntPtr ppDacl, ref IntPtr ppSacl, out IntPtr ppSecurityDescriptor);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool LookupAccountSid(
            IntPtr systemName, // name of local or remote computer
            IntPtr sid, // security identifier
            StringBuilder accountName, // account name buffer
            ref uint accountNameLength, // size of account name buffer
            StringBuilder domainName, // domain name
            ref uint domainNameLength, // size of domain name buffer
            out int sidType // SID type
            );

        /// <summary>
        /// Made public for testing purposes.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [ComImport]
        [Guid("51372AE0-CAE7-11CF-BE81-00AA00A2FA25")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IObjectContext
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            Object CreateInstance([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

            void SetComplete();

            void SetAbort();

            void EnableCommit();

            void DisableCommit();

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.Bool)]
            bool IsInTransaction();

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.Bool)]
            bool IsSecurityEnabled();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool IsCallerInRole([In]
            [MarshalAs(UnmanagedType.BStr)] String role);
        }

        internal enum ExtendedNameFormat : int
        {
            // Examples for the following formats assume a fictitous company
            // which hooks into the global X.500 and DNS name spaces as follows.
            //
            // Enterprise root domain in DNS is
            //
            //      widget.com
            //
            // Enterprise root domain in X.500 (RFC 1779 format) is
            //
            //      O=Widget, C=US
            //
            // There exists the child domain
            //
            //      engineering.widget.com
            //
            // equivalent to
            //
            //      OU=Engineering, O=Widget, C=US
            //
            // There exists a container within the Engineering domain
            //
            //      OU=Software, OU=Engineering, O=Widget, C=US
            //
            // There exists the user
            //
            //      CN=John Doe, OU=Software, OU=Engineering, O=Widget, C=US
            //
            // And this user's downlevel (pre-ADS) user name is
            //
            //      Engineering\JohnDoe

            // unknown name type
            NameUnknown = 0,

            // CN=John Doe, OU=Software, OU=Engineering, O=Widget, C=US
            NameFullyQualifiedDN = 1,

            // Engineering\JohnDoe
            NameSamCompatible = 2,

            // Probably "John Doe" but could be something else.  I.e. The
            // display name is not necessarily the defining RDN.
            NameDisplay = 3,

            // String-ized GUID as returned by IIDFromString().
            // eg: {4fa050f0-f561-11cf-bdd9-00aa003a77b6}
            NameUniqueId = 6,

            // engineering.widget.com/software/John Doe
            NameCanonical = 7,

            // johndoe@engineering.com
            NameUserPrincipal = 8,

            // Same as NameCanonical except that rightmost '/' is
            // replaced with '\n' - even in domain-only case.
            // eg: engineering.widget.com/software\nJohn Doe
            NameCanonicalEx = 9,

            // www/SRv.engineering.com/engineering.com
            NameServicePrincipal = 10,

            /// <summary>
            /// DNS domain name + SAM username 
            /// eg: engineering.widget.com\JohnDoe
            /// </summary>
            NameDnsDomain = 12
        }

        internal enum SE_OBJECT_TYPE
        {
            SE_UNKNOWN_OBJECT_TYPE = 0,
            SE_FILE_OBJECT,
            SE_SERVICE,
            SE_PRINTER,
            SE_REGISTRY_KEY,
            SE_LMSHARE,
            SE_KERNEL_OBJECT,
            SE_WINDOW_OBJECT,
            SE_DS_OBJECT,
            SE_DS_OBJECT_ALL,
            SE_PROVIDER_DEFINED_OBJECT,
            SE_WMIGUID_OBJECT
        }


        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagDVTARGETDEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int tdSize;
            [MarshalAs(UnmanagedType.U2)]
            public short tdDriverNameOffset;
            [MarshalAs(UnmanagedType.U2)]
            public short tdDeviceNameOffset;
            [MarshalAs(UnmanagedType.U2)]
            public short tdPortNameOffset;
            [MarshalAs(UnmanagedType.U2)]
            public short tdExtDevmodeOffset;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public COMRECT()
            {
            }

            public COMRECT(Rectangle r)
            {
                this.left = r.X;
                this.top = r.Y;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

            public COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static NativeMethods.COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new NativeMethods.COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return string.Concat(new object[] { "Left = ", this.left, " Top ", this.top, " Right = ", this.right, " Bottom = ", this.bottom });
            }

        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagLOGPALETTE
        {
            [MarshalAs(UnmanagedType.U2)]
            public short palVersion;
            [MarshalAs(UnmanagedType.U2)]
            public short palNumEntries;
        }
    }

}
