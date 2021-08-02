using PlainElastic7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainElastic7.Net.Utils;
namespace Juan.Data.ESearch
{
    public class OperationOptions : BulkOperationOptions
    {


        public override string ToString()
        {
            var result = new[] {
                                   VersionType.IsNullOrEmpty() ? "" : "\"_version_type\": " + VersionType.Quotate(),
                                   Version == null             ? "" : "\"_version\": " + Version.AsString(),
                                   Routing.IsNullOrEmpty()     ? "" : "\"_routing\": " + Routing.Quotate(),
                                   Percolate.IsNullOrEmpty()   ? "" : "\"_percolate\": " + Percolate.Quotate(),
                                   Parent.IsNullOrEmpty()      ? "" : "\"_parent\": " + Parent.Quotate(),
                                   Timestamp.IsNullOrEmpty()   ? "" : "\"_timestamp\": " + Timestamp.Quotate(),
                                   Ttl.IsNullOrEmpty()         ? "" : "\"_ttl\": " + Ttl.Quotate(),
                               };

            return result.Where(s => !s.IsNullOrEmpty()).JoinWithComma();
        }


    }
}
