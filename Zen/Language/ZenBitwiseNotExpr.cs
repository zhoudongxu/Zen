﻿// <copyright file="ZenBitwiseNotExpr.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Research.Zen
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class representing a Not expression.
    /// </summary>
    internal sealed class ZenBitwiseNotExpr<T> : Zen<T>
    {
        private static Dictionary<object, ZenBitwiseNotExpr<T>> hashConsTable =
            new Dictionary<object, ZenBitwiseNotExpr<T>>();

        public static ZenBitwiseNotExpr<T> Create(Zen<T> expr)
        {
            CommonUtilities.Validate(expr);
            CommonUtilities.ValidateIsIntegerType(typeof(T));

            if (hashConsTable.TryGetValue(expr, out var value))
            {
                return value;
            }

            var ret = new ZenBitwiseNotExpr<T>(expr);
            hashConsTable[expr] = ret;
            return ret;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZenBitwiseNotExpr{T}"/> class.
        /// </summary>
        /// <param name="expr">The expression.</param>
        private ZenBitwiseNotExpr(Zen<T> expr)
        {
            this.Expr = expr;
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        internal Zen<T> Expr { get; }

        /// <summary>
        /// Convert the expression to a string.
        /// </summary>
        /// <returns>The string representation.</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"~({this.Expr.ToString()})";
        }

        /// <summary>
        /// Implementing the visitor interface.
        /// </summary>
        /// <param name="visitor">The visitor object.</param>
        /// <param name="parameter">The visitor parameter.</param>
        /// <typeparam name="TParam">The visitor parameter type.</typeparam>
        /// <typeparam name="TReturn">The visitor return type.</typeparam>
        /// <returns>A return value.</returns>
        internal override TReturn Accept<TParam, TReturn>(IZenExprVisitor<TParam, TReturn> visitor, TParam parameter)
        {
            return visitor.VisitZenBitwiseNotExpr(this, parameter);
        }

        /// <summary>
        /// Implementing the transformer interface.
        /// </summary>
        /// <param name="visitor">The visitor object.</param>
        /// <returns>A return value.</returns>
        internal override Zen<T> Accept(IZenExprTransformer visitor)
        {
            return visitor.VisitZenBitwiseNotExpr(this);
        }
    }
}
