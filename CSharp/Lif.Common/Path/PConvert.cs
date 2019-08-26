using Lif.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lif.Common.Path
{
    public class PConvert
    {
        public string Prefix { get; set; }
        public string ArraySymbol { get; set; } = "[*]";
        public bool IsRegex { get; set; }

        public string PathOf<T1, TV1>(Expression<Func<T1, TV1>> f1)
        {
            return PathOf<T1, TV1, T1, TV1>(f1, null);
        }

        public string PathOf<T1, TV1, T2, TV2>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2)
        {
            return PathOf<T1, TV1, T2, TV2, T2, TV2>(f1, f2, null);
        }

        public string PathOf<T1, TV1, T2, TV2, T3, TV3>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3)
        {
            return PathOf<T1, TV1, T2, TV2, T3, TV3, T3, TV3>(f1, f2, f3, null);
        }

        public string PathOf<T1, TV1, T2, TV2, T3, TV3, T4, TV4>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3, Expression<Func<T4, TV4>> f4)
        {
            return PathOf<T1, TV1, T2, TV2, T3, TV3, T4, TV4, T4, TV4>(f1, f2, f3, f4, null);
        }

        public string PathOf<T1, TV1, T2, TV2, T3, TV3, T4, TV4, T5, TV5>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3, Expression<Func<T4, TV4>> f4, Expression<Func<T5, TV5>> f5)
        {
            var types = new Queue<Type>();
            var exprPaths = new List<string>();
            if (f1 != null)
            {
                types.Enqueue(typeof(T1));
                exprPaths.Add(f1.GetPath());
            }
            if (f2 != null)
            {
                types.Enqueue(typeof(T2));
                exprPaths.Add(f2.GetPath());
            }
            if (f3 != null)
            {
                types.Enqueue(typeof(T3));
                exprPaths.Add(f3.GetPath());
            }
            if (f4 != null)
            {
                types.Enqueue(typeof(T4));
                exprPaths.Add(f4.GetPath());
            }
            if (f5 != null)
            {
                types.Enqueue(typeof(T5));
                exprPaths.Add(f5.GetPath());
            }
            var path = string.Join(".", exprPaths);
            return Prefix + GetField(types, path);
        }

        public string PathOf<T1>(Expression<Func<T1, object>> f1)
        {
            return PathOf<T1, object>(f1);
        }

        public string PathOf<T1, T2>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2)
        {
            return PathOf<T1, object, T2, object>(f1, f2);
        }

        public string PathOf<T1, T2, T3>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3)
        {
            return PathOf<T1, object, T2, object, T3, object>(f1, f2, f3);
        }

        public string PathOf<T1, T2, T3, T4>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3, Expression<Func<T4, object>> f4)
        {
            return PathOf<T1, object, T2, object, T3, object, T4, object>(f1, f2, f3, f4);
        }

        public string PathOf<T1, T2, T3, T4, T5>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3, Expression<Func<T4, object>> f4, Expression<Func<T5, object>> f5)
        {
            return PathOf<T1, object, T2, object, T3, object, T4, object, T5, object>(f1, f2, f3, f4, f5);
        }


        public string PathOf(MemberPath memberPath)
        {
            var types = new Queue<Type>();
            var exprPaths = new List<string>();
            foreach (var item in memberPath.Items)
            {
                types.Enqueue(item.InType);
                exprPaths.Add(item.Expression.GetPath());
            }
            var path = string.Join(".", exprPaths);
            return Prefix + GetField(types, path);
        }

        private string GetField(Queue<Type> types, string path)
        {
            var parts = path.Split('.');
            var fields = new string[parts.Count(o => !string.IsNullOrEmpty(o))];
            var type = types.Dequeue();

            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                if (string.IsNullOrEmpty(part))
                {
                    //fields[i] = part;
                    continue;
                }

                MemberInfo memberInfo;

                do
                {
                    var members = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(o => o.CanRead && o.GetIndexParameters().Length == 0)
                        .Select(o => o as MemberInfo).ToList();
                    memberInfo = members.Find(o => o.Name == part);
                    if (memberInfo == null)
                    {
                        var exMessage = $"{type.FullName} 中不存在 {path}";
                        type = types.Dequeue();
                        if (type == null)
                        {
                            throw new NotSupportedException(exMessage);
                        }

                    }

                } while (memberInfo == null);

                var dataType = memberInfo is PropertyInfo ?
                    (memberInfo as PropertyInfo).PropertyType :
                    (memberInfo as FieldInfo).FieldType;

                fields[i] = memberInfo.Name;

                var isList = ReflectionUtils.IsList(dataType);

                if (isList)
                {
                    //如果是取Count之类的
                    if (i + 1 < parts.Length && !string.IsNullOrEmpty(parts[i + 1]))
                    {
                        if (dataType.GetMember(parts[i + 1]).Length > 0)
                        {
                            type = dataType;
                            continue;
                        }
                    }
                    type = ReflectionUtils.GetListItemType(dataType);
                    if (i != parts.Length - 1)
                        fields[i] += ArraySymbol;
                }
                else
                {
                    type = dataType;
                }

            }
            return string.Join(IsRegex ? @"\." : ".", fields);
        }
    }

}
