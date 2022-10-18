﻿using Bouvet.AssetHub.API.Contracts;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.AssetHub.API.Controllers.Helpers
{
    public class ResultHelper<T> : ControllerBase
    {
        public ActionResult<T> OkOrNotFound(Option<T> result)
        {
            return result.IsSome ? Ok(result.FirstOrDefault()) : NotFound();
        }
        public ActionResult<T> OkOrBadRequest(Option<T> result, string msg)
        {
            return result.IsSome ? Ok(result.FirstOrDefault()) : BadRequest(msg);
        }

    }
}