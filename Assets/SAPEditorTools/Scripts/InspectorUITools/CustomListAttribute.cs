using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAPUnityEditorTools.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class CustomListAttribute : PropertyAttribute 
    {
        public CustomListAttribute() {}
    }
}
