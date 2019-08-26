using System;
using System.Linq.Expressions;

namespace Lif.Common.Path
{
    public static class P
    {

        public static string PathOf<T1>(Expression<Func<T1, object>> f1)
        {
            return new PConvert().PathOf(f1);
        }
        public static string PathOf<T1, T2>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2)
        {
            return new PConvert().PathOf(f1, f2);
        }

        public static string PathOf<T1, T2, T3>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3)
        {
            return new PConvert().PathOf(f1, f2, f3);
        }

        public static string PathOf<T1, T2, T3, T4>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3, Expression<Func<T4, object>> f4)
        {
            return new PConvert().PathOf(f1, f2, f3, f4);
        }

        public static string PathOf<T1, T2, T3, T4, T5>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3, Expression<Func<T4, object>> f4, Expression<Func<T5, object>> f5)
        {
            return new PConvert().PathOf(f1, f2, f3, f4, f5);
        }


        public static string PathOf<T1, TV1>(Expression<Func<T1, TV1>> f1)
        {
            return new PConvert().PathOf(f1);
        }

        public static string PathOf<T1, TV1, T2, TV2>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2)
        {
            return new PConvert().PathOf(f1, f2);
        }

        public static string PathOf<T1, TV1, T2, TV2, T3, TV3>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3)
        {
            return new PConvert().PathOf(f1, f2, f3);
        }

        public static string PathOf<T1, TV1, T2, TV2, T3, TV3, T4, TV4>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3, Expression<Func<T4, TV4>> f4)
        {
            return new PConvert().PathOf(f1, f2, f3, f4);
        }

        public static string PathOf<T1, TV1, T2, TV2, T3, TV3, T4, TV4, T5, TV5>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3, Expression<Func<T4, TV4>> f4, Expression<Func<T5, TV5>> f5)
        {
            return new PConvert().PathOf(f1, f2, f3, f4, f5);
        }


        public static MemberPath Path<T1>(Expression<Func<T1, object>> f1)
        {
            return Path<T1, object>(f1);
        }

        public static MemberPath Path<T1, T2>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2)
        {
            return Path<T1, object, T2, object>(f1, f2);
        }

        public static MemberPath Path<T1, T2, T3>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3)
        {
            return Path<T1, object, T2, object, T3, object>(f1, f2, f3);
        }

        public static MemberPath Path<T1, T2, T3, T4>(Expression<Func<T1, object>> f1, Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3, Expression<Func<T4, object>> f4)
        {
            return Path<T1, object, T2, object, T3, object, T4, object>(f1, f2, f3, f4);
        }

        public static MemberPath Path<T1, T2, T3, T4, T5>(Expression<Func<T1, object>> f1,
            Expression<Func<T2, object>> f2, Expression<Func<T3, object>> f3, Expression<Func<T4, object>> f4,
            Expression<Func<T5, object>> f5)
        {
            return Path<T1, object, T2, object, T3, object, T4, object, T5, object>(f1, f2, f3, f4, f5);
        }


        public static MemberPath Path<T1, TV1>(Expression<Func<T1, TV1>> f1)
        {
            return Path<T1, TV1, T1, TV1>(f1, null);
        }

        public static MemberPath Path<T1, TV1, T2, TV2>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2)
        {
            return Path<T1, TV1, T2, TV2, T2, TV2>(f1, f2, null);
        }

        public static MemberPath Path<T1, TV1, T2, TV2, T3, TV3>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3)
        {
            return Path<T1, TV1, T2, TV2, T3, TV3, T3, TV3>(f1, f2, f3, null);
        }

        public static MemberPath Path<T1, TV1, T2, TV2, T3, TV3, T4, TV4>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3, Expression<Func<T4, TV4>> f4)
        {
            return Path<T1, TV1, T2, TV2, T3, TV3, T4, TV4, T4, TV4>(f1, f2, f3, f4, null);
        }

        public static MemberPath Path<T1, TV1, T2, TV2, T3, TV3, T4, TV4, T5, TV5>(Expression<Func<T1, TV1>> f1, Expression<Func<T2, TV2>> f2, Expression<Func<T3, TV3>> f3, Expression<Func<T4, TV4>> f4, Expression<Func<T5, TV5>> f5)
        {
            var memberPaths = new MemberPath();
            if (f1 == null) return memberPaths;
            memberPaths.AddItem(f1);
            if (f2 == null) return memberPaths;
            memberPaths.AddItem(f2);
            if (f3 == null) return memberPaths;
            memberPaths.AddItem(f3);
            if (f4 == null) return memberPaths;
            memberPaths.AddItem(f4);
            if (f5 == null) return memberPaths;
            memberPaths.AddItem(f5);
            return memberPaths;
        }

        public static string PathOf(MemberPath memberPath)
        {
            return new PConvert().PathOf(memberPath);
        }
    }

}
