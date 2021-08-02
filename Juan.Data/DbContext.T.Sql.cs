using Juan.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Juan.Data
{
    public abstract partial class DbContext<T, Key> : IDbContext<T, Key> where T : class,new()
    {
        

   

    }
}
