using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juan.Core;
using Juan.Data;
using Juan.Context.Test.Context;

namespace Juan.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AlishopContext objAlishopContext=new AlishopContext();
            var info= objAlishopContext.GetList("","",100,0);
            Console.ReadLine();
        }
    }
}
