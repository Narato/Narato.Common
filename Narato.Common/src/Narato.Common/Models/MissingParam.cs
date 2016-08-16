using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narato.Common.Models
{
    public enum MissingParamType
    {
        QuerystringParam,
        Body,
        Header
    }
    public class MissingParam
    {
        public string Name { get; set; }
        public MissingParamType Type{ get; set; }

        public MissingParam(string name, MissingParamType type)
        {
            Name = name;
            Type = type;
        }
    }
}
