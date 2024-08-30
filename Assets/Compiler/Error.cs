using System.Numerics;
using System;
using System.Collections.Generic;
namespace Compiler{public class CompilingError
    {
        public ErrorCode Code { get; private set; }

        public string Argument { get; private set; }

        public int Position{get; private set;}

        public void PrintErrors(List<CompilingError> Errors)
        {
           foreach(CompilingError error in Errors)
           {
             Console.WriteLine(error.Argument + " " + "at line" + " " + error.Position);
           }
        }

        public CompilingError(int position, ErrorCode code, string argument)
        {
            this.Code = code;
            this.Argument = argument;
            Position = position;
        }
    }

    public enum ErrorCode
    {
        None,
        Expected,
        Invalid,
        Unknown,
    }
}