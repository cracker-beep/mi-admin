﻿using Mi.Domain.Shared.Response;

namespace Mi.Domain.Extension
{
    public static class ResponseStructureExtension
    {
        public static bool IsOk(this MessageModel? response)
        {
            return response != null && response.Code == response_type.Success;
        }

        public static MessageModel<T> As<T>(this MessageModel model, T? result = default)
        {
            return new MessageModel<T>(model.Code, model.Message ?? "", result);
        }
    }
}