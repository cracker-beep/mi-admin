﻿using Simple.Admin.Domain.Shared.Fields;
using Simple.Admin.Domain.Shared.Models;

namespace Simple.Admin.Application.Contracts.System.Models.Dict
{
    public class DictSearch : PagingSearchModel, IRemark
    {
        /// <summary>
        /// Name/Key
        /// </summary>
        public string? Vague { get; set; }

        public string? Remark { get; set; }

        public long? ParentId { get; set; }
    }
}