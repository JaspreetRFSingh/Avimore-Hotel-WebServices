﻿using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    public interface IBaseService: IErrorResponse
    {
        public void SetErrorResponse(int errorCode, string resourceId, string resourceName);
    }
}