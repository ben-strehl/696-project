using System;
using System.Runtime.InteropServices;

namespace PocketPython
{
    public static class LuaBindings
    {
#if !UNITY_EDITOR && UNITY_IOS
        private const string _libName = "__Internal";
#else
        private const string _libName = "pocketpy";
#endif
        public delegate void OutputHandler(IntPtr vm, string message);
        public delegate void OnDeleteHandler(IntPtr vm, IntPtr obj);
        public delegate int LuaStyleFunction(IntPtr vm);

        [DllImport(_libName)]
        public static extern bool pkpy_clear_error(IntPtr vm, out IntPtr message);
        [DllImport(_libName)]
        public static extern bool pkpy_error(IntPtr vm, string name, string message);
        [DllImport(_libName)]
        public static extern IntPtr pkpy_vm_create(bool use_stdio, bool enable_os);
        [DllImport(_libName)]
        public static extern bool pkpy_vm_run(IntPtr vm, string source);
        [DllImport(_libName)]
        public static extern void pkpy_vm_destroy(IntPtr vm);
        [DllImport(_libName)]
        public static extern bool pkpy_pop(IntPtr vm, int n);
        [DllImport(_libName)]
        public static extern bool pkpy_push(IntPtr vm, int index);
        [DllImport(_libName)]
        public static extern bool pkpy_push_function(IntPtr vm, LuaStyleFunction function, int argc);
        [DllImport(_libName)]
        public static extern bool pkpy_push_int(IntPtr vm, int value);
        [DllImport(_libName)]
        public static extern bool pkpy_push_float(IntPtr vm, double value);
        [DllImport(_libName)]
        public static extern bool pkpy_push_bool(IntPtr vm, bool value);
        [DllImport(_libName)]
        public static extern bool pkpy_push_string(IntPtr vm, string value);
        [DllImport(_libName)]
        public static extern bool pkpy_push_stringn(IntPtr vm, string value, int length);
        [DllImport(_libName)]
        public static extern bool pkpy_push_voidp(IntPtr vm, IntPtr value);
        [DllImport(_libName)]
        public static extern bool pkpy_push_none(IntPtr vm);
        [DllImport(_libName)]
        public static extern bool pkpy_set_global(IntPtr vm, string name);
        [DllImport(_libName)]
        public static extern bool pkpy_get_global(IntPtr vm, string name);
        [DllImport(_libName)]
        public static extern bool pkpy_call(IntPtr vm, int argc);
        [DllImport(_libName)]
        public static extern bool pkpy_call_method(IntPtr vm, string name, int argc);
        [DllImport(_libName)]
        public static extern bool pkpy_to_int(IntPtr vm, int index, out int ret);
        [DllImport(_libName)]
        public static extern bool pkpy_to_float(IntPtr vm, int index, out double ret);
        [DllImport(_libName)]
        public static extern bool pkpy_to_bool(IntPtr vm, int index, out bool ret);
        [DllImport(_libName)]
        public static extern bool pkpy_to_voidp(IntPtr vm, int index, out IntPtr ret);
        [DllImport(_libName)]
        public static extern bool pkpy_to_string(IntPtr vm, int index, out string ret);
        [DllImport(_libName)]
        public static extern bool pkpy_to_stringn(IntPtr vm, int index, out IntPtr ret, out int size);
        [DllImport(_libName)]
        public static extern bool pkpy_is_int(IntPtr vm, int index);
        [DllImport(_libName)]
        public static extern bool pkpy_is_float(IntPtr vm, int index);
        [DllImport(_libName)]
        public static extern bool pkpy_is_bool(IntPtr vm, int index);
        [DllImport(_libName)]
        public static extern bool pkpy_is_string(IntPtr vm, int index);
        [DllImport(_libName)]
        public static extern bool pkpy_is_voidp(IntPtr vm, int index);
        [DllImport(_libName)]
        public static extern bool pkpy_is_none(IntPtr vm, int index);
        [DllImport(_libName)]
        public static extern bool pkpy_check_global(IntPtr vm, string name);
        [DllImport(_libName)]
        public static extern bool pkpy_check_error(IntPtr vm);
        [DllImport(_libName)]
        public static extern bool pkpy_check_stack(IntPtr vm, int free);
        [DllImport(_libName)]
        public static extern int pkpy_stack_size(IntPtr vm);
        [DllImport(_libName)]
        public static extern void pkpy_set_output_handlers(IntPtr vm, OutputHandler stdout_handler, OutputHandler stderr_handler);
        [DllImport(_libName)]
        public static extern void pkpy_vm_gc_on_delete(IntPtr vm, OnDeleteHandler handler);

        [DllImport(_libName)]
        public static extern bool pkpy_getattr(IntPtr vm, string name);
        [DllImport(_libName)]
        public static extern bool pkpy_setattr(IntPtr vm, string name);
        [DllImport(_libName)]
        public static extern bool pkpy_eval(IntPtr vm, string source);
    }
}
