﻿using FreeSql.Internal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreeSql.Internal.CommonProvider
{

    public abstract class Select3Provider<T1, T2, T3> : Select0Provider<ISelect<T1, T2, T3>, T1>, ISelect<T1, T2, T3>
            where T1 : class
            where T2 : class
            where T3 : class
    {

        public Select3Provider(IFreeSql orm, CommonUtils commonUtils, CommonExpression commonExpression, object dywhere) : base(orm, commonUtils, commonExpression, dywhere)
        {
            if (_orm.CodeFirst.IsAutoSyncStructure) _orm.CodeFirst.SyncStructure(typeof(T2), typeof(T3));
            _tables.Add(new SelectTableInfo { Table = _commonUtils.GetTableByEntity(typeof(T2)), Alias = $"SP10b", On = null, Type = SelectTableInfoType.From });
            _tables.Add(new SelectTableInfo { Table = _commonUtils.GetTableByEntity(typeof(T3)), Alias = $"SP10c", On = null, Type = SelectTableInfoType.From });
        }

        double ISelect<T1, T2, T3>.Avg<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) return default(double);
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalAvg(column?.Body);
        }

        ISelectGrouping<TKey, NaviteTuple<T1, T2, T3>> ISelect<T1, T2, T3>.GroupBy<TKey>(Expression<Func<T1, T2, T3, TKey>> exp)
        {
            if (exp == null) return this.InternalGroupBy<TKey, NaviteTuple<T1, T2, T3>>(exp?.Body);
            for (var a = 0; a < exp.Parameters.Count; a++) _tables[a].Parameter = exp.Parameters[a];
            return this.InternalGroupBy<TKey, NaviteTuple<T1, T2, T3>>(exp?.Body);
        }

        TMember ISelect<T1, T2, T3>.Max<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) return default(TMember);
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalMax<TMember>(column?.Body);
        }

        TMember ISelect<T1, T2, T3>.Min<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) return default(TMember);
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalMin<TMember>(column?.Body);
        }

        ISelect<T1, T2, T3> ISelect<T1, T2, T3>.OrderBy<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) this.InternalOrderBy(column?.Body);
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalOrderBy(column?.Body);
        }

        ISelect<T1, T2, T3> ISelect<T1, T2, T3>.OrderByDescending<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) this.InternalOrderBy(column?.Body);
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalOrderByDescending(column?.Body);
        }

        decimal ISelect<T1, T2, T3>.Sum<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) this.InternalOrderBy(column?.Body);
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalSum(column?.Body);
        }

        TReturn ISelect<T1, T2, T3>.ToAggregate<TReturn>(Expression<Func<ISelectGroupingAggregate<T1>, ISelectGroupingAggregate<T2>, ISelectGroupingAggregate<T3>, TReturn>> select)
        {
            if (select == null) return default(TReturn);
            for (var a = 0; a < select.Parameters.Count; a++) _tables[a].Parameter = select.Parameters[a];
            return this.InternalToAggregate<TReturn>(select?.Body);
        }

        List<TReturn> ISelect<T1, T2, T3>.ToList<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select)
        {
            if (select == null) return this.InternalToList<TReturn>(select?.Body);
            for (var a = 0; a < select.Parameters.Count; a++) _tables[a].Parameter = select.Parameters[a];
            return this.InternalToList<TReturn>(select?.Body);
        }

        List<TDto> ISelect<T1, T2, T3>.ToList<TDto>() => (this as ISelect<T1, T2, T3>).ToList(GetToListDtoSelector<TDto>());
        Expression<Func<T1, T2, T3, TDto>> GetToListDtoSelector<TDto>()
        {
            return Expression.Lambda<Func<T1, T2, T3, TDto>>(
                typeof(TDto).InternalNewExpression(),
                _tables[0].Parameter ?? Expression.Parameter(typeof(T1), "a"),
                Expression.Parameter(typeof(T2), "b"),
                Expression.Parameter(typeof(T3), "c"));
        }

        DataTable ISelect<T1, T2, T3>.ToDataTable<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select)
        {
            if (select == null) return this.InternalToDataTable(select?.Body);
            for (var a = 0; a < select.Parameters.Count; a++) _tables[a].Parameter = select.Parameters[a];
            return this.InternalToDataTable(select?.Body);
        }

        string ISelect<T1, T2, T3>.ToSql<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select, FieldAliasOptions fieldAlias)
        {
            if (select == null) return this.InternalToSql<TReturn>(select?.Body, fieldAlias);
            for (var a = 0; a < select.Parameters.Count; a++) _tables[a].Parameter = select.Parameters[a];
            return this.InternalToSql<TReturn>(select?.Body, fieldAlias);
        }

        ISelect<T1, T2, T3> ISelect<T1, T2, T3>.LeftJoin(Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (exp == null) return this.InternalJoin(exp?.Body, SelectTableInfoType.LeftJoin);
            for (var a = 0; a < exp.Parameters.Count; a++) _tables[a].Parameter = exp.Parameters[a];
            return this.InternalJoin(exp?.Body, SelectTableInfoType.LeftJoin);
        }

        ISelect<T1, T2, T3> ISelect<T1, T2, T3>.InnerJoin(Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (exp == null) return this.InternalJoin(exp?.Body, SelectTableInfoType.LeftJoin);
            for (var a = 0; a < exp.Parameters.Count; a++) _tables[a].Parameter = exp.Parameters[a];
            return this.InternalJoin(exp?.Body, SelectTableInfoType.InnerJoin);
        }

        ISelect<T1, T2, T3> ISelect<T1, T2, T3>.RightJoin(Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (exp == null) return this.InternalJoin(exp?.Body, SelectTableInfoType.LeftJoin);
            for (var a = 0; a < exp.Parameters.Count; a++) _tables[a].Parameter = exp.Parameters[a];
            return this.InternalJoin(exp?.Body, SelectTableInfoType.RightJoin);
        }

        ISelect<T1, T2, T3> ISelect<T1, T2, T3>.Where(Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (exp == null) return this.Where(null);
            for (var a = 0; a < exp.Parameters.Count; a++) _tables[a].Parameter = exp.Parameters[a];
            return this.Where(_commonExpression.ExpressionWhereLambda(_tables, exp?.Body, null, _whereCascadeExpression, _params));
        }

        ISelect<T1, T2, T3> ISelect<T1, T2, T3>.WhereIf(bool condition, Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (condition == false || exp == null) return this;
            for (var a = 0; a < exp.Parameters.Count; a++) _tables[a].Parameter = exp.Parameters[a];
            return this.Where(_commonExpression.ExpressionWhereLambda(_tables, exp?.Body, null, _whereCascadeExpression, _params));
        }

        bool ISelect<T1, T2, T3>.Any(Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (exp == null) return this.Any();
            for (var a = 0; a < exp.Parameters.Count; a++) _tables[a].Parameter = exp.Parameters[a];
            return this.Where(_commonExpression.ExpressionWhereLambda(_tables, exp?.Body, null, _whereCascadeExpression, _params)).Any();
        }

        TReturn ISelect<T1, T2, T3>.ToOne<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select) => (this as ISelect<T1, T2, T3>).Limit(1).ToList(select).FirstOrDefault();
        TReturn ISelect<T1, T2, T3>.First<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select) => (this as ISelect<T1, T2, T3>).Limit(1).ToList(select).FirstOrDefault();
        TDto ISelect<T1, T2, T3>.First<TDto>() => (this as ISelect<T1, T2, T3>).Limit(1).ToList<TDto>().FirstOrDefault();

