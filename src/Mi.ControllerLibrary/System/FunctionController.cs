﻿using Mi.Application.Contracts.System;
using Mi.Application.Contracts.System.Models.Function;
using Mi.Domain.Shared.Core;

namespace Mi.ControllerLibrary.System
{
    [Authorize]
    public class FunctionController : MiControllerBase
    {
        private readonly IFunctionService _functionService;

        public FunctionController(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        [HttpPost, AuthorizeCode("System:Function:Query")]
        public async Task<ResponseStructure> GetFunctionList([FromBody] FunctionSearch search)
        {
            return await _functionService.GetFunctionListAsync(search);
        }

        [HttpPost, AuthorizeCode("System:Function:AddOrUpdate")]
        public async Task<ResponseStructure> AddOrUpdateFunction([FromBody] FunctionOperation operation)
            => await _functionService.AddOrUpdateFunctionAsync(operation);

        [HttpPost, AuthorizeCode("System:Function:Remove")]
        public async Task<ResponseStructure> RemoveFunction([FromBody] PrimaryKeys input)
            => await _functionService.RemoveFunctionAsync(input);

        [HttpPost, AuthorizeCode("System:Function:Query")]
        public IList<TreeOption> GetFunctionTree() => _functionService.GetFunctionTree();
    }
}