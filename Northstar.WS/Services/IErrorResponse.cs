﻿using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    public interface IErrorResponse
    {
        public ApiError GetApiErrorResponse();
    }
}