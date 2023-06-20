using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FilesSeekProvider.Compoment
{
    public class Win32
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibraryW(string s_File);

        public static IntPtr LoadLibrary(string s_File)
        {
            IntPtr h_Module = LoadLibraryW(s_File);
            if (h_Module != IntPtr.Zero)
                return h_Module;

            int s32_Error = Marshal.GetLastWin32Error();
            throw new Win32Exception(s32_Error);
        }
    }

    public class ExtRichTextBox : RichTextBox
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams i_Params = base.CreateParams;
                try
                {
                    // Available since XP SP1
                    Win32.LoadLibrary("MsftEdit.dll"); // throws

                    // Replace "RichEdit20W" with "RichEdit50W"
                    i_Params.ClassName = "RichEdit50W";
                }
                catch
                {
                    // Windows XP without any Service Pack.
                }
                return i_Params;
            }
        }
    }
}
