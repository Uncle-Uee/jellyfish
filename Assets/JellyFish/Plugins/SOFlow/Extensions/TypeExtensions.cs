// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Reflection;
using UnityEngine;

namespace SOFlow.Extensions
{
    public static class TypeExtensions
    {
	    /// <summary>
	    ///     Returns the Type instance of the given class name.
	    /// </summary>
	    /// <param name="strFullyQualifiedName"></param>
	    /// <returns></returns>
	    public static Type GetInstanceType(string strFullyQualifiedName)
        {
            string sanitizedName = strFullyQualifiedName.Replace("PPtr<$", "").Replace(">", "");
            Type type = Type.GetType(sanitizedName);

            if(type != null) return type;

            foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Module[] modules = asm.GetModules();

                foreach(Module module in modules)
                {
                    foreach(Type _type in module.GetTypes())
                    {
                        if(_type.Name == sanitizedName)
                            return _type;
                    }
                }
            }

            return null;
        }
    }
}