using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Idp.Service.Models
{
    public class EdErrorModel
    {
        public int Code { get; set; }

        public string ErrorMsg { get; set; }

        public bool IsSuccess { get; set; }
    }
}
