﻿using System.Text.Json;

using Mi.Application.Contracts.Cache;
using Mi.Application.Contracts.Cache.Models;
using Mi.Domain.Shared.Core;

namespace Mi.Application.Cache
{
    public class CacheKeyManagerService : ICacheKeyManagerService, IScoped
    {
        private readonly IMemoryCache _cache;
        private readonly ICurrentUser _currentUser;

        public CacheKeyManagerService(IMemoryCache cache, ICurrentUser currentUser)
        {
            _cache = cache;
            _currentUser = currentUser;
        }

        public async Task<MessageModel<IList<Option>>> GetAllKeysAsync(CacheKeySearch input)
        {
            var keys = _cache.GetCacheKeys();
            var list = keys.Select(k => new Option
            {
                Name = k,
                Value = k
            }).ToList();

            return await Task.FromResult(new MessageModel<IList<Option>>(list));
        }

        public async Task<MessageModel<string>> GetDataAsync(CacheKeyIn input)
        {
            if (_currentUser.IsDemo) return await Task.FromResult(new MessageModel<string>(Demo.Tip));

            _cache.TryGetValue(input.key, out var value);
            var str = JsonSerializer.Serialize(value ?? "");

            return await Task.FromResult(new MessageModel<string>(str));
        }

        public Task<MessageModel> RemoveKeyAsync(CacheKeyIn input)
        {
            if (_currentUser.IsDemo) return Task.FromResult(Back.Fail(Demo.Tip));

            _cache.Remove(input.key);

            return Task.FromResult(Back.Success());
        }
    }
}