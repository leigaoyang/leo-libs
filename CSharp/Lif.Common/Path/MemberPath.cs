using Lif.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lif.Common.Path
{
    public class MemberPath
    {
        public MemberPath()
        {
            Items = new List<MemberPathItem>();
        }

        public List<MemberPathItem> Items { get; }

        internal void AddItem<T, TV>(Expression<Func<T, TV>> f)
        {
            Items.Add(new MemberPathItem(typeof(T), typeof(TV), f));
        }

        public Type InType
        {
            get => Items.FirstOrDefault()?.InType;
        }

        public Type OutType
        {
            get => Items.LastOrDefault()?.OutType;
        }


        public override string ToString()
        {
            return P.PathOf(this);
        }
    }

    public class MemberPathItem
    {
        public MemberPathItem(Type inType, Type outType, Expression expression)
        {
            InType = inType;
            OutType = outType;
            Expression = expression;
        }
        public Type InType { get; }
        public Type OutType { get; }
        internal Expression Expression { get; }

        public override string ToString()
        {
            return Expression.GetPath();
        }
    }
}
