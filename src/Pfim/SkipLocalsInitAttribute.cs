﻿using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Module |
                    AttributeTargets.Class |
                    AttributeTargets.Struct |
                    AttributeTargets.Interface |
                    AttributeTargets.Constructor |
                    AttributeTargets.Method |
                    AttributeTargets.Property |
                    AttributeTargets.Event, Inherited = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class SkipLocalsInitAttribute : Attribute
    {
        public SkipLocalsInitAttribute()
        {
        }
    }
}
