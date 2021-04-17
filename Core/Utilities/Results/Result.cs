﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //buradaki this tek parametreli constructora değer yollamak için
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public Result(string message)
        {
            Message = message;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