#if net40
#else
        Task<double> ISelect<T1, T2, T3>.AvgAsync<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) return Task.FromResult(default(double));
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalAvgAsync(column?.Body);
        }

        Task<TMember> ISelect<T1, T2, T3>.MaxAsync<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) return Task.FromResult(default(TMember));
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalMaxAsync<TMember>(column?.Body);
        }

        Task<TMember> ISelect<T1, T2, T3>.MinAsync<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) return Task.FromResult(default(TMember));
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalMinAsync<TMember>(column?.Body);
        }

        Task<decimal> ISelect<T1, T2, T3>.SumAsync<TMember>(Expression<Func<T1, T2, T3, TMember>> column)
        {
            if (column == null) this.InternalOrderBy(column?.Body);
            for (var a = 0; a < column.Parameters.Count; a++) _tables[a].Parameter = column.Parameters[a];
            return this.InternalSumAsync(column?.Body);
        }

        Task<TReturn> ISelect<T1, T2, T3>.ToAggregateAsync<TReturn>(Expression<Func<ISelectGroupingAggregate<T1>, ISelectGroupingAggregate<T2>, ISelectGroupingAggregate<T3>, TReturn>> select)
        {
            if (select == null) return Task.FromResult(default(TReturn));
            for (var a = 0; a < select.Parameters.Count; a++) _tables[a].Parameter = select.Parameters[a];
            return this.InternalToAggregateAsync<TReturn>(select?.Body);
        }

        Task<List<TReturn>> ISelect<T1, T2, T3>.ToListAsync<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select)
        {
            if (select == null) return this.InternalToListAsync<TReturn>(select?.Body);
            for (var a = 0; a < select.Parameters.Count; a++) _tables[a].Parameter = select.Parameters[a];
            return this.InternalToListAsync<TReturn>(select?.Body);
        }
        Task<List<TDto>> ISelect<T1, T2, T3>.ToListAsync<TDto>() => (this as ISelect<T1, T2, T3>).ToListAsync(GetToListDtoSelector<TDto>());

        Task<DataTable> ISelect<T1, T2, T3>.ToDataTableAsync<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select)
        {
            if (select == null) return this.InternalToDataTableAsync(select?.Body);
            for (var a = 0; a < select.Parameters.Count; a++) _tables[a].Parameter = select.Parameters[a];
            return this.InternalToDataTableAsync(select?.Body);
        }

        Task<bool> ISelect<T1, T2, T3>.AnyAsync(Expression<Func<T1, T2, T3, bool>> exp)
        {
            if (exp == null) return this.AnyAsync();
            for (var a = 0; a < exp.Parameters.Count; a++) _tables[a].Parameter = exp.Parameters[a];
            return this.Where(_commonExpression.ExpressionWhereLambda(_tables, exp?.Body, null, _whereCascadeExpression, _params)).AnyAsync();
        }

        async Task<TReturn> ISelect<T1, T2, T3>.ToOneAsync<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select) => (await (this as ISelect<T1, T2, T3>).Limit(1).ToListAsync(select)).FirstOrDefault();
        async Task<TReturn> ISelect<T1, T2, T3>.FirstAsync<TReturn>(Expression<Func<T1, T2, T3, TReturn>> select) => (await (this as ISelect<T1, T2, T3>).Limit(1).ToListAsync(select)).FirstOrDefault();
        async Task<TDto> ISelect<T1, T2, T3>.FirstAsync<TDto>() => (await (this as ISelect<T1, T2, T3>).Limit(1).ToListAsync<TDto>()).FirstOrDefault();

#endif
    }
}