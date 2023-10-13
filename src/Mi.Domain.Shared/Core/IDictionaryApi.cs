﻿namespace Mi.Domain.Shared.Core
{
    /// <summary>
    /// 全局字典
    /// </summary>
    public interface IDictionaryApi : ISingleton
    {
        /// <summary>
        /// 读取字典value
        /// </summary>
        /// <param name="key">字典key</param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 读取字典value
        /// </summary>
        /// <param name="key">字典key</param>
        /// <returns></returns>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        /// 读取字典子集
        /// </summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetManyAsync(string parentKey);

        /// <summary>
        /// 更新字典value
        /// </summary>
        /// <param name="key">字典key</param>
        /// <param name="value">字典value</param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, string value);

        /// <summary>
        /// 批量更新字典value
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<bool> SetAsync(Dictionary<string, string> dict);
    }
}