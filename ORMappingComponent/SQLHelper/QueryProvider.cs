// ***********************************************************************
// Assembly         : ORMappingComponent
// Author           : Dragonet
// Created          : 12-18-2015
//
// Last Modified By : Dragonet
// Last Modified On : 12-18-2015
// ***********************************************************************
// <copyright file="QueryGenerator.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 查询生成器
/// http://www.cnblogs.com/yaozhenfa/p/iqueryable_and_iqueryprovider.html
/// </summary>
namespace ComponentORM.SQLHelper
{
    public class QueryProvider : IQueryProvider
    {
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            this.AnalysisExpression(expression);
            return new Query<TElement>(this,expression);
        }

        /// <summary>
        /// 构造一个 <see cref="T:System.Linq.IQueryable" /> 对象，该对象可计算指定表达式目录树所表示的查询。
        /// http://www.cnblogs.com/yaozhenfa/p/iqueryable_and_iqueryprovider.html
        /// </summary>
        /// <param name="expression">表示 LINQ 查询的表达式目录树。</param>
        /// <returns>一个 <see cref="T:System.Linq.IQueryable" />，它可计算指定表达式目录树所表示的查询。</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = expression.Type;
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(Query<>).MakeGenericType(elementType),new object[]{this,expression});
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)this.Execute(expression);
        }

        /// <summary>
        /// 执行指定表达式目录树所表示的查询。
        /// </summary>
        /// <param name="expression">表示 LINQ 查询的表达式目录树。</param>
        /// <returns>执行指定查询所生成的值。</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Execute(Expression expression)
        {
            return 123;
        }

        public void AnalysisExpression(Expression exp)
         {
             switch (exp.NodeType)
             {
                 case ExpressionType.Call:
                     {
                         MethodCallExpression mce = exp as MethodCallExpression;
                         Console.WriteLine("The Method Is {0}", mce.Method.Name);
                         for (int i = 0; i < mce.Arguments.Count; i++)
                         {
                             AnalysisExpression(mce.Arguments[i]);
                         }
                     }
                     break;
                 case ExpressionType.Quote:
                     {
                         UnaryExpression ue = exp as UnaryExpression;
                         AnalysisExpression(ue.Operand);
                     }
                     break;
                 case ExpressionType.Lambda:
                     {
                         LambdaExpression le = exp as LambdaExpression;
                         AnalysisExpression(le.Body);
                     }
                     break;
                 case ExpressionType.Equal:
                     {
                         BinaryExpression be = exp as BinaryExpression;
                         Console.WriteLine("The Method Is {0}", exp.NodeType.ToString());
                         AnalysisExpression(be.Left);
                         AnalysisExpression(be.Right);
                     }
                     break;
                 case ExpressionType.Constant:
                     {
                         ConstantExpression ce = exp as ConstantExpression;
                         Console.WriteLine("The Value Type Is {0}", ce.Value.ToString());
                     }
                     break;
                 case ExpressionType.Parameter:
                     {
                         ParameterExpression pe = exp as ParameterExpression;
                         Console.WriteLine("The Parameter Is {0}", pe.Name);
                     }
                     break;
                 default:
                     {
                         Console.Write("UnKnow");
                     }
                     break;
             }
         }
    }
}
