// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CredentialKind.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CompanyCalendar.Exporter.Google
{
    /// <summary>
    /// 認証方法を示す列挙体を表します。
    /// </summary>
    public enum CredentialKind
    {
        /// <summary>OAuth2.0 での認証を表します。</summary>
        OAuth,

        /// <summary>API キーでの認証を表します。</summary>
        ApiKey,

        /// <summary>サービスアカウントでの認証を表します。</summary>
        ServiceAccount,
    }
}
