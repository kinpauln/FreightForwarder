﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigzoneBusinessCenterService
{
    public class ServiceRetnEntity
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public object RetnValue { get; set; }
    }
}
