using Lif.Common.Path;
using Lif.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Lif.Tests.Lif.Common
{
    [TestClass]
    public class Path
    {
        [TestMethod]
        public void Analysis()
        {
            Assert.AreEqual(P.PathOf<Organization>(o => o.Members[0].Name), "Members[*].Name");
            Assert.AreEqual(P.PathOf<Organization>(o => o.Members.Count), "Members.Count");
            Assert.AreEqual(P.PathOf<Organization, List<object>>(o => o.Members, o => o.Count), "Members.Count");
            Assert.AreEqual(P.PathOf<Organization, List<string>>(o => o.Members.Items, o => o.Count), "Members.Items[*].Count");
        }
    }
}
