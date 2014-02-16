using FreightForwarder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Business
{
    public class CommonTool
    {
        public static string GetMachineCode() {
            SoftReg sr = new SoftReg();
            return sr.GetMachineCode();
        }

        public static string GeRegCode(string machineCode)
        {
            SoftReg sr = new SoftReg();
            return sr.GetRegCode(machineCode);
        }
    }
}
