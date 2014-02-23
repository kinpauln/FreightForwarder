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
            try
            {
                return sr.GetMachineCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetRegCode(string machineCode)
        {
            SoftReg sr = new SoftReg();
            try
            {
                return sr.GetRegCode(machineCode).InsertFormat(6, "-");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
