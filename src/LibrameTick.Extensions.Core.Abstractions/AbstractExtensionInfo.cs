﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Text.Json.Serialization;

namespace Librame.Extensions.Core
{
    /// <summary>
    /// 抽象扩展信息（抽象实现 <see cref="IExtensionInfo"/>）。
    /// </summary>
    public abstract class AbstractExtensionInfo : AbstractNotifyProperty, IExtensionInfo
    {
        /// <summary>
        /// 抽象一个 <see cref="AbstractExtensionInfo"/>。
        /// </summary>
        /// <param name="parent">给定的父级 <see cref="IExtensionInfo"/>。</param>
        public AbstractExtensionInfo(IExtensionInfo? parent)
            : base()
        {
            Parent = parent;
        }


        /// <summary>
        /// 当前类型。
        /// </summary>
        [JsonIgnore]
        public virtual Type CurrentType
            => GetType();

        /// <summary>
        /// 名称。
        /// </summary>
        [JsonIgnore]
        public virtual string Name
            => CurrentType.Name;

        /// <summary>
        /// 父级。
        /// </summary>
        [JsonIgnore]
        public virtual IExtensionInfo? Parent { get; private set; }
    }
}
