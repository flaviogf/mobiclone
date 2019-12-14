﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Mobiclone.Api.Models;

namespace Mobiclone.Api.Lib
{
    public class DefaultExtract : IExtract
    {
        private readonly IDbConnection _connection;

        private readonly IAuth _auth;

        public DefaultExtract(IDbConnection connection, IAuth auth)
        {
            _connection = connection;
            _auth = auth;
        }

        public async Task<IList<Transaction>> Read(DateTime begin, DateTime end)
        {
            var user = await _auth.User();

            var transactions = await _connection.QueryAsync<Transaction>(
                @"
                SELECT revenues.Id, revenues.Description, revenues.Value, revenues.Date, revenues.AccountId FROM Revenues revenues
                INNER JOIN Accounts accounts ON revenues.AccountId = accounts.Id
                INNER JOIN Users users ON accounts.UserId = users.Id
                WHERE revenues.Date BETWEEN @Begin AND @End AND users.Id = @UserId
                UNION
                SELECT expenses.Id, expenses.Description, expenses.Value, expenses.Date, expenses.AccountId FROM Expenses expenses
                INNER JOIN Accounts accounts ON expenses.AccountId = accounts.Id
                INNER JOIN Users users ON accounts.UserId = users.Id
                WHERE expenses.Date BETWEEN @Begin AND @End AND users.Id = @UserId
                ORDER BY Date DESC",
                new
                {
                    Begin = begin,
                    End = end,
                    UserId = user.Id
                }
            );

            return transactions.AsList();
        }
    }
}
