﻿using Mi.Application.Contracts.System;
using Mi.Application.Contracts.System.Models;

namespace Mi.ControllerLibrary.System
{
    [ApiRoute]
    public class FunctionController : ControllerBase
    {
        private readonly IFunctionService _functionService;

        public FunctionController(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        [HttpGet, AuthorizeCode("System:Function:Query")]
        public async Task<ResponseStructure> GetFunctionList(FunctionSearch search)
        {
            return await _functionService.GetFunctionListAsync(search);
        }

        [HttpPost, AuthorizeCode("System:Function:AddOrUpdate")]
        public async Task<ResponseStructure> AddOrUpdateFunction([FromBody] FunctionOperation operation)
            => await _functionService.AddOrUpdateFunctionAsync(operation);

        [HttpPost, AuthorizeCode("System:Function:Remove")]
        public async Task<ResponseStructure> RemoveFunction([FromForm] IList<long> ids)
            => await _functionService.RemoveFunctionAsync(ids);

        [HttpPost, AuthorizeCode("System:Function:Query")]
        public IList<TreeOption> GetFunctionTree() => _functionService.GetFunctionTree();
    }
}