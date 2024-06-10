using System;
using UnityEngine;

namespace OMG.Editors
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class HideParentFieldsAttribute : Attribute
    {
        public string[] FieldNames { get; }

        public HideParentFieldsAttribute(params string[] fieldNames)
        {
            FieldNames = fieldNames;
        }
    }
}
