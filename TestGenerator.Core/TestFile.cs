using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator.Core
{
    internal class TestFile
    {
        internal string TestName;
        internal string data;

        internal TestFile(string name,string data_)
        {
            TestName = name;
            data = data_;
        }
    }
}
