using System;
using System.Runtime.InteropServices;

namespace PocketPython
{
    public static class Bindings
    {
#if !UNITY_EDITOR && UNITY_IOS
        private const string _libName = "__Internal";
#else
        private const string _libName = "pocketpy";
#endif
        [DllImport(_libName)]
        public static extern void pkpy_vm_compile(IntPtr vm, string source, string filename, int mode, out bool ok, out string res);
        [DllImport(_libName)]
        public static extern IntPtr pkpy_new_vm(bool enable_os);
        [DllImport(_libName)]
        public static extern void pkpy_vm_add_module(IntPtr vm, string name, string source);
        [DllImport(_libName)]
        public static extern void pkpy_vm_exec(IntPtr vm, string source);
        [DllImport(_libName)]
        public static extern void pkpy_vm_exec_2(IntPtr vm, string source, string filename, int mode, string module);
    }
}
