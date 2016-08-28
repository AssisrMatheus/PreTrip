using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PreTrip.Lib.Utils;

namespace PreTrip.Test
{
    [TestClass]
    public class PasswordAdminGenerator
    {
        [TestMethod]
        public void GerarPasswordAdministrador()
        {
            var password = CreatePass.Create();

            Debug.Write(string.Format("\r\n{0}",password));
        }
    }
}


