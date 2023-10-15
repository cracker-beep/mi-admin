﻿using Mi.Domain.Exceptions;
using Mi.Domain.Shared.Core;

namespace Mi.DataDriver
{
    public class Demo
    {
        public static string Tip => "演示账号没有此权限";

        public static void ThrowExceptionForDBWriteAction(ICurrentUser currentUser)
        {
            if (currentUser.IsDemo)
            {
                throw new FriendlyException(Tip);
            }
        }
    }
}