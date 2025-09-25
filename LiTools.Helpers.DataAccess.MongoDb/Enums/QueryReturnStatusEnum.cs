// <summary>
// MongoDb Query return status enum.
// </summary>
// <copyright file="QueryReturnStatusEnum.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiTools.Helpers.DataAccess.MongoDb.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Query return status.
    /// </summary>
    public enum QueryReturnStatusEnum
    {
        /// <summary>Qyery was corrct.</summary>
        Ok = 10,

        /// <summary>Qyery has error.</summary>
        Error = 30,

        /// <summary>Input data has error.</summary>
        InputDataError = 60,

        /// <summary>DuplicateKey, already exists.</summary>
        AlreadyExists = 70,

        /// <summary>Query has zero returns.</summary>
        ZeroReturn = 80,

        /// <summary> Request is cancellated. </summary>
        Cancellation = 100,
    }

    /// <summary>
    /// Document exist return enum.
    /// </summary>
    public enum DocumentExistReturnStatusEnum
    {
        /// <summary>Query has error.</summary>
        Error,

        /// <summary>Input data has error.</summary>
        InputDataError,

        /// <summary>Document exist in database.</summary>
        Exist,

        /// <summary>Document dont exist in database..</summary>
        DontExist,
    }
}