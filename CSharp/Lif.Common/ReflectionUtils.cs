using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lif.Common
{
    public static class ReflectionUtils
    {
        public static bool IsList(Type type)
        {
            if (type.IsArray) return true;
            if (type == typeof(string)) return false; // do not define "String" as IEnumerable<char>
            //if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) return true;

            foreach (var @interface in type.GetInterfaces())
            {
                if (@interface.GetTypeInfo().IsGenericType)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        // if needed, you can also return the type used as generic argument
                        return true;
                    }
                }

                if (type.GetTypeInfo().IsGenericType && @interface == typeof(IEnumerable))
                {
                    return true;
                }
            }

            return false;
        }

        public static Type GetListItemType(Type listType)
        {
            if (listType.IsArray) return listType.GetElementType();

            foreach (var i in listType.GetInterfaces())
            {
                if (i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return i.GetTypeInfo().GetGenericArguments()[0];
                }
                else if (listType.GetTypeInfo().IsGenericType && i == typeof(IEnumerable))
                {
                    return listType.GetTypeInfo().GetGenericArguments()[0];
                }
            }

            return typeof(object);
        }

        public static MemberInfo SelectMember(IEnumerable<MemberInfo> members, params Func<MemberInfo, bool>[] predicates)
        {
            foreach (var predicate in predicates)
            {
                var member = members.FirstOrDefault(predicate);

                if (member != null)
                {
                    return member;
                }
            }

            return null;
        }
    }

}
