﻿using Jobbr.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jobbr.WebApi.Tests
{
    [TestClass]
    public class HostingUrlTests : IntegrationTestBase
    {
        [TestMethod]
        public void ServerWithRootedHostPlain_AccessHostRootPlain_NoError()
        {
            var host = $"http://localhost:{NextFreeTcpPort()}";

            this.GivenRunningServerWithWebApi(host);
            var client = new JobbrClient(host);

            Assert.IsTrue(client.IsAvailable());
        }

        [TestMethod]
        public void ServerWithRootedHostPlain_AccessHostRootDash_NoError()
        {
            var host = $"http://localhost:{NextFreeTcpPort()}";

            this.GivenRunningServerWithWebApi(host);
            var client = new JobbrClient(host + "/");

            Assert.IsTrue(client.IsAvailable());
        }

        [TestMethod]
        public void ServerWithRootedHostDash_AccessHostRootPlain_NoError()
        {
            var host = $"http://localhost:{NextFreeTcpPort()}";

            this.GivenRunningServerWithWebApi(host + "/");
            var client = new JobbrClient(host);

            Assert.IsTrue(client.IsAvailable());
        }

        [TestMethod]
        public void ServerWithRootedHostDash_AccessHostRootDash_NoError()
        {
            var host = $"http://localhost:{NextFreeTcpPort()}";

            this.GivenRunningServerWithWebApi(host + "/");
            var client = new JobbrClient(host + "/");

            Assert.IsTrue(client.IsAvailable());
        }

        [TestMethod]
        public void ServerWithHostPathPlain_AccessHostPathPlain_NoError()
        {
            var host = $"http://localhost:{NextFreeTcpPort()}";

            this.GivenRunningServerWithWebApi(host + "/path");
            var client = new JobbrClient(host + "/path");

            Assert.IsTrue(client.IsAvailable());
        }

        [TestMethod]
        public void ServerWithHostPathPlain_AccessHostPathDash_NoError()
        {
            var host = $"http://localhost:{NextFreeTcpPort()}";

            this.GivenRunningServerWithWebApi(host + "/path");
            var client = new JobbrClient(host + "/path/");

            Assert.IsTrue(client.IsAvailable());
        }

        [TestMethod]
        public void ServerWithdHostPathDash_AccessHostPathPlain_NoError()
        {
            var host = $"http://localhost:{NextFreeTcpPort()}";

            this.GivenRunningServerWithWebApi(host + "/path/");
            var client = new JobbrClient(host + "/path");

            Assert.IsTrue(client.IsAvailable());
        }

        [TestMethod]
        public void ServerWithHostPathDash_AccessHostRootDash_NoError()
        {
            var host = $"http://localhost:{NextFreeTcpPort()}";

            this.GivenRunningServerWithWebApi(host + "/path/");
            var client = new JobbrClient(host + "/path");

            Assert.IsTrue(client.IsAvailable());
        }
    }
}
